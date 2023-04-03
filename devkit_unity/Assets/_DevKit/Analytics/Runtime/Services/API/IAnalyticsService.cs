using System.Collections.Generic;
using DevKit.Analytics.Events.API;
using DevKit.Core.Objects;

namespace DevKit.Analytics.Services.API
{
    public interface IAnalyticsService : IInitializable
    {
        AnalyticsServiceConfig Config { get; }

        void SendEvent(IAnalyticsEvent analyticsEvent);

        void SendEvents(IEnumerable<IAnalyticsEvent> analyticsEvents);
    }
}
