using System;

namespace DevKit.Nexus.Binding.Internals
{
    public abstract class BindingPath
    {
        protected internal object Source { get; set; }

        protected internal Exception Error { get; set; }

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

        internal abstract object GetValue();

        internal abstract void SetValue(object value);
    }
}
