using DevKit.Serialization.Json.API;

namespace DevKit.Serialization.Tests.Editor.Json.Fixtures
{
    [JsonDataContract]
    public abstract class BaseDataObject
    {
        [JsonDataMember(Name = "error")]
        public string Error { get; set; }
    }
}
