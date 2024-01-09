using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevKit.Analytics.Events.API;
using DevKit.Analytics.Services.Config;

namespace DevKit.Analytics.Services.API
{
    public interface IAnalyticsProvider : IDisposable
    {
        AnalyticsProviderConfig Config { get; set; }

        bool IsEnabled { get; }

        Task InitAsync();

        void SendEvents(IEnumerable<IAnalyticsEvent> analyticsEvents);
    }
}
