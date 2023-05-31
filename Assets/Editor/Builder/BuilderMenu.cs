using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuilderMenu
{
    public const string MenuRoot = "GameSets";
    public const string BundleNode = MenuRoot + "/Bundles/";

    [MenuItem(BundleNode + "LZ4 Append Hash")]
    public static void BuildBundleLZ4AppendHash()
    {
        
    }
}
