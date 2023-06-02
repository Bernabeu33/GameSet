using System.Collections;
using System.Collections.Generic;
using AssetBundles;
using UnityEngine;

namespace GameSets
{
    public class DownloadingTask
    {
        public enum DownloadState
        {
            Initialized,
            Loading,
            Finished,
            Failed,
        }
        
        private const int MaxTryDownloadCount = 3; // 最大尝试下载次数
        private int _currentDownloadCount = 0; // 当前下载次数
        private DownloadState _downloadState;
        private WWWLoader _wwwLoader;
        private readonly AssetVersionItem _assetVersionItem;
        public bool Started => _downloadState == DownloadState.Loading;
        public bool Finished => _downloadState == DownloadState.Finished;
        public bool Failed => _downloadState == DownloadState.Failed;
        public DownloadingTask(AssetVersionItem assetVersionItem, DownloadState downloadState)
        {
            _assetVersionItem = assetVersionItem;
            _downloadState = downloadState;
        }
        public bool Start()
        {
            if (_downloadState == DownloadState.Initialized)
            {
                TryDownload();
                return true;
            }

            return false;
        }
        private void TryDownload()
        {
            _currentDownloadCount++;
            Debug.Log("hotfix file [" + _assetVersionItem.file + "] try download count : " + _currentDownloadCount);
            var url = $"{Settings.CDNPath}/{PathMgr.GetBuildPlatformName()}/{PathMgr.GetAbRelativePath(_assetVersionItem.file)}";
            _wwwLoader = WWWLoader.Load(url);
            _downloadState = DownloadState.Loading;
        }
        
        public void Update()
        {
            if (_downloadState != DownloadState.Loading) return;
            if (_wwwLoader.IsError)
            {
                _wwwLoader?.Release();
                if (_currentDownloadCount <= MaxTryDownloadCount)
                {
                    TryDownload();   
                }
                else
                {
                    HotfixError.HotfixErrorType = HotfixErrorType.DownLoadFileError;
                    _downloadState = DownloadState.Failed;
                }
            }
            else if (_wwwLoader.isDone)
            {
                if (SaveToLocal())
                {
                    _wwwLoader?.Release();
                    _downloadState = DownloadState.Finished;
                }
                else
                {
                    HotfixError.HotfixErrorType = HotfixErrorType.WriteFileError;
                    _wwwLoader?.Release();
                    _downloadState = DownloadState.Failed;
                }
            }
        }
        
        public int DownloadedSize
        {
            get
            {
                switch (_downloadState)
                {
                    case DownloadState.Loading:
                        return _wwwLoader != null ? (int) (_wwwLoader.progress * _assetVersionItem.size) : 0;
                    case DownloadState.Finished:
                        return _assetVersionItem.size;
                    default:
                        return 0;
                }
            }
        }
        
        private bool SaveToLocal()
        {
            var path = ABUtility.GetPersistentDataPath(_assetVersionItem.file);
            return GameUtility.SafeWriteAllBytes(path, _wwwLoader.Www.bytes);
        }
    }
}

