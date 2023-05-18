using System;

namespace DevKit.Nexus.Binding.Internals
{
    public abstract class BindingPath
    {
        internal object Source { get; private set; }

        internal Exception Error { get; set; }

        protected BindingPath(object source)
        {
            Source = source;
        }

        public override string ToString()
        {
            return $"{nameof(Source)}: {Source}";
        }

        public virtual void Dispose()
        {
            Source = null;
        }
    }
}
