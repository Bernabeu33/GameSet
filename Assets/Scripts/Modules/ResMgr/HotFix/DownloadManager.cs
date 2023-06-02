using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AssetBundles;
using UnityEngine;

namespace GameSets
{
    public class DownloadManager : Singleton<DownloadManager>
    {
        private readonly List<AssetVersionItem> _hotfixAssetVersionItemList = new List<AssetVersionItem>();

        private int HotfixTotalFileSize { get; set; } = 0;
        private WWWLoader _remoteVersionCfgLoader;
        private WWWLoader _remoteAssetVersionsLoader;
        
        private bool _needReloadLocalCfg = false;
        private VersionItem _localVersionItem;
        private VersionItem _remoteVersionItem;
        private AssetVersionList _localVersionList;
        private AssetVersionList _remoteVersionList;
        private const string DownCheckLoadVersion = "DownCheckLoadVersion";
        private string _remoteAssetVersionsPath;
        private DownLoadTaskMgr _downLoadTaskMgr;
        private float _totalTime = 0f;
        private const float IntervalTime = 1f;
        private bool _updateSuccess;
        
             
        /// <summary>
        /// 加载动更配置文件的超时时间
        /// </summary>
        private const float LoadRemoteCfgTimeout = 5f;
        public IEnumerator Start()
        {
            var data = new Dictionary<string, object>();
            yield return LoadLocalCfg();
            if (_needReloadLocalCfg)
            {
                yield return LoadLocalCfg();
            }
            yield return LoadRemoteCfg();
            var needHotFix = CompareLocalAndRemote();
            if (needHotFix)
            {
                _downLoadTaskMgr = new DownLoadTaskMgr();
                yield return _downLoadTaskMgr.StartDownload(_hotfixAssetVersionItemList, (isWait) =>
                {
                    _totalTime += Time.deltaTime;
                    if (_totalTime >= IntervalTime || !isWait)
                    {
                        
                        _totalTime = 0f;
                    }
                });
                if (_downLoadTaskMgr.Success)
                {
                    SaveVersionCfg();
                }
            }
            yield return EndHotFixUpdate();
        }

        private IEnumerator LoadLocalCfg()
        {
            // 1. 先下载VersionCfg.json.1.0.0 判断在StreamingAssets还是在沙河路径下
            // 2. 如果在streamingAssets下：
            //   2.1: 获取VersionCfg的hash值 ，然后获得AssetBundles下的AssetVersion：ab信息文件
            //   2.2: 存储本地ab数据留作和远程对比
            // 3. 如果在沙河路径下：
            //   3.1 : 重复2.1, 2.2
            //   3.2 : 查看ab映射的文件与本地文件是否存在，如果不存在需要热更
            // VersionCfg.json.1.0.0
            var localVersionCfgPath = $"{PathMgr.VersionConfigPath}.{Settings.Version}";
            var resourceFullPathType = PathMgr.GetResourceFullPath(
                localVersionCfgPath, true, out var localVersionCfgFullPath);
            var inApp = resourceFullPathType == GetResourceFullPathType.InApp; // 如果是安装包内的资源，就不需要检验文件
            //如果读取的是安装包内的资源，尝试清理之前的热更资源
            if (inApp)
            {
                ///为了只在新版本升级时，删除老的无用的资源，防止数据无限增长，做了一个版本标记
                if (!GetVersionMark())
                {
                    TryDeleteHotfixResources();
                    GameUtility.SafeWriteAllText(VersionMark, "");
                }
            }
            // file:///Users/mac/Desktop/WorkSpace/GameSets/Assets/StreamingAssets/VersionCfg.json.1.0.0
            var localVersionCfgLoader = WWWLoader.Load(localVersionCfgFullPath);

            while (!localVersionCfgLoader.isDone)
            {
                yield return null;
            }
            var localVersionCfg = JsonUtility.FromJson<VersionConfig>(localVersionCfgLoader.Www.text);
            localVersionCfgLoader.Release();
            _localVersionItem = localVersionCfg.GetItem(Settings.Version);
            var localItemHash = _localVersionItem.hash;
            // 记录所有ab的各种信息，包括md5名字，bundle名字，size 如：AssetVersions_8d59f762318f00153213275c52e9cdb3.json
            var localAssetVersionsName = PathMgr.VersionListFileName.Replace(".json", $"_{localItemHash}.json");
            // AssetBundles/AssetVersions_8d59f762318f00153213275c52e9cdb3.json
            var localAssetVersionsPath = PathMgr.GetAbRelativePath(localAssetVersionsName);
            // file:///Users/mac/Desktop/WorkSpace/GameSets/Assets/StreamingAssets/AssetBundles/AssetVersions_8d59f762318f00153213275c52e9cdb3.json
            var localAssetVersionsFullPath = PathMgr.GetResourceFullPath(localAssetVersionsPath, true, false);
            if (MApp.Instance.CheckLocalAssetsIsComplete)
            {
                if (string.IsNullOrEmpty(localAssetVersionsFullPath))
                {
                    // 如果没有ab映射文件,说明需要更新
                    // Users/mac/Library/Application Support/Bernabeu/GameSet/VersionCfg.json.1.0.0
                    GameUtility.SafeDeleteFile($"{PathMgr.AppDataPath}{localVersionCfgPath}");
                    _needReloadLocalCfg = true;
                    yield break;
                }    
            }
            var localAssetVersionsLoader = WWWLoader.Load(localAssetVersionsFullPath);
            while (!localAssetVersionsLoader.isDone)
            {
                yield return null;
            }
             
            _localVersionList = JsonUtility.FromJson<AssetVersionList>(localAssetVersionsLoader.Www.text);
            if (MApp.Instance.CheckLocalAssetsIsComplete & !inApp)
            {
                var start = DateTime.Now;
                // 检验沙河路径下文件ab映射下环境是否完整，如果不完整需要更新
                foreach (var assetVersionItem in _localVersionList.items)
                {
                    Debug.Log(assetVersionItem.file);
                    var url = PathMgr.GetAbRelativePath(assetVersionItem.file);
                    var isResourceExist = PathMgr.IsResourceExist(url, false);
                    if (isResourceExist) continue;
                    GameUtility.SafeDeleteFile($"{PathMgr.AppDataPath}{localVersionCfgPath}");
                    _needReloadLocalCfg = true;
                    localAssetVersionsLoader.Release();
                    yield break;
                }
                Debug.Log("CheckLocalAssetsIsComplete use " + (DateTime.Now - start).TotalMilliseconds + " ms");
            }
            _needReloadLocalCfg = false;
            localAssetVersionsLoader.Release();
        }

