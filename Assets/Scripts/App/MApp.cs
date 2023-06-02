using System;
using UnityEngine;

namespace GameSets
{
    public class MApp: MonoSingleton<MApp>
    {
        private static bool _IsApplicationQuited = false;
        public static bool IsApplicationQuited
        {
            get { return _IsApplicationQuited; }
        }

        public bool ForceLuaFileInEditor = true;
        public bool IsLogAbInfo = false;
        public bool IsEditorLoadAsset = false;
        public bool IsLogDeviceInfo = true;
        public bool IsLogAbLoadCost = true;
        public bool IsLoadAssetBundle = true;
        
        public bool UseAssetDebugger = true;
        [Tooltip("是否热更新")]
        public bool IsHotfix = false;
        [Tooltip("校验本地文件是否完整")]
        public bool CheckLocalAssetsIsComplete = false;
        private void OnApplicationQuit()
        {
            _IsApplicationQuited = true;
        }
    }
}