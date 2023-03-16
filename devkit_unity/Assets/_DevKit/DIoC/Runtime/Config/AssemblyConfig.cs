using System;
using System.Runtime.Serialization;

namespace DevKit.DIoC.Config
{
    [Serializable]
    public class AssemblyConfig : BaseConfig
    {
        public TypeConfig[] Types
        {
            get;
            set;
        }
    }
}
