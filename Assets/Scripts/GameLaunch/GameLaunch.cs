using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using AssetBundles;

public class GameLaunch : MonoBehaviour
{
    public GameObject reporterObj = null;
    private void Awake()
    {
        
    }

    private void Start()
    {
#if UNITY_IPHONE
        UnityEngine.iOS.Device.SetNoBackupFlag(Application.persistentDataPath);
#endif
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        string platform = string.Empty;
#if !UNITY_EDITOR
        #if UNITY_IPHONE
        platform = "iOS";
        #elif UNITY_WEBGL
        platform = "WebGL";
        #else
        platform = "Android";
        #endif
#else
        switch (EditorUserBuildSettings.activeBuildTarget)
        {
            case BuildTarget.Android:
                platform = "Android";
                break;
            case BuildTarget.iOS:
                platform = "iOS";
                break;
            case BuildTarget.WebGL:
                platform = "WebGL";
                break;
            default:
                throw new Exception("Error buildTarget!!!");
        }
#endif
        // Editor下路径: /Users/mac/Desktop/WorkSpace/GameSets/AssetBundles/
        string rootPath = Path.Combine(System.Environment.CurrentDirectory, AssetBundleConfig.AssetBundlesFolderName);  
        string AssetBundleRootPath = Path.Combine(rootPath, platform + "/AssetBundles");
        // AssetBundle根路径  如:/Users/mac/Desktop/WorkSpace/GameSets/AssetBundles/Android/AssetBundles
        AssetBundleConfig.AssetBundleRootPath = AssetBundleRootPath;
        StartCoroutine(StartUp());
    }

    IEnumerator StartUp()
    {
        // 配置数据读取
        StartCoroutine(Config());
        // SDK初始化
        var start = DateTime.Now;
        
        yield return AssetBundleManager.Instance.Initialize();
        yield break; 
    }

    /// <summary>
    /// 配置数据读取
    /// </summary>
    /// <returns></returns>
    IEnumerator Config()
    {
        // TODO
        yield break;
    }
}
