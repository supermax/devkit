using System;
using System.Collections.Generic;
using DevKit.Core.Extensions;

namespace DevKit.DIoC.Config
{
    [Serializable]
    public abstract class BaseConfig : Dictionary<string, object>
    {
        protected readonly string NameKey = nameof(Name).ToJsonPropName();

        public virtual string Name
        {
            get { return this[NameKey] as string; }
            set { this[NameKey] = value; }
        }
    }
}
