using System;
using System.Collections.Generic;

namespace DevKit.Analytics.Events.API
{
    public interface IAnalyticsEvent
    {
        DateTime EventTime { get; }

        AnalyticsEventType EventType { get; }

        string EventName { get; }

        Dictionary<string, object> EventProperties { get; }
    }
}
