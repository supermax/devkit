using System;

namespace DevKit.DIoC.Attributes
{
    // TODO review AttributeUsage params
    [AttributeUsage(AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
        // TODO use custom ID
        public override object TypeId { get; }
    }
}
