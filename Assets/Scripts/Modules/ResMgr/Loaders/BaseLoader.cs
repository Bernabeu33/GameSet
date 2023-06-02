using UnityEngine;
using System;
using System.Collections.Generic;
using AssetBundles;
using XLua;

namespace GameSets
{
    [LuaCallCSharp]
    public abstract class BaseLoader : IAsyncObject
    {
        public delegate void LoaderDelgate(bool isOk, object resultObject);
        private readonly List<BaseLoader.LoaderDelgate> _afterFinishedCallbacks = new List<BaseLoader.LoaderDelgate>();
        public object ResultObject { get; private set; }
        public object AsyncResult
        {
            get { return ResultObject; }
        }
        
        public bool IsCompleted { get; private set; }
        public virtual bool IsError { get; private set; }

        public virtual string AsyncMessage
        {
            get { return null; }
        }

        public bool IsSuccess
        {
            get { return !IsError && ResultObject != null && !IsReadyDisposed; }
        }
        /// <summary>
        /// RefCount 为 0，进入预备状态
        /// </summary>
        protected bool IsReadyDisposed { get; private set; }
        
        /// <summary>
        /// 是否处于Application退出状态
        /// </summary>
        private bool _isQuitApplication = false;

        /// <summary>
        /// ForceNew的，非AutoNew
        /// </summary>
        public bool IsForceNew;
        
        [System.NonSerialized]
        public float InitTiming = -1;
        [System.NonSerialized]
        public float FinishTiming = -1;
        /// <summary>
        /// 加载用时
        /// </summary>
        public float FinishUsedTime
        {
            get
            {
                if (!IsCompleted) return -1f;
                return FinishTiming - InitTiming;
            }
        }
        
        private int refCount = 0;

        public int RefCount
        {
            get { return refCount; }
            set
            {
                refCount = value;
            }
        }

        public string Url { get; private set; }
        /// <summary>
        /// 进度0-1
        /// </summary>
        public virtual float Progress { get; protected set; }

        public float progress
        {
            get { return Progress; }
        }
        
        public bool isDone
        {
            get
            {
                return IsCompleted;
            }
        }
        public event Action DisposeEvent;
        public event Action<string> SetDescEvent;
        private string _desc = "";

        /// <summary>
        /// 描述, 额外文字, 一般用于资源Debugger用
        /// </summary>
        /// <returns></returns>
        public virtual string Desc
        {
            get { return _desc; }
            set
            {
                _desc = value;
                if (SetDescEvent != null)
                    SetDescEvent(_desc);
            }
        }
     
        
        protected static T AutoNew<T>(string url, LoaderDelgate callback = null, bool forceCreateNew = false, params object[] initArgs) where T : BaseLoader, new()
        {
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogErrorFormat("[{0}:AutoNew]url为空", typeof(T));
                return null;
            }
            
            Dictionary<string, BaseLoader> typesDict = AssetBundleManager.GetTypeDict(typeof(T));
            BaseLoader loader;
            typesDict.TryGetValue(url, out loader);
            if (forceCreateNew || loader == null )
            {
                loader = new T();
                if (!forceCreateNew)
                {
                    typesDict[url] = loader;
                }

                loader.IsForceNew = forceCreateNew;
                loader.Init(url, initArgs);
                // if (Application.isEditor)
                // {
                //     MResourceLoaderDebugger.Create(typeof(T).Name, url, loader);
                // }
            }
            else if (loader != null && loader.IsCompleted && loader.IsError)
            {
                loader.Init(url,initArgs);
            }
            else
            {
                if (loader.RefCount < 0)
                {
                    Debug.LogError("Error RefCount!");
                }
            }

            loader.RefCount++;

            // RefCount++了，重新激活，在队列中准备清理的Loader
            if (AssetBundleManager.UnUsesLoaders.ContainsKey(loader))
            {
                AssetBundleManager.UnUsesLoaders.Remove(loader);
                loader.Revive();
            }

            loader.AddCallback(callback);

            return loader as T;
        }
        
        protected BaseLoader()
        {
            RefCount = 0;
        }
        
        public virtual void Init(string url, params object[] args)
        {
            InitTiming = Time.realtimeSinceStartup;
            ResultObject = null;
            IsReadyDisposed = false;
            IsError = false;
            IsCompleted = false;

            Url = url;
            Progress = 0f;
        }
        
        protected virtual void OnFinish(object resultObj)
        {
            ResultObject = resultObj;
            var callbackObject = !IsReadyDisposed ? ResultObject : null;

            FinishTiming = Time.realtimeSinceStartup;
            Progress = 1f;
            IsError = callbackObject == null;

            IsCompleted = true;
            DoCallback(IsSuccess, callbackObject);

            if (IsReadyDisposed)
            {
                Debug.LogFormat("[AbstractResourceLoader:OnFinish]时，准备Disposed {0}", Url);
            }
        }
        
        public void AddCallback(LoaderDelgate callback)
        {
            if (callback != null)
            {
                if (IsCompleted)
                {
                    if (ResultObject == null)
                        Debug.LogErrorFormat("Null ResultAsset {0}", Url);
                    callback(ResultObject != null, ResultObject);
                }
                else
                    _afterFinishedCallbacks.Add(callback);
            }
        }
        
        public virtual void Revive()
        {
            IsReadyDisposed = false;
        }
        
        protected void DoCallback(bool isOk, object resultObj)
        {
            foreach (var callback in _afterFinishedCallbacks)
            {
                callback(isOk, resultObj);
            }
            _afterFinishedCallbacks.Clear();
        }
        
        
        public virtual void Release(bool gcNow)
        {
            Release();
            
        }
        
        public virtual void Release()
        {
            if (IsReadyDisposed )
            {
                Debug.LogWarningFormat("[{0}]repeat  dispose! {1}, Count: {2}", GetType().Name, this.Url, RefCount);
            }

            RefCount--;
            if (RefCount <= 0)
            {
               

                // 加入队列，准备Dispose
                AssetBundleManager.UnUsesLoaders[this] = Time.time;

                IsReadyDisposed = true;
                OnReadyDisposed();
            }
        }
        
        protected virtual void OnReadyDisposed()
        {
        }
        
        public void Dispose()
        {
            if (DisposeEvent != null)
                DisposeEvent();

            if (!IsForceNew)
            {
                var type = GetType();
                var typeDict = AssetBundleManager.GetTypeDict(type);
                if (Url != null) 
                {
                    var bRemove = typeDict.Remove(Url);
                    if (!bRemove)
                    {
                        Debug.LogWarningFormat("[{0}:Dispose]No Url: {1}, Cur RefCount: {2}", type.Name, Url, RefCount);
                    }
                }
            }

            if (IsCompleted)
                DoDispose();
            // 未完成，在OnFinish时会执行DoDispose
        }
        
        public virtual void ForceDispose()
        {
            if (_isQuitApplication)
                return;
            if (RefCount != 1)
            {
                Debug.LogWarning("[ForceDisose]Use force dispose to dispose loader, recommend this loader RefCount == 1");
            }
            Dispose();
            IsReadyDisposed = true;
        }
        
        protected virtual void DoDispose()
        {
            
        }
        
        protected void OnApplicationQuit()
        {
            _isQuitApplication = true;
        }
    }
}