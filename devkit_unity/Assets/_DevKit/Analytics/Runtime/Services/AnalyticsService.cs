using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using DevKit.Analytics.Events.API;
using DevKit.Analytics.Services.API;

namespace DevKit.Analytics.Services
{
    public class AnalyticsService : IAnalyticsService, IDisposable
    {
        private readonly ConcurrentQueue<IAnalyticsEvent> _eventsQueue = new();

        private readonly ConcurrentQueue<IAnalyticsEvent> _unprocessedEventsQueue = new();

        private Timer _timer;

        private bool _isProcessing;

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

        public void Init()
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
            if (_eventsQueue.IsEmpty)
            {
                DisableTimer();
                return;
            }

            try
            {
                _isProcessing = true;
                _eventsQueue.TryDequeue(out var analyticsEvent);
                SendEventToService(analyticsEvent);
            }
            finally
            {
                DisableTimer();
                _isProcessing = false;
            }
        }

        protected virtual void SendEventToService(IAnalyticsEvent analyticsEvent)
        {
            // TODO
        }

        protected virtual void OnEventSendFailed(IAnalyticsEvent analyticsEvent)
        {
            _unprocessedEventsQueue.Enqueue(analyticsEvent);
        }

        public virtual void Reset()
        {
            throw new System.NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer?.Dispose();
            }
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
