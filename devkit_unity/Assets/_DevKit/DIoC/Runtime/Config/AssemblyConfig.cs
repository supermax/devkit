using System;
using DevKit.Core.Extensions;

namespace DevKit.DIoC.Config
{
    [Serializable]
    public class AssemblyConfig : BaseConfig
    {
        public TypeConfig[] Types
        {
            get { return GetValue<TypeConfig[]>(); }
            set { SetValue(value); }
        }
    }
}
