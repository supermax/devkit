using System;

namespace DevKit.Core.Threading
{
    public interface IThreadDispatcher
    {
        int ThreadId { get; }

        void Dispatch(Delegate action, object[] payload, DispatcherTaskPriority priority = DispatcherTaskPriority.Medium, float? delay = null);
    }
}
