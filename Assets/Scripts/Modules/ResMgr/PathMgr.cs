using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using AssetBundles;

namespace GameSets
{
    public class PathMgr
    {
        
        /// <summary>
        /// 控制版本热更的配置文件
        /// </summary>
        public static string VersionConfigPath = "VersionCfg.json";
        /// <summary>
        /// 记录所有ab的各种信息，包括md5名字，bundle名字，size
        /// </summary>
        public static string VersionListFileName = "AssetVersions.json";
        
        private static string _unityEditorEditorUserBuildSettingsActiveBuildTarget;
        public static string UnityEditor_EditorUserBuildSettings_activeBuildTarget
        {
            get
            {
                if (Application.isPlaying && !string.IsNullOrEmpty(_unityEditorEditorUserBuildSettingsActiveBuildTarget))
                {
                    return _unityEditorEditorUserBuildSettingsActiveBuildTarget;
                }

                var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
                foreach (var a in assemblies)
                {
                    if (a.GetName().Name == "UnityEditor")
                    {
                        Type lockType = a.GetType("UnityEditor.EditorUserBuildSettings");
                        //var retObj = lockType.GetMethod(staticMethodName,
                        //    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                        //    .Invoke(null, args);
                        //return retObj;
                        var p = lockType.GetProperty("activeBuildTarget");

                        var em = p.GetGetMethod().Invoke(null, new object[] { }).ToString();
                        _unityEditorEditorUserBuildSettingsActiveBuildTarget = em;
                        return em;
                    }
                }

                return null;
            }
        }
        public static string GetBuildPlatformName()
        {
            string buildPlatformName = "Windows"; // default

            if (Application.isEditor)
            {
                var buildTarget = UnityEditor_EditorUserBuildSettings_activeBuildTarget;
                //UnityEditor.EditorUserBuildSettings.activeBuildTarget;
                switch (buildTarget)
                {
                    case "StandaloneOSXIntel":
                    case "StandaloneOSXIntel64":
                    case "StandaloneOSXUniversal":
                    case "StandaloneOSX":
                        buildPlatformName = "MacOS";
                        break;
                    case "StandaloneWindows": // UnityEditor.BuildTarget.StandaloneWindows:
                    case "StandaloneWindows64": // UnityEditor.BuildTarget.StandaloneWindows64:
                        buildPlatformName = "Windows";
                        break;
                    case "Android": // UnityEditor.BuildTarget.Android:
                        buildPlatformName = "Android";
                        break;
                    case "iPhone": // UnityEditor.BuildTarget.iPhone:
                    case "iOS":
                        buildPlatformName = "iOS";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                buildPlatformName = GetRunningPlatformName();
            }
            
            return buildPlatformName;
        }
        
        public static string GetRunningPlatformName()
        {
            string buildPlatformName = "Windows";
            switch (Application.platform)
            {
                case RuntimePlatform.OSXPlayer:
                    buildPlatformName = "MacOS";
                    break;
                case RuntimePlatform.Android:
                    buildPlatformName = "Android";
                    break;
                case RuntimePlatform.IPhonePlayer:
                    buildPlatformName = "iOS";
                    break;
                case RuntimePlatform.WindowsPlayer:
#if !UNITY_5_4_OR_NEWER
                    case RuntimePlatform.WindowsWebPlayer:
#endif
                    buildPlatformName = "Windows";
                    break;
                default:
                    // Debuger.Assert(false);
                    break;
            }

            return buildPlatformName;
        }
        
        /**路径说明
         * Editor下模拟下载资源：
         *     AppData:C:\xxx\xxx\Appdata
         *     StreamAsset:C:\KSFramrwork\Product
         * 真机：
         *     AppData:Android\data\com.xxx.xxx\files\
         *     StreamAsset:apk内
         */
        private static string editorProductFullPath;
        
        /// <summary>
        /// Product Folder Full Path , Default: C:\KSFramework\Product
        /// </summary>
        public static string EditorProductFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(editorProductFullPath))
                {
                    editorProductFullPath = Path.GetFullPath(string.Format("AssetBundles/{0}/AssetBundles", GetBuildPlatformName()));
                    //editorProductFullPath = AssetBundleConfig.AssetBundleRootPath;
                }
                return editorProductFullPath;
            }
        }
        
        /// <summary>
        /// Bundles/Android/ etc... no prefix for streamingAssets
        /// </summary>
        public static string BundlesPathRelative { get; private set; }
        
        /// <summary>
        /// On Windows, file protocol has a strange rule that has one more slash
        /// </summary>
        /// <returns>string, file protocol string</returns>
        public static string GetFileProtocol
        {
            get
            {
                string fileProtocol = "file://";
                if (Application.platform == RuntimePlatform.WindowsEditor ||
                    Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    fileProtocol = "file:///";
                }else if (Application.platform == RuntimePlatform.Android)
                {
                    fileProtocol = "jar:file://";
                }


                return fileProtocol;
            }
        }
        
        /// <summary>
        /// file://+Application.persistentDataPath
        /// </summary>
        public static string AppDataPathWithProtocol;
        
        private static string appDataPath = null;
        /// <summary>
        /// app的数据目录，有读写权限，实际是Application.persistentDataPath，以/结尾
        /// </summary>
        public static string AppDataPath
        {
            get
            {
                if (appDataPath == null) appDataPath = Application.persistentDataPath + "/";
                return appDataPath;
            }
        }
        
        public static bool ReadStreamFromEditor = true;
        
        /// <summary>
        /// WWW的读取需要file://前缀
        /// </summary>
        public static string AppBasePathWithProtocol { get; private set; }
        
        /// <summary>
        /// 安装包内的路径，移动平台为只读权限，针对Application.streamingAssetsPath进行多平台处理，以/结尾
        /// </summary>
        public static string AppBasePath { get; private set; }
        
         /// <summary>
        /// Initialize the path of AssetBundles store place ( Maybe in PersitentDataPath or StreamingAssetsPath )
        /// </summary>
        /// <returns></returns>
        public static void InitResourcePath()
        {
            // /Users/mac/Desktop/WorkSpace/GameSets/AssetBundles/Android/AssetBundles
            string editorProductPath = EditorProductFullPath;
            // AssetBundles/
            BundlesPathRelative = AssetBundleConfig.AssetBundlesFolderName;
            string fileProtocol = GetFileProtocol;
            // file:///Users/mac/Library/Application Support/Bernabeu/GameSet/
            AppDataPathWithProtocol = fileProtocol + AppDataPath;
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.LinuxEditor:
                {
                    if (ReadStreamFromEditor)
                    {
                        AppBasePath = Application.streamingAssetsPath + "/";
                        AppBasePathWithProtocol = fileProtocol + AppBasePath;
                    }
                    else
                    {
                        AppBasePath = editorProductPath + "/";
                        AppBasePathWithProtocol = fileProtocol + AppBasePath;
                    }
                }
                    break;
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.OSXPlayer:
                {
                    string path = Application.streamingAssetsPath.Replace('\\', '/');
                    AppBasePath = path + "/";
                    AppBasePathWithProtocol = fileProtocol + AppBasePath;
                }
                    break;
                case RuntimePlatform.Android:
                {
                    //文档：https://docs.unity3d.com/Manual/StreamingAssets.html
                    //注意，StramingAsset在Android平台是在apk中，无法通过File读取请使用LoadAssetsSync，如果要同步读取ab请使用GetAbFullPath
                    //NOTE 我见到一些项目的做法是把apk包内的资源放到Assets的上层res内，读取时使用 jar:file://+Application.dataPath + "!/assets/res/"，editor上则需要/../res/
                    AppBasePath = Application.dataPath + "!/assets/";
                    AppBasePathWithProtocol = fileProtocol + AppBasePath;
                }
                    break;
                case RuntimePlatform.IPhonePlayer:
                {
                    // MacOSX下，带空格的文件夹，空格字符需要转义成%20
                    // only iPhone need to Escape the fucking Url!!! other platform works without it!!!
                    AppBasePath = System.Uri.EscapeUriString(Application.dataPath + "/Raw/");
                    AppBasePathWithProtocol = fileProtocol + AppBasePath;
                }
                    break;
                default:
                {
                    //Debuger.Assert(false);
                }
                    break;
            }
        }
         
         /// <summary>
         /// 完整路径，优先级：热更目录->安装包
         /// 根路径：Product
         /// </summary>
         /// <param name="url"></param>
         /// <param name="withFileProtocol">是否带有file://前缀</param>
         /// <param name="raiseError"></param>
         /// <returns></returns>
         public static string GetResourceFullPath(string url, bool withFileProtocol = false, bool raiseError = true)
         {
             string fullPath;
             if (GetResourceFullPath(url, withFileProtocol, out fullPath, raiseError) != GetResourceFullPathType.Invalid)
                 return fullPath;
             return null;
         }

         /// <summary>
         /// 根据相对路径，获取到完整路径，優先从下載資源目录找，没有就读本地資源目錄 
         /// 根路径：Product
         /// </summary>
         /// <param name="url">相对路径</param>
         /// <param name="withFileProtocol"></param>
         /// <param name="fullPath">完整路径</param>
         /// <param name="raiseError">文件不存在打印Error</param>
         /// <returns></returns>
         public static GetResourceFullPathType GetResourceFullPath(string url, bool withFileProtocol, out string fullPath, bool raiseError = true)
         {
             if (string.IsNullOrEmpty(url))
             {
                 Debug.LogErrorFormat("尝试获取一个空的资源路径! url = {0}", url);
                 fullPath = null;
                 return GetResourceFullPathType.Invalid;
             }

             
             string docUrl;
             bool hasDocUrl = TryGetAppDataUrl(url, withFileProtocol, out docUrl);
             if (hasDocUrl)
             {
                 fullPath = docUrl;
                 return GetResourceFullPathType.InDocument;
             }
             
             bool hasInAppUrl = TryGetInAppStreamingUrl(url, withFileProtocol, out string inAppUrl);
             if (!hasInAppUrl)
             {
                 if (raiseError) 
                     UnityEngine.Debug.LogError($"[Not Found] StreamingAssetsPath Url Resource: {url} ,fullPath:{inAppUrl}");
                 fullPath = null;
                 return GetResourceFullPathType.Invalid;
             }

             fullPath = inAppUrl; // 直接使用本地資源！
             return GetResourceFullPathType.InApp;
         }

         /// <summary>
         /// 可读写的目录
         /// </summary>
         /// <param name="url"></param>
         /// <param name="withFileProtocol">是否带有file://前缀</param>
         /// <param name="newUrl"></param>
         /// <returns></returns>
         public static bool TryGetAppDataUrl(string url, bool withFileProtocol, out string newUrl)
         {
             // 如：file:///Users/mac/Library/Application Support/Bernabeu/GameSet/VersionCfg.json.1.0.0
             newUrl = (withFileProtocol ? AppDataPathWithProtocol : AppDataPath) + url;
             return File.Exists(Path.GetFullPath(AppDataPath + url));;
         }
         
         /// <summary>
         /// 可读写的目录
         /// </summary>
         /// <param name="url"></param>
         /// <param name="withFileProtocol">是否带有file://前缀</param>
         /// <param name="newUrl"></param>
         /// <returns></returns>
         public static bool TryGetInAppStreamingUrl(string url, bool withFileProtocol, out string newUrl)
         {
             // 如：file:///Users/mac/Desktop/WorkSpace/GameSets/Assets/StreamingAssets/VersionCfg.json.1.0.0
             newUrl = (withFileProtocol ? AppBasePathWithProtocol : AppBasePath) + url;
             if (Application.isEditor)
             {
                 if (!File.Exists(Path.GetFullPath(AppBasePath + url)))
                 {
                     return false;
                 }
             }else if(Application.platform == RuntimePlatform.Android) // 注意，StreamingAssetsPath在Android平台時，壓縮在apk里面，不要使用File做文件檢查
             {
                 return true; // TODO 待处理
             }else if (Application.platform == RuntimePlatform.IPhonePlayer)
             {
                 if (!File.Exists(Path.GetFullPath(AppBasePath + url)))
                 {
                     return false;
                 }
             }
             return true;
         }
         
         public static string GetAbRelativePath(string abName)
         {
             return BundlesPathRelative + GetRelativeWithHash(abName);
         }
         
         /// <summary>
         /// 获取带有hash值的名字
         /// </summary>
         /// <param name="bundleName"></param>
         /// <returns></returns>
         public static string GetRelativeWithHash(string bundleName)
         {
             var item = GetFileVersionItem(bundleName);
             if (item!=null)
             {
                 return item.file;
             }
             return bundleName;
         }
         
         private static Dictionary<string, AssetVersionItem> _fileListMap = new Dictionary<string, AssetVersionItem>();
         public static  void SetFilePath(AssetVersionList versionList)
         {
             int find = 0;
             for (int k = 0; k < versionList.items.Count; k++)
             {
                 AssetVersionItem item = versionList.items[k];
                 if (!item.name.EndsWith(".manifest"))
                 {
                     _fileListMap[item.name] = item;
                 }
             }
         }
         
         public static AssetVersionItem GetFileVersionItem(string path)
         {
             AssetVersionItem item = null;
             _fileListMap.TryGetValue(path, out item);
             return item;
         }
         
         
         /// <summary>
         /// 资源是否存在
         /// </summary>
         /// <param name="url">相对路径</param>
         /// <param name="raiseError">文件不存在打印Error</param>
         /// <returns></returns>
         public static bool IsResourceExist(string url, bool raiseError = true)
         {
#if UNITY_EDITOR
             if (!url.EndsWith(".lua") && !url.EndsWith(".txt"))
             {
                 //var editorPath = "Assets/" + KEngineDef.ResourcesBuildDir + "/" + url;
                 string editorPath = "Assets/AssetsPackage/" + url;
                 return File.Exists(editorPath);
             }
#endif
             var pathType = GetResourceFullPath(url, false, out string fullPath, raiseError);
             return pathType != GetResourceFullPathType.Invalid;
         }

    }
}

