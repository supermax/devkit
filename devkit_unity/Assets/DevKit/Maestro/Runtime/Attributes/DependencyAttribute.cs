using System;

namespace DevKit.IOC.Attributes
{
    // TODO review AttributeUsage params
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class DependencyAttribute : Attribute
    {
        public Type[] Dependencies { get; set; }
    }
}
