using System;
using DevKit.Core.Config;

namespace DevKit.DIoC.Config
{
    [Serializable]
    public class TypeConfig : BaseConfig
    {
        public string SourceType
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string[] TypeMappings
        {
            get { return GetValue<string[]>(); }
            set { SetValue(value); }
        }

        public string[] TypeDependencies
        {
            get { return GetValue<string[]>(); }
            set { SetValue(value); }
        }

        public TypeInitTrigger InitTrigger
        {
            get { return GetValue<TypeInitTrigger>(); }
            set { SetValue(value); }
        }

        public bool IsSingleton
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
    }
}
