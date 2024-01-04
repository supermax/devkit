using System.Collections.Generic;
using DevKit.Serialization.Json.API;

namespace DevKit.Analytics.Services.Config
{
    [JsonDataContract]
    public class AnalyticsServiceConfig : BaseAnalyticsConfig
    {
        [JsonDataMember("enabled")]
        public override bool IsEnabled { get; set; } = true;

        [JsonDataMember("providersConfig")]
        public Dictionary<string, AnalyticsProviderConfig> ProvidersConfig { get; set; } = new();
    }
}
