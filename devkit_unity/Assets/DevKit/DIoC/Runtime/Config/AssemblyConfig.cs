using System;
using System.Runtime.Serialization;

namespace DevKit.DIoC.Config
{
    [Serializable]
    [DataContract]
    public class AssemblyConfig : BaseConfig
    {
        [DataMember(Name = "types")]
        public TypeConfig[] Types
        {
            get;
            set;
        }
    }
}
