using System;
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
        private readonly PriorityQueues _tasks = new();

        public int ThreadId
        {
            get;
            private set;
        }

        public int TasksCount
        {
            get
            {
                return _tasks.GetTotalCount();
            }
        }

        protected override void OnAwake()
        {
            Init();
            base.OnAwake();
        }

        public void Dispatch(Delegate action, object[] payload, DispatcherTaskPriority priority = DispatcherTaskPriority.Medium, float? delay = null)
        {
            _tasks.Dispatch(action, payload, priority);
        }

        private void Update()
        {
            StartCoroutine(_tasks.Process());
        }

        public void Dispose()
        {
            enabled = false;
            _tasks.Clear();
        }

        public void Init()
        {
            ThreadId = Thread.CurrentThread.ManagedThreadId;
            Debug.LogFormat($"[±] {nameof(Thread.CurrentThread.ManagedThreadId)}: ThreadId");
        }

        public void Reset()
        {
            _tasks.Clear();
        }
    }
}