        IEnumerator LoadRemoteCfg()
        {
            if (!MApp.Instance.IsHotfix)
            {
                yield break;
            }
            // http://192.168.17.69:9999/Android/VersionCfg.json
            var remoteVersionCfgPath = $"{Settings.CDNPath}/{PathMgr.GetBuildPlatformName()}/{PathMgr.VersionConfigPath}";
            _remoteVersionCfgLoader = WWWLoader.Load(remoteVersionCfgPath);
            var timeStart = Time.time;
            while (!_remoteVersionCfgLoader.isDone)
            {
                if (Time.time - timeStart > LoadRemoteCfgTimeout)//超时
                {
                    break;
                }
                yield return null;
            }
            // 没有远程配置
            if (!_remoteVersionCfgLoader.IsSuccess)
            {
                HotfixError.HotfixErrorType = HotfixErrorType.RemoteVersionCfgError;
                Debug.Log(1111 + remoteVersionCfgPath);
                yield break;
            }
            var remoteVersionCfg = JsonUtility.FromJson<VersionConfig>(_remoteVersionCfgLoader.Www.text);
            _remoteVersionItem = remoteVersionCfg.GetItem(Settings.Version);
            if (_remoteVersionItem == null)
            {
                HotfixError.HotfixErrorType = HotfixErrorType.RemoteVersionItemError;
                yield break;
            }
            var remoteItemHash = _remoteVersionItem.hash;
            var localItemHash = _localVersionItem.hash;
            if (localItemHash.Equals(remoteItemHash))
            {
                Debug.Log(
                    $"[Hotfix] <color=red>本地VersionCfg[{localItemHash}]和CDN VersionCfg[{remoteItemHash}]一样, 不需要更新</color>");
                yield break;
            }
            
            Debug.Log(
                $"[Hotfix] <color=red>本地VersionCfg[{localItemHash}]和CDN VersionCfg[{remoteItemHash}]不一样, 需要更新</color>");
            
            // AssetVersions_8d59f762318f00153213275c52e9cdb3.json
            _remoteAssetVersionsPath = PathMgr.VersionListFileName.Replace(".json", $"_{remoteItemHash}.json");
            // AssetBundles/AssetVersions_8d59f762318f00153213275c52e9cdb3.json
            var remoteAssetVersionsRelativePath = PathMgr.GetAbRelativePath(_remoteAssetVersionsPath);
            
            // http://192.168.17.69:9999/Android/AssetBundles/AssetVersions_8d59f762318f00153213275c52e9cdb3.json
            var remoteAssetVersionsFullPath =
                $"{Settings.CDNPath}/{PathMgr.GetBuildPlatformName()}/{remoteAssetVersionsRelativePath}";
            _remoteAssetVersionsLoader = WWWLoader.Load(remoteAssetVersionsFullPath);
            while (!_remoteAssetVersionsLoader.isDone)
            {
                yield return null;
            }

            if (_remoteAssetVersionsLoader.IsSuccess)
            {
                _remoteVersionList = JsonUtility.FromJson<AssetVersionList>(_remoteAssetVersionsLoader.Www.text);
            }
            else
            {
                HotfixError.HotfixErrorType = HotfixErrorType.RemoteAssetVersionsError;
            }
        }

