using DevKit.Serialization.Json.API;

namespace DevKit.Serialization.Tests.Editor.Json.Fixtures
{
    [JsonDataContract]
    public abstract class Person
    {
        [JsonDataMember("firstName")] public string Name { get; set; }

        [JsonDataMember("lastName")] public string Surname { get; set; }

        [JsonDataMember("age")] public int Age { get; set; }

        [JsonDataMember("numbers")] public int[] Numbers { get; set; }
    }
}
