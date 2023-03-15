using DevKit.Serialization.Json.API;

namespace DevKit.Serialization.Tests.Editor.Json.Fixtures
{
	[JsonDataContract]
    public class UserCurrencyObj : BaseDataObject
    {
        [JsonDataMember(Name = "money")]
        public int Money { get; set; }

        [JsonDataMember(Name = "currency")]
        public int Currency { get; set; }

        [JsonDataMember(Name = "lvl")]
        public int Level { get; set; }

        [JsonDataMember(Name = "xp")]
        public int Xp { get; set; }

        [JsonDataMember(Name = "fs")]
        public int FreeSpins { get; set; }
    }
}
