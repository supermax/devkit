using DevKit.Serialization.Json.API;

namespace DevKit.Analytics.Services.Config
{
    [JsonDataContract]
    public class AnalyticsProviderConfig : BaseAnalyticsConfig
    {
        [JsonDataMember("apiKey")]
        public string ApiKey { get; set; }
    }
}
