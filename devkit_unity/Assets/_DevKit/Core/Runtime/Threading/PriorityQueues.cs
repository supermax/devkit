using System;
using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;

namespace DevKit.Core.Threading
{
    internal class PriorityQueues
    {
        private readonly ConcurrentQueue<DispatcherTask> _tasksP1 = new();

        private readonly ConcurrentQueue<DispatcherTask> _tasksP2 = new();

        private readonly ConcurrentQueue<DispatcherTask> _tasksP3 = new();

        internal int GetTotalCount()
        {
            var count = _tasksP1.Count + _tasksP2.Count + _tasksP3.Count;
            return count;
        }

        internal int GetCount(DispatcherTaskPriority priority)
        {
            var count = priority switch
            {
                DispatcherTaskPriority.High => _tasksP1.Count,
                DispatcherTaskPriority.Medium => _tasksP2.Count,
                DispatcherTaskPriority.Low => _tasksP3.Count,
                _ => throw new ArgumentOutOfRangeException(nameof(priority), priority, "Unsupported priority")
            };
            return count;
        }

        private static IEnumerator Process(ConcurrentQueue<DispatcherTask> tasks, DispatcherTaskPriority priority)
        {
            const string logFormat = "[±] Dispatching task {0} [Queue Priority: {1}, Queue Tasks Count: {2}]";

            while(tasks.Count > 0)
            {
                if (!tasks.TryDequeue(out var task))
                {
                    continue;
                }
                Debug.LogFormat(logFormat, task, priority, tasks.Count);

                if (task.Delay.HasValue)
                {
                    yield return new WaitForSeconds(task.Delay.Value);
                }

                task.Invoke();
                task.Dispose();
            }
        }

        internal IEnumerator Process()
        {
            yield return Process(_tasksP1, DispatcherTaskPriority.High);
            yield return Process(_tasksP2, DispatcherTaskPriority.Medium);
            yield return Process(_tasksP3, DispatcherTaskPriority.Low);
        }

        internal void Dispatch(Delegate action, object[] payload, DispatcherTaskPriority priority = DispatcherTaskPriority.Medium, float? delay = null)
        {
            var task = new DispatcherTask(action, payload, priority, delay);
            switch (priority)
            {
                case DispatcherTaskPriority.High:
                    _tasksP1.Enqueue(task);
                    break;

                case DispatcherTaskPriority.Medium:
                    _tasksP2.Enqueue(task);
                    break;

                case DispatcherTaskPriority.Low:
                    _tasksP3.Enqueue(task);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(priority), priority, "Unsupported priority");
            }
        }

        internal void Clear()
        {
            _tasksP1.Clear();
            _tasksP2.Clear();
            _tasksP3.Clear();
        }
    }
}
