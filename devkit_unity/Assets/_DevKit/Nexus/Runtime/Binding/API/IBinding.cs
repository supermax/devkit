using System;
using DevKit.Nexus.Binding.Internals;

namespace DevKit.Nexus.Binding.API
{
    public interface IBinding : IDisposable
    {
        IBinding SetSourceBindingPath(BindingPath path);

        IBinding SetTargetBindingPath(BindingPath path);

        IBinding InitValues();

        IBinding Bind();
    }
}
