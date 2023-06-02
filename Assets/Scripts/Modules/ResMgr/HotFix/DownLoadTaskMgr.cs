using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameSets
{
    public class DownLoadTaskMgr
    {
        private readonly LinkedList<DownloadingTask> _pendingTasks = new LinkedList<DownloadingTask>();
        private readonly LinkedList<DownloadingTask> _downloadingTasks = new LinkedList<DownloadingTask>();
        private readonly LinkedList<DownloadingTask> _finishedTasks = new LinkedList<DownloadingTask>();
        private readonly LinkedList<DownloadingTask> _failedTasks = new LinkedList<DownloadingTask>();
        
        private int MaxDownloadingTaskCount { get; } = 5;
        private bool _success = false;
        
        
        public int DownloadedSize {
            get {
                var size = 0;
                foreach (var task in _finishedTasks)
                    size += task.DownloadedSize;

                foreach (var task in _downloadingTasks)
                    size += task.DownloadedSize;

                return size;
            }
        }
        
        public bool Success
        {
            get { return _success; }
        }
        
        public IEnumerator StartDownload(List<AssetVersionItem> hotfixAssetVersionItemList, Action<bool> action)
        {
            ClearTask();
            foreach (var assetVersionItem in hotfixAssetVersionItemList)
            {
                AddTask(assetVersionItem, false);
            }
            
            while (true)
            {
                var downloadingTasksCount = 0;

                var node = _downloadingTasks.First;
                while (node != null)
                {
                    downloadingTasksCount++;
                    var next = node.Next;
                    var task = node.Value;
                    if (task != null)
                    {
                        task.Update();
                        action(true);
                        if (task.Finished)
                        {
                            node.List.Remove(node);
                            _finishedTasks.AddLast(node);
                        }
                        else if (task.Failed)
                        {
                            node.List.Remove(node);
                            _failedTasks.AddLast(node);
                        }
                    }

                    node = next;
                }

                downloadingTasksCount = _downloadingTasks.Count;

                // Start loading
                var pendingNode = _pendingTasks.First;
                while (downloadingTasksCount < MaxDownloadingTaskCount && pendingNode != null)
                {
                    downloadingTasksCount++;
                    var next = pendingNode.Next;
                    var task = pendingNode.Value;
                    if (task != null && task.Start())
                    {
                        pendingNode.List.Remove(pendingNode);
                        _downloadingTasks.AddLast(pendingNode);
                    }
                    pendingNode = next;
                }
                action(false);
                if (_downloadingTasks.Count <= 0 && _pendingTasks.Count <= 0)
                {
                    if (_failedTasks.Count <= 0)
                    {
                        _success = true;
                    }
                    yield break;
                }

                yield return null;
            }
        }
        
        private void AddTask(AssetVersionItem hotAssetVersionItem, bool addFinishedTask)
        {
            if (addFinishedTask)
            {
                _finishedTasks.AddLast(new DownloadingTask(hotAssetVersionItem, DownloadingTask.DownloadState.Finished));
            }
            else
            {
                _pendingTasks.AddLast(new DownloadingTask(hotAssetVersionItem, DownloadingTask.DownloadState.Initialized));
            }
        }
        
        private void ClearTask()
        {
            _success = false;
            _pendingTasks.Clear();
            _downloadingTasks.Clear();
            _finishedTasks.Clear();
            _failedTasks.Clear();
        }

    }
}

