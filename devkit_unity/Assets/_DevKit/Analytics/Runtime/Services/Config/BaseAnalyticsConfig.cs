using DevKit.Core.Config;
using DevKit.Logging;
using DevKit.Serialization.Json.API;

namespace DevKit.Analytics.Services.Config
{
    // TODO init config values from server or from local file
    [JsonDataContract]
    public abstract class BaseAnalyticsConfig : BaseConfig
    {
        [JsonDataMember("enabled")]
        public virtual bool IsEnabled { get; set; }

        [JsonDataMember("eventsRecoveryEnabled")]
        public bool IsEventsRecoveryEnabled { get; set; }

        [JsonDataMember("onlineModeEnabled")]
        public virtual bool IsOnlineModeEnabled { get; set; }

        [JsonDataMember("eventsPerWriteRequest")]
        public virtual int EventsPerWriteRequest { get; set; }

        [JsonDataMember("loggingEnabled")]
        public virtual bool IsLoggingEnabled { get; set; }

        [JsonDataMember("logType")]
        public virtual LogType LogType { get; set; }
    }
}
