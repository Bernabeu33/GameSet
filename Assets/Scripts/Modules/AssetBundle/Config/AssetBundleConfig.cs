using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class AssetBundleConfig
    {
        public const string ManifestBundleName = "AssetBundles";
        public const string AssetsFolderName = "AssetsPackage";
        public const string AssetBundlesFolderName = "AssetBundles/";
        public const string AssetsPathMapFileName = "AssetsMap.bytes";
        public const string AssetBundleSuffix = ".assetbundle";
        public const string CommonMapPattren = ",";
        
        private static string _rootPath = string.Empty;
        //AssetBundle根路径
        public static string AssetBundleRootPath
        {
            get
            {
                return _rootPath;
            }
            set
            {
                _rootPath = value;
            }
        }
    }


