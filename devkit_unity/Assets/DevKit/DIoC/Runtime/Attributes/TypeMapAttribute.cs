using System;
using DevKit.DIoC.Config;

namespace DevKit.DIoC.Attributes
{
    // TODO review AttributeUsage params
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class TypeMapAttribute : Attribute
    {
        public bool IsSingleton { get; set; }

        public TypeInitTrigger InitTrigger { get; set; }

        public Type[] MapTypes { get; set; }
    }
}
