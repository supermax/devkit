using System.Collections.Generic;
using DevKit.Analytics.Events.API;
using DevKit.Analytics.Services.Config;
using DevKit.Core.Objects;

namespace DevKit.Analytics.Services.API
{
    public interface IAnalyticsService
    {
        AnalyticsServiceConfig Config { get; }

        void SendEvent(IAnalyticsEvent analyticsEvent);

        void SendEvents(IEnumerable<IAnalyticsEvent> analyticsEvents);
    }
}
