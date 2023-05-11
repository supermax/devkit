using System;
using System.Collections.Generic;

namespace DevKit.Analytics.Events.API
{
    public interface IAnalyticsEvent
    {
        string Namespace { get; }

        string EntityId { get; }

        string EntityType { get; }

        DateTime? EventTime { get; }

        AnalyticsEventType? EventType { get; }

        string EventName { get; }

        string SessionId { get; }

        public string Username { get; }

        public string DeviceUniqueId { get; }

        public string TimeZone { get; }

        Dictionary<string, object> EventProperties { get; }
    }
}