        private bool CompareLocalAndRemote()
        {
            if (_remoteVersionList == null)
            {
                return false;
            }
            var localAssetVersionItemDic = new Dictionary<string, AssetVersionItem>();
            foreach (var assetVersionItem in _localVersionList.items)
            {
                localAssetVersionItemDic.Add(assetVersionItem.hash, assetVersionItem);
            }
            HotfixTotalFileSize = 0;
            _hotfixAssetVersionItemList.Clear();
            Debug.LogFormat("local item count = {0}, remote item count = {1}", _localVersionList.items.Count, _remoteVersionList.items.Count);
            foreach (var assetVersionItem in _remoteVersionList.items)
            {
                var url = PathMgr.GetAbRelativePath(assetVersionItem.file);
                if (!localAssetVersionItemDic.ContainsKey(assetVersionItem.hash))
                {
                    var isResourceExist = PathMgr.IsResourceExist(url, false);
                    if (!isResourceExist)
                    {
                        _hotfixAssetVersionItemList.Add(assetVersionItem);
                        HotfixTotalFileSize += assetVersionItem.size;
                    }
                }
            }
            var needHotfix = _hotfixAssetVersionItemList.Count > 0;
            Debug.Log(
                $"[Hotfix]更新的文件有：<color=red>[{_hotfixAssetVersionItemList.Count}]</color>个, 总大小为<color=red>{(HotfixTotalFileSize / (1024f * 1024f)):0.##}M</color>");
            return needHotfix;
        }
        private string VersionMark
        {
            get
            {
                // 沙河路径：Users/mac/Library/Application Support/Bernabeu/GameSet/DownCheckLoadVersion.1.0.0
                return $"{PathMgr.AppDataPath}{DownCheckLoadVersion}.{Settings.Version}";
            }
        }
        
        private bool GetVersionMark()
        {
            return File.Exists(VersionMark);
        }
        
        private void SaveVersionCfg()
        {
            // /Users/mac/Library/Application Support/Bernabeu/GameSet/AssetBundles/AssetVersions_8d59f762318f00153213275c52e9cdb3.json
            var path = ABUtility.GetPersistentDataPath(_remoteAssetVersionsPath);
            if (GameUtility.SafeWriteAllBytes(path, _remoteAssetVersionsLoader.Www.bytes))
            {
                // Users/mac/Library/Application Support/Bernabeu/GameSet/VersionCfg.json.1.0.0
                var cfgPath = Path.Combine(Application.persistentDataPath,
                    $"{PathMgr.VersionConfigPath}.{Settings.Version}");
                if (GameUtility.SafeWriteAllBytes(cfgPath, _remoteVersionCfgLoader.Www.bytes))
                {
                    _updateSuccess = true;
                }
                else
                {
                    HotfixError.HotfixErrorType = HotfixErrorType.WriteRemoteVersionCfgError;
                }
            }
            else
            {
                HotfixError.HotfixErrorType = HotfixErrorType.WriteRemoteAssetVersionsError;
            }
        }
        
        private IEnumerator EndHotFixUpdate()
        {
            var dcData = new Dictionary<string, object>();
            if (_updateSuccess)
            {
                dcData.Add("result", "success");
                dcData.Add("reason", "");
                SetFilePath(_remoteVersionItem, _remoteVersionList);
            }
            else
            {
                dcData.Add("result", "fail");
                dcData.Add("reason", HotfixError.HotfixErrorType.ToString());
                SetFilePath(_localVersionItem, _localVersionList);
            }
            
            Clear();
            yield return null;
        }

        public override void Init()
        {
            base.Init();
        }
        
        /// <summary>
        /// 尝试清理无用的热更资源
        /// </summary>
        private void TryDeleteHotfixResources()
        {
            var path = ABUtility.GetPersistentDataPath();
            GameUtility.SafeDeleteDir(path);
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
        
        private void SetFilePath(VersionItem versionItem, AssetVersionList assetVersionList)
        {
            Debug.LogFormat("[Hotfix] final use resource = {0}", versionItem.hash);
            Settings.ResVersion = versionItem.resVersion;
            PathMgr.SetFilePath(assetVersionList);
        }
        
        /// <summary>
        /// 各种清理工作
        /// </summary>
        private void Clear()
        {
            if (_remoteAssetVersionsLoader != null)
            {
                _remoteAssetVersionsLoader.Release();
                _remoteAssetVersionsLoader = null;
            }

            if (_remoteVersionCfgLoader != null)
            {
                _remoteVersionCfgLoader.Release();
                _remoteAssetVersionsLoader = null;
            }

            _hotfixAssetVersionItemList.Clear();
            _downLoadTaskMgr = null;
        }
    }
}

