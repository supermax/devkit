using System;
using System.Runtime.Serialization;

namespace DevKit.IOC.Config
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
