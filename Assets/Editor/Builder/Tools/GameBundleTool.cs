using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO;
using System.Text;
using System;
using Codice.CM.Client.Differences.Graphic;
using UnityEditor;
using GameSets;

namespace GameEditor
{
    public class GameBundleTool
    {
        public static void GenerageVersionList()
        {
            List<string> mPaths = new List<string> ();
            List<string> mFiles = new List<string> ();
            
            // 进行 AssetsVersion 并 在此进行
            string osStreamPath = PathUtil.GetOSStreamAssetPath ();
            // /Users/mac/Desktop/WorkSpace/GameSets/Assets/StreamingAssets/AssetBundles/
            var pathBundleDir = osStreamPath + AssetBundleConfig.AssetBundlesFolderName;
            PathUtil.TraverseR (pathBundleDir, ref mFiles, ref mPaths);

            AssetVersionList versionList = new AssetVersionList();
            string bundleName;
            for (int i = 0; i < mFiles.Count; i++)
            {
                string file = mFiles[i];
                if (file.EndsWith (".meta") || file.Contains (".DS_Store") || file.EndsWith(".manifest")) continue;
                bundleName = file.Replace(pathBundleDir, string.Empty);
                FileInfo fileInfo = new FileInfo (file);
                AssetVersionItem item = new AssetVersionItem ();
                item.name = bundleName;
                item.size = (int)fileInfo.Length;
                if (bundleName.EndsWith("AssetBundles"))
                {
                    item.hash = md5file (file);;
                    item.file = bundleName + "_" + item.hash;
                    var fileDestPath = file + "_" + item.hash;
                    if (!File.Exists(fileDestPath))
                    {
                        File.Move(file, fileDestPath);
                    }
                }
                else
                {
                    item.hash = trimHash(bundleName);
                    // 如：prefabs.assetbundle
                    item.name = item.name.Replace("_" + item.hash, string.Empty);
                    item.file = bundleName;
                }
                versionList.items.Add(item);
            }

            string json = JsonUtility.ToJson(versionList);
            string versionMd5 = md5text(json);
            string md5Sufix = "_" + versionMd5;
            string VersionListFileName = "AssetVersions.json";
            // 路径 ：/Users/mac/Desktop/WorkSpace/GameSets/Assets/StreamingAssets/AssetBundles/AssetVersions_8d59f762318f00153213275c52e9cdb3.json
            string outPath = pathBundleDir + VersionListFileName.Replace(".json", md5Sufix + ".json");
           SaveFile(json, outPath);
           GenerateResConfig(versionMd5);
        }

        public static void GenerateResConfig(string versionMd5)
        {
            // /Users/mac/Desktop/WorkSpace/GameSets/Assets/StreamingAssets/VersionCfg.json
            string path = Path.Combine(Application.streamingAssetsPath, "VersionCfg.json");
            string text = "{}";
            if (File.Exists(path))
            {
                text = File.ReadAllText(path);
            }
            VersionConfig versionConfig = JsonUtility.FromJson<VersionConfig>(text);
            string platform = PathMgr.GetBuildPlatformName();
            string updateVersions = BuilderCfg.ResConfig;
            if (updateVersions == "")
            {
                updateVersions = Settings.Version;
            }
            versionConfig.SetItem(platform, updateVersions, versionMd5, BuilderCfg.ResVersion);
            // {"Platforms":[{"Platform":"Android","Versions":[{"version":"1.0.0","hash":"8d59f762318f00153213275c52e9cdb3","resVersion":"0"}]}]}
            string json = JsonUtility.ToJson(versionConfig);
            SaveFile(json, path);
            SaveFile(json, path+"."+Settings.Version);
        }
        
        public static void SaveFile(string str, string outPath)
        {
            if (File.Exists (outPath)) {
                File.Delete (outPath);
            }
            
            FileStream fs = new FileStream (outPath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter (fs);
            sw.Write (str);
            sw.Close ();
            fs.Close ();
            AssetDatabase.Refresh ();
        }
        
        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        public static string md5file (string file) {
            try {
                FileStream fs = new FileStream (file, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider ();
                byte[] retVal = md5.ComputeHash (fs);
                fs.Close ();

                StringBuilder sb = new StringBuilder ();
                for (int i = 0; i < retVal.Length; i++) {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            } catch (Exception ex) {
                throw new Exception("md5file() fail, error:" + ex.Message);
            }
        }
        
        /// <summary>
        /// 计算字符串的MD5值
        /// </summary>
        public static string md5text (string text) {
            string str = "";
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes (text);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider ();
            byte[] bytes = md5.ComputeHash (data);
            for (int i = 0; i < bytes.Length; i++) {
                str += bytes[i].ToString("x2");
            }
            return str;
        }
        
        public static string trimHash(string filePah)
        {
            var path = filePah;
            if (filePah.EndsWith(AssetBundleConfig.AssetBundleSuffix))
            {
                path = filePah.Replace(AssetBundleConfig.AssetBundleSuffix, string.Empty);
            }

            string hash = null;
            string[] arr = path.Split('_');
            if (arr.Length > 1)
            {
                hash = arr[arr.Length - 1];
            }

            return hash;
        }
    }
    
    
}

