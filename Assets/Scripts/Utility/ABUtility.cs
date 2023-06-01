using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssetBundles
{
    public class ABUtility 
    {
        public static string AssetsPathToPackagePath(string assetPath)
        {
            // Assets/AssetsPackage/lua
            string path = "Assets/" + AssetBundleConfig.AssetsFolderName + "/";
            if (assetPath.StartsWith(path))
            {
                // lua
                return assetPath.Substring(path.Length);
            }
            else
            {
                Debug.LogWarning(assetPath + " is not a package path!");
                return assetPath;
            }
        }
        
        public static string PackagePathToAssetsPath(string assetPath)
        {
            // Assets/AssetBundles/ + assetPath
            return "Assets/" + AssetBundleConfig.AssetsFolderName + "/" + assetPath;
        }
    } 
}

