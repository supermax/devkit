using System;
using DevKit.Core.WeakRef;

namespace DevKit.Core.Threading
{
    public class DispatcherTask : IDisposable
    {
        private WeakReferenceDelegate Action
        {
            get; set;
        }

        private object[] Payload
        {
            get;
            set;
        }

        private DispatcherTaskPriority Priority
        {
            get;
            set;
        }

        public float? Delay
        {
            get;
            set;
        }

        public DispatcherTask(Delegate action
            , object[] payload
            , DispatcherTaskPriority priority = DispatcherTaskPriority.Medium
            , float? delay = null)
        {
            Action = new WeakReferenceDelegate(action);
            Payload = payload;
            Priority = priority;
            Delay = delay;
        }

        public void Invoke()
        {
            if(Action == null || !Action.IsAlive)
            {
                return;
            }
            Action.Invoke(Payload);
        }

        public void Dispose()
        {
            if(Action != null)
            {
                Action.Dispose();
                Action = null;
            }
            Payload = null;
        }

        public override string ToString()
        {
            return $"{Action}";
        }
    }
}
