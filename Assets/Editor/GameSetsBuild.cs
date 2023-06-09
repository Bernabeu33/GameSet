using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameSetsBuild
{
    [MenuItem("GameSets/Build/Apk")]
    public static void BuildApk()
    {
        BuildPlayerOptions options = new BuildPlayerOptions();
        options.target = BuildTarget.Android;
        options.options = BuildOptions.None;
        options.scenes = new string[]{"Assets/Scenes/LaunchScene.unity"};
        options.locationPathName = @"/Users/mac/Desktop/WorkSpace/Android/test.apk";
        var report = BuildPipeline.BuildPlayer(options);
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log("Build result =  succeeded: " + report.summary.totalSize + "bytes");
        }

        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Failed)
        {
            Debug.LogError("Build result =  failed  ++++");
        }
    }
}
