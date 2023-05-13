using System;

namespace DevKit.Nexus.Binding
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Property |  AttributeTargets.Method)]
    public class BindingAttribute : Attribute
    {

    }
}
