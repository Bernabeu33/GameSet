using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class PathUtil
{
    /// <summary>
    /// 获取StreamingAssets的子路径
    /// </summary>
    public static string GetOSStreamAssetPath () {
        return Application.streamingAssetsPath + "/";
    }

    public static void TraverseR(string path, ref List<string> files, ref List<string> paths)
    {
        string[] names = Directory.GetFiles (path);
        string[] dirs = Directory.GetDirectories (path);
        foreach (string name in names)
        {
            string ext = Path.GetExtension (name);
            if (!ext.Equals (".meta")) {
                files.Add (name.Replace ('\\', '/'));
            }
        }

        foreach (string dir in dirs)
        {
            paths.Add (dir.Replace ('\\', '/'));
            TraverseR (dir, ref files, ref paths);
        }
    }
}
