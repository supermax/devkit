using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DevKit.Core.Extensions;

namespace DevKit.Core.Config
{
    [Serializable]
    public abstract class BaseConfig : Dictionary<string, object>
    {
        public virtual string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected virtual T GetValue<T>([CallerMemberName] string propertyName = "")
        {
            propertyName = propertyName.ToJsonPropName();
            if (!ContainsKey(propertyName))
            {
                return default;
            }
            var value = (T)this[propertyName];
            return value;
        }

        protected virtual void SetValue<T>(T value, [CallerMemberName] string propertyName = "")
        {
            propertyName = propertyName.ToJsonPropName();
            this[propertyName] = value;
        }
    }
}
