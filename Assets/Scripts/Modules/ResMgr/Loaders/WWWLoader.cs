using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssetBundles;
using System;

namespace GameSets
{
    public class WWWLoader : BaseLoader
    {
        // 前几项用于监控器
        /// <summary>
        /// 专门监控WWW的协程
        /// </summary>
        private static IEnumerator CachedWWWLoaderMonitorCoroutine;
        
        /// <summary>
        /// 同时进行的最大Www加载个数，超过的排队等待
        /// </summary>
        private const int MAX_WWW_COUNT = 15;
        public static bool LoadByQueue = false;
        
        /// <summary>
        /// 有多少个WWW正在运作, 有上限的
        /// </summary>
        private static int WWWLoadingCount = 0;
        
        /// <summary>
        /// WWWLoader的加载是后进先出! 有一个协程全局自我管理. 后来涌入的优先加载！
        /// </summary>
        private static readonly Stack<WWWLoader> WWWLoadersStack = new Stack<WWWLoader>();
        
        public static event Action<string> WWWFinishCallback;
        
        public WWW Www;
        public float BeginLoadTime;
        public float FinishLoadTime;
        
        public static WWWLoader Load(string url, LoaderDelgate callback = null)
        {
            var wwwLoader = AutoNew<WWWLoader>(url, callback);
            return wwwLoader;
        }

        public override void Init(string url, params object[] args)
        {
            base.Init(url, args);
            WWWLoadersStack.Push(this);
            if (CachedWWWLoaderMonitorCoroutine == null)
            {
                CachedWWWLoaderMonitorCoroutine = WWWLoaderMonitorCoroutine();
                AssetBundleManager.Instance.StartCoroutine(CachedWWWLoaderMonitorCoroutine);
            }
            
        }

        /// <summary>
        /// 监视器协程
        /// 超过最大WWWLoader时，挂起~
        /// 后来的新loader会被优先加载
        /// </summary>
        /// <returns></returns>
        protected static IEnumerator WWWLoaderMonitorCoroutine()
        {
            //yield return new WaitForEndOfFrame(); // 第一次等待本帧结束
            yield return null;

            while (WWWLoadersStack.Count > 0)
            {
                if (LoadByQueue)
                {
                    while (AssetBundleManager.GetCount<WWWLoader>() != 0)
                        yield return null;
                }

                while (WWWLoadingCount >= MAX_WWW_COUNT)
                {
                    yield return null;
                }

                var wwwLoader = WWWLoadersStack.Pop();
                wwwLoader.StartLoad();
            }

            AssetBundleManager.Instance.StopCoroutine(CachedWWWLoaderMonitorCoroutine);
            CachedWWWLoaderMonitorCoroutine = null;
        }
        
        protected void StartLoad()
        {
            AssetBundleManager.Instance.StartCoroutine(CoLoad(Url)); //开启协程加载Assetbundle，执行Callback
        }
        
        /// <summary>
        /// 协程加载Assetbundle，加载完后执行callback
        /// </summary>
        /// <param name="url">资源的url</param>
        /// <returns></returns>
        private IEnumerator CoLoad(string url)
        {
            // url 如:file:///Users/mac/Desktop/WorkSpace/GameSets/Assets/StreamingAssets/VersionCfg.json.1.0.0
            Www = new WWW(url);
            BeginLoadTime = Time.time;
            WWWLoadingCount++;
            //设置AssetBundle解压缩线程的优先级
            Www.threadPriority = Application.backgroundLoadingPriority; // 取用全局的加载优先速度
            while (!Www.isDone)
            {
                Progress = Www.progress;
                yield return null;
            }
            yield return Www;
            WWWLoadingCount--;
            Progress = 1;
            if (IsReadyDisposed)
            {
                Debug.LogErrorFormat("[WWWLoader]Too early release: {0}", url);
                OnFinish(null);
                yield break;
            }

            if (!string.IsNullOrEmpty(Www.error))
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    // TODO: Android下的错误可能是因为文件不存在!
                }
#if DEBUG
                Debug.LogErrorFormat("[WWWLoader:Error]{0} {1}", Www.error, url);
#endif
                OnFinish(null);
                yield break;
            }
            else
            {
                if (WWWFinishCallback != null)
                    WWWFinishCallback(url);
                
                Desc = string.Format("{0}K", Www.bytes.Length / 1024f);
                OnFinish(Www);
            }
            
            // 预防WWW加载器永不初始化, 造成内存泄露~
            if (Application.isEditor)
            {
                while (AssetBundleManager.GetCount<WWWLoader>() > 0)
                    yield return null;

                yield return new WaitForSeconds(5f);

                while (!IsReadyDisposed)
                {
                    Debug.LogErrorFormat("[WWWLoader]Not Disposed Yet! : {0}", this.Url);
                    yield return null;
                }
            }
            
        }
        
        protected override void OnFinish(object resultObj)
        {
            FinishLoadTime = Time.time;
            base.OnFinish(resultObj);
        }

        protected override void DoDispose()
        {
            base.DoDispose();

            Www.Dispose();
            Www = null;
        }
    } 
}

