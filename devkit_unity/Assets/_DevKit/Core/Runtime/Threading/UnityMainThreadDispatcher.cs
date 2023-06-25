using System;
using System.Collections.Concurrent;
using System.Threading;
using DevKit.Core.Objects;
using UnityEngine;

namespace DevKit.Core.Threading
{
    public class UnityMainThreadDispatcher
        : MonoBehaviourSingleton<IThreadDispatcher, UnityMainThreadDispatcher>
            , IThreadDispatcher
            , IDisposable
            , IInitializable
    {
        private readonly ConcurrentQueue<DispatcherTask> _tasks = new();

        public int ThreadId
        {
            get;
            private set;
        }

        public int TasksCount
        {
            get
            {
                return _tasks.Count;
            }
        }

        protected override void OnAwake()
        {
            Init();
            base.OnAwake();
        }

        public void Dispatch(Delegate action, object[] payload)
        {
            _tasks.Enqueue(new DispatcherTask(action, payload));
        }

        private void Update()
        {
            while(_tasks.Count > 0)
            {
                if (!_tasks.TryDequeue(out var task))
                {
                    continue;
                }
                Debug.LogFormat($"Dispatching {task} ({_tasks.Count}: {_tasks.Count})");

                task.Invoke();
                task.Dispose();
            }
        }

        public void Dispose()
        {
            enabled = false;
            _tasks.Clear();
        }

        public void Init()
        {
            ThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public void Reset()
        {
            _tasks.Clear();
        }
    }
}
