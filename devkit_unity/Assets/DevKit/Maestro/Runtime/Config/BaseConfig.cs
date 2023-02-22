using System;
using System.Runtime.Serialization;

namespace DevKit.IOC.Config
{
    [Serializable]
    [DataContract]
    public abstract class BaseConfig
    {
        [DataMember(Name = "name")]
        public virtual string Name
        {
            get;
            set;
        }
    }
}
