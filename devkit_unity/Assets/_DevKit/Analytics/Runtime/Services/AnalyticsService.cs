using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DevKit.Analytics.Events.API;
using DevKit.Analytics.Services.API;
using DevKit.Analytics.Services.Config;
using DevKit.Core.Extensions;
using DevKit.Core.Threading;
using DevKit.Logging;
using DevKit.Logging.Extensions;

namespace DevKit.Analytics.Services
{
    public abstract class AnalyticsService : IAnalyticsService, IDisposable
    {
        protected readonly ConcurrentQueue<IAnalyticsEvent> EventsQueue = new();

        protected readonly ConcurrentQueue<IAnalyticsEvent> UnprocessedEventsQueue = new();

        private readonly List<IAnalyticsProvider> _providers = new();

        private readonly IThreadDispatcher _dispatcher;

        protected bool IsProcessing;

        public bool IsInitialized { get; protected set; }

        public bool IsInitializing { get; protected set; }

        public virtual AnalyticsServiceConfig Config { get; protected set; } = new();

        protected AnalyticsService(IThreadDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public virtual void SendEvent(IAnalyticsEvent analyticsEvent)
        {
            EventsQueue.Enqueue(analyticsEvent);
            ProcessEvents();
        }

        public virtual void SendEvents(IEnumerable<IAnalyticsEvent> analyticsEvents)
        {
            foreach (var analyticsEvent in analyticsEvents)
            {
                EventsQueue.Enqueue(analyticsEvent);
            }
            ProcessEvents();
        }

        protected virtual void ProcessEvents()
        {
            if (EventsQueue.IsEmpty
                && UnprocessedEventsQueue.IsEmpty
                || IsProcessing
                || !Config.IsOnlineModeEnabled)
            {
                return;
            }

            if (EventsQueue.IsEmpty && !UnprocessedEventsQueue.IsEmpty)
            {
                while (UnprocessedEventsQueue.TryDequeue(out var analyticsEvent))
                {
                    EventsQueue.Enqueue(analyticsEvent);
                }
            }

            if (EventsQueue.IsEmpty)
            {
                return;
            }

            try
            {
                IsProcessing = true;
                var eventsCount = EventsQueue.Count > Config.EventsPerWriteRequest
                    ? Config.EventsPerWriteRequest
                    : EventsQueue.Count;
                var analyticsEvents = new IAnalyticsEvent[eventsCount];
                for (var i = 0; i < analyticsEvents.Length; i++)
                {
                    EventsQueue.TryDequeue(out var analyticsEvent);
                    analyticsEvents[i] = analyticsEvent;
                }

                Action<IEnumerable<IAnalyticsEvent>> act = SendEventsToService;
                _dispatcher.Dispatch(act, new object[]{analyticsEvents}, DispatcherTaskPriority.Low);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        protected virtual void SendEventsToService(IEnumerable<IAnalyticsEvent> analyticsEvents)
        {
            if (!Config.IsOnlineModeEnabled)
            {
                return;
            }
            analyticsEvents.ThrowIfNull(nameof(analyticsEvents));

            foreach (var provider in _providers)
            {
                try
                {
                    provider.SendEvents(analyticsEvents);
                }
                catch (Exception e)
                {
                    this.LogError($"Error on sending {nameof(analyticsEvents)} to {provider}: {e}");
                }
            }
        }

        protected virtual void OnEventsSendFailed(IEnumerable<IAnalyticsEvent> analyticsEvents)
        {
            //TODO override in inheritor
            foreach (var analyticsEvent in analyticsEvents)
            {
                UnprocessedEventsQueue.Enqueue(analyticsEvent);
            }
        }

        public virtual void SaveUnprocessedEvents()
        {
            // TODO store unprocessed events in local file(s)
        }

        protected virtual async Task InitProviders()
        {
            foreach (var provider in _providers.ToArray())
            {
                try
                {
                    var providerName = provider.GetType().Name;
                    if (Config.ProvidersConfig.ContainsKey(providerName))
                    {
                        var providerConfig = Config.ProvidersConfig[providerName];
                        if (!providerConfig.IsEnabled)
                        {
                            provider.Dispose();
                            _providers.Remove(provider);
                            this.LogInfo($"The {provider} is disabled, removing it from the list");
                            continue;
                        }
                        provider.Config = providerConfig;
                    }

                    LogInfo($"Initialing {provider}");
                    await provider.InitAsync();
                }
                catch (Exception e)
                {
                    this.LogError($"Error on initialising {provider}: {e}");
                }
            }
        }

        protected void AddProviders(params IAnalyticsProvider[] providers)
        {
            providers.ThrowIfNullOrEmpty(nameof(providers));

            foreach (var provider in providers)
            {
                AddProvider(provider);
            }
        }

        protected void AddProvider(IAnalyticsProvider provider)
        {
            provider.ThrowIfNull(nameof(provider));

            _providers.Add(provider);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            try
            {
                IsProcessing = true;
                EventsQueue.Clear();
                UnprocessedEventsQueue.Clear();

                _providers.ForEach(provider =>
                {
                    provider.Dispose();
                    this.LogInfo($"Disposed {provider}");
                });
                _providers.Clear();
            }
            finally
            {
                IsProcessing = false;
                this.LogInfo("Disposed");
            }
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region Logging

        protected void LogInfo(string message, [CallerMemberName] string callerName = null)
        {
            if (!Config.IsLoggingEnabled)
            {
                return;
            }
            if (!Config.LogType.HasFlag(LogType.Info))
            {
                return;
            }

            LoggingExtensions.LogInfo(this, message, callerName);
        }

        protected void LogWarning(string message, [CallerMemberName] string callerName = null)
        {
            if (!Config.IsLoggingEnabled)
            {
                return;
            }
            if (!Config.LogType.HasFlag(LogType.Warning))
            {
                return;
            }

            LoggingExtensions.LogWarning(this, message, callerName);
        }

        protected void LogError(string message, [CallerMemberName] string callerName = null)
        {
            if (!Config.IsLoggingEnabled)
            {
                return;
            }
            if (!Config.LogType.HasFlag(LogType.Error))
            {
                return;
            }

            LoggingExtensions.LogError(this, message, callerName);
        }

        #endregion
    }
}
