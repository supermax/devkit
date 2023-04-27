using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using DevKit.Analytics.Events.API;
using DevKit.Analytics.Services.API;

namespace DevKit.Analytics.Services
{
    public abstract class AnalyticsService : IAnalyticsService, IDisposable
    {
        private readonly ConcurrentQueue<IAnalyticsEvent> _eventsQueue = new();

        private readonly ConcurrentQueue<IAnalyticsEvent> _unprocessedEventsQueue = new();

        private Timer _timer;

        private bool _isProcessing;

        protected bool IsInitialized { get; set; }

        public AnalyticsServiceConfig Config { get; } = new();

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

        public virtual void Init()
        {

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
                var analyticsEvents = new IAnalyticsEvent[Config.EventsPerWriteRequest];
                for (var i = 0; i < analyticsEvents.Length; i++)
                {
                    _eventsQueue.TryDequeue(out var analyticsEvent);
                    analyticsEvents[i] = analyticsEvent;
                }
                SendEventsToService(analyticsEvents);
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

        public virtual void Reset()
        {
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

        public virtual void SaveUnprocessedEvents()
        {
            // TODO store unprocessed events in local file(s)
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Reset();
            }
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
