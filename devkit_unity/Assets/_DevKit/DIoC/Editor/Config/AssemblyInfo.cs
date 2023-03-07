using System;
using System.Runtime.Serialization;

namespace DevKit.DIoC.Editor.Config
{
    [Serializable]
    [DataContract]
    public class AssemblyInfo
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
