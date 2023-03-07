using System;
using System.Runtime.Serialization;

namespace DevKit.DIoC.Config
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
