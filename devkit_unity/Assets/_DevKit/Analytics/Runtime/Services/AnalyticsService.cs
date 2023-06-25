using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using DevKit.Analytics.Events.API;
using DevKit.Analytics.Services.API;
using DevKit.Core.Threading;

namespace DevKit.Analytics.Services
{
    public abstract class AnalyticsService : IAnalyticsService, IDisposable
    {
        private readonly ConcurrentQueue<IAnalyticsEvent> _eventsQueue = new();

        private readonly ConcurrentQueue<IAnalyticsEvent> _unprocessedEventsQueue = new();

        private readonly IThreadDispatcher _dispatcher;

        private Timer _timer;

        private bool _isProcessing;

        protected bool IsInitialized { get; set; }

        protected bool IsInitializing { get; set; }

        public AnalyticsServiceConfig Config { get; } = new();

        protected AnalyticsService(IThreadDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public virtual void SendEvent(IAnalyticsEvent analyticsEvent)
        {
            _eventsQueue.Enqueue(analyticsEvent);
            ProcessEvents();
        }

        public virtual void SendEvents(IEnumerable<IAnalyticsEvent> analyticsEvents)
        {
            foreach (var analyticsEvent in analyticsEvents)
            {
                _eventsQueue.Enqueue(analyticsEvent);
            }
            ProcessEvents();
        }

        protected virtual void ProcessEvents()
        {
            if (_eventsQueue.IsEmpty || _isProcessing)
            {
                return;
            }
            EnableTimer();
        }

        protected void EnableTimer()
        {
            _timer = new Timer(OnTimerTick, this, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }

        protected void DisableTimer()
        {
            _timer?.Dispose();
            _timer = null;
        }

        protected virtual void OnTimerTick(object state)
        {
            if (!_isProcessing && _eventsQueue.IsEmpty && !_unprocessedEventsQueue.IsEmpty)
            {
                foreach (var analyticsEvent in _unprocessedEventsQueue)
                {
                    _eventsQueue.Enqueue(analyticsEvent);
                }
                return;
            }

            if (_eventsQueue.IsEmpty)
            {
                DisableTimer();
                return;
            }

            try
            {
                _isProcessing = true;
                var eventsCount = _eventsQueue.Count > Config.EventsPerWriteRequest
                    ? Config.EventsPerWriteRequest
                    : _eventsQueue.Count;
                var analyticsEvents = new IAnalyticsEvent[eventsCount];
                for (var i = 0; i < analyticsEvents.Length; i++)
                {
                    _eventsQueue.TryDequeue(out var analyticsEvent);
                    analyticsEvents[i] = analyticsEvent;
                }

                Action<IEnumerable<IAnalyticsEvent>> act = SendEventsToService;
                _dispatcher.Dispatch(act, new object[]{analyticsEvents});
            }
            finally
            {
                DisableTimer();
                _isProcessing = false;
            }
        }

        protected abstract void SendEventsToService(IEnumerable<IAnalyticsEvent> analyticsEvents);

        protected virtual void OnEventsSendFailed(IEnumerable<IAnalyticsEvent> analyticsEvents)
        {
            foreach (var analyticsEvent in analyticsEvents)
            {
                _unprocessedEventsQueue.Enqueue(analyticsEvent);
            }
        }

        public virtual void SaveUnprocessedEvents()
        {
            // TODO store unprocessed events in local file(s)
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            DisableTimer();
            try
            {
                _isProcessing = true;
                _eventsQueue.Clear();
                _unprocessedEventsQueue.Clear();
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
