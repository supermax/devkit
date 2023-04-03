using System;
using System.Collections.Generic;
using DevKit.Analytics.Events.API;
using DevKit.Core.Extensions;
using DevKit.Serialization.Json.API;
using UnityEngine.Device;

namespace DevKit.Analytics.Events
{
    [Serializable]
    [JsonDataContract]
    public class AnalyticsEvent : IAnalyticsEvent
    {
        [JsonDataMember(Name = "sid")]
        public string SessionId { get; set; }

        [JsonDataMember(Name = "username")]
        public string Username { get; set; }

        [JsonDataMember(Name = "duid")]
        public string DeviceUniqueId { get; set; }

        [JsonDataMember(Name = "tz")]
        public string TimeZone { get; set; }

        [JsonDataMember(Name = "namespace")]
        public string Namespace { get; set; }

        [JsonDataMember(Name = "entityId")]
        public string EntityId { get; set; }

        [JsonDataMember(Name = "entityType")]
        public string EntityType { get; set; }

        [JsonDataMember("eventTime")]
        public DateTime EventTime { get; set; }

        [JsonDataMember("eventType")]
        public AnalyticsEventType EventType { get; set; }

        [JsonDataMember("eventName")]
        public string EventName { get; set; }

        [JsonDataMember("eventProps")]
        public Dictionary<string, object> EventProperties { get; set; }

        public AnalyticsEvent() { }

        public AnalyticsEvent(AnalyticsEventType eventType, string eventName, Dictionary<string, object> eventValues)
        {
            EventTime = DateTime.UtcNow;
            TimeZone = System.TimeZone.CurrentTimeZone.Invariant();
            DeviceUniqueId = SystemInfo.deviceUniqueIdentifier;
            EventType = eventType;
            EventName = eventName;
            EventProperties = eventValues;
        }
    }
}
