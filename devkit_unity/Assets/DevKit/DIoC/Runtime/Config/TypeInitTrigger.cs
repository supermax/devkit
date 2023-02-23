using System;
using System.Runtime.Serialization;

namespace DevKit.DIoC.Config
{
    [Serializable]
    [DataContract]
    public enum TypeInitTrigger
    {
        [EnumMember(Value = "onDemand")]
        OnDemand,

        [EnumMember(Value = "onMapping")]
        OnMapping
    }
}
