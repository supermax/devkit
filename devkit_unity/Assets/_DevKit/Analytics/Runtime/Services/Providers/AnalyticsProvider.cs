using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DevKit.Analytics.Events.API;
using DevKit.Analytics.Services.API;
using DevKit.Analytics.Services.Config;
using DevKit.Logging;
using DevKit.Logging.Extensions;

namespace DevKit.Analytics.Services.Providers
{
    public abstract class AnalyticsProvider : IAnalyticsProvider
    {
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        public AnalyticsProviderConfig Config { get; set; } = new();

        public virtual bool IsEnabled
        {
            get { return Config.IsEnabled; }
            set { Config.IsEnabled = value; }
        }

        public abstract Task InitAsync();

        public abstract void SendEvents(IEnumerable<IAnalyticsEvent> analyticsEvents);

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
