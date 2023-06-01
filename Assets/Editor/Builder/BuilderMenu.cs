using System.IO;
using UnityEditor;
using UnityEngine;
using AssetBundleBrowser;

namespace AssetBundleBrowser
{
    public class BuilderMenu
    {
        public const string MenuRoot = "GameSets";
        public const string BundleNode = MenuRoot + "/Bundles/";
       

        [MenuItem(BundleNode + "LZ4 Append Hash")]
        public static void BuildBundleLZ4AppendHash()
        {
             BuildBundle(AssetBundleBuildTab.CompressOptions.ChunkBasedCompression, true);
        }

        [MenuItem(MenuRoot + "/Lua/Copy Lua")]
        public static void CopyLua()
        {
            CopyLuaToAssetsPackage();
        }
        
        private static void BuildBundle(AssetBundleBuildTab.CompressOptions compressOption, bool appendHash = false)
        {
            var path = Path.Combine(Application.streamingAssetsPath, AssetBundleConfig.ManifestBundleName);
            GameUtility.SafeDeleteDir(path);

            CopyLua();
            AssetBundleBuildTab buildTab = new AssetBundleBuildTab();
            buildTab.RealEnable();
            buildTab.ResetState();
            buildTab.SetAppendHash(appendHash);
            buildTab.SetCompressType(compressOption);
            buildTab.ExecuteMethod();
        }
       

        private static void CopyLuaToAssetsPackage()
        {
            string destination = Path.Combine(Application.dataPath, AssetBundleConfig.AssetsFolderName);
            // 目标路径:/Users/mac/Desktop/WorkSpace/GameSets/Assets/AssetsPackage
            destination = Path.Combine(destination, "Lua");
            // 源路径:/Users/mac/Desktop/WorkSpace/GameSets/Assets/LuaScripts
            string source = Path.Combine(Application.dataPath, "LuaScripts");
            GameUtility.SafeDeleteDir(destination);
            
            FileUtil.CopyFileOrDirectoryFollowSymlinks(source, destination);
            var notLuaFiles = GameUtility.GetSpecifyFilesInFolder(destination, new string[] { ".lua", ".pb" }, true);
            if (notLuaFiles != null && notLuaFiles.Length > 0)
            {
                for (int i = 0; i < notLuaFiles.Length; i++)
                {
                    GameUtility.SafeDeleteFile(notLuaFiles[i]);
                }
            }

            var luaFiles = GameUtility.GetSpecifyFilesInFolder(destination, new string[] { ".lua", ".pb" }, false);
            if (luaFiles != null && luaFiles.Length > 0)
            {
                for (int i = 0; i < luaFiles.Length; i++)
                {
                    GameUtility.SafeRenameFile(luaFiles[i], luaFiles[i] + ".bytes");
                }
            }

            AssetDatabase.Refresh();
           // Debug.Log("Copy lua files over");
        }
    }

}
