using System;
using DevKit.Serialization.Json.API;

namespace DevKit.DIoC.Editor.Config
{
    [Serializable]
    [JsonDataContract]
    public class AssemblyInfo
    {
        [JsonDataMember(Name = "name")]
        public string Name { get; set; }
    }
}
