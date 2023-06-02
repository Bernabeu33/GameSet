using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XLua;
using System;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using Debug = UnityEngine.Debug;
#if UNITY_EDITOR
using UnityEditor;
#endif
using AssetBundles;
using UnityEngine.U2D;
using GameSets;


namespace AssetBundles
{
    public enum GetResourceFullPathType
    {
        /// <summary>
        /// 无资源
        /// </summary>
        Invalid,

        /// <summary>
        /// 安装包内
        /// </summary>
        InApp,

        /// <summary>
        /// 热更新目录
        /// </summary>
        InDocument,
    }
    
    [Hotfix] 
    [LuaCallCSharp]
    public class AssetBundleManager: MonoSingleton<AssetBundleManager>
    {
        private static readonly Dictionary<Type, Dictionary<string, BaseLoader>> _loadersPool = new Dictionary<Type, Dictionary<string, BaseLoader>>();
        // <summary>
        /// 进行垃圾回收
        /// </summary>
        internal static readonly Dictionary<BaseLoader, float> UnUsesLoaders =  new Dictionary<BaseLoader, float>();
        public IEnumerator Initialize()
        {
            PathMgr.InitResourcePath();
            var start = DateTime.Now;
            yield return DownloadManager.instance.Start();
        }
        
        public static Dictionary<string, BaseLoader> GetTypeDict(Type type)
        {
            Dictionary<string, BaseLoader> typesDict;
            if (!_loadersPool.TryGetValue(type, out typesDict))
            {
                typesDict = _loadersPool[type] = new Dictionary<string, BaseLoader>();
            }

            return typesDict;
        }
        
        public static int GetCount<T>()
        {
            return GetTypeDict(typeof(T)).Count;
        }
    }
}

