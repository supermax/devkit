#region

using System;
using System.Collections.Concurrent;
using System.Threading;
using DevKit.Core.WeakRef;
using DevKit.Logging.Extensions;

#endregion

namespace DevKit.PubSub.Demo
{
    public class ThreadQueue<T> : IDisposable
    {
        private readonly ConcurrentQueue<Tuple<WeakReferenceDelegate, T>> _queue = new();

        private Timer _timer;

        public void Dispose()
        {
            Stop();
            Clear();
        }

        private void OnTick(object state)
        {
            this.LogInfo("OnTick - Thread ID: {0}", Thread.CurrentThread.ManagedThreadId.ToString());

            while (_queue.Count > 0)
            {
                if (_queue.TryDequeue(out var tuple))
                {
                    tuple.Item1.Invoke(new object[] {tuple.Item2});
                }
            }
        }

        public void Start()
        {
            _timer = new Timer(OnTick, null, 0, 1000);
        }

        public void Stop()
        {
            if (_timer == null) return;

            _timer.Dispose();
            _timer = null;
        }

        public void Clear()
        {
            while (_queue.Count > 0)
            {
                if (_queue.TryDequeue(out var tuple))
                {
                    tuple.Item1.Dispose();
                }
            }
        }

        public void Enqueue(Action<T> method, T parameter)
        {
            _queue.Enqueue(new Tuple<WeakReferenceDelegate, T>(new WeakReferenceDelegate(method), parameter));
        }
    }
}
