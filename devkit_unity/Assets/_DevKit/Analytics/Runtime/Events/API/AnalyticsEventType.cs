using DevKit.Serialization.Json.API;

namespace DevKit.Analytics.Events.API
{
    [JsonDataContract]
    public enum AnalyticsEventType
    {
        [JsonDataMember("other")]
        Other = 0,

        [JsonDataMember("ui")]
        UI,

        [JsonDataMember("gameplay")]
        Gameplay,

        [JsonDataMember("economy")]
        Economy,

        [JsonDataMember("playerData")]
        PlayerData,

        [JsonDataMember("tech")]
        Technical,
        
        [JsonDataMember("popup")]
        Popup
    }
}
