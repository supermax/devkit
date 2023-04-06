using DevKit.Serialization.Json.API;

namespace DevKit.Entities.Demo.Characters.Players.API
{
    [JsonDataContract]
    public enum LoginType
    {
        [JsonDataMember("guest")]
        Guest = 0,

        [JsonDataMember("social")]
        Social,

        [JsonDataMember("email")]
        Email,

        [JsonDataMember("platform")]
        Platform
    }
}
