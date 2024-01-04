using System;
using System.Threading;
using DevKit.Core.Objects;

namespace DevKit.Core.Threading
{
    public class MainThreadDispatcher
        : Singleton<IThreadDispatcher, MainThreadDispatcher>
        , IThreadDispatcher
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

        public MainThreadDispatcher()
        {
            ThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public void Dispatch(Delegate action, object[] payload, DispatcherTaskPriority priority = DispatcherTaskPriority.Medium, float? delay = null)
        {
            _tasks.Dispatch(action, payload, priority);
        }

        // TODO adjust for classic .NET async queue dispatching
        private void Update()
        {
            _tasks.Process();
        }
    }
}
