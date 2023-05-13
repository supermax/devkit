using DevKit.Core.Observables.API;
using DevKit.Nexus.Binding.API;
using DevKit.Nexus.Binding.Internals;

namespace DevKit.Nexus.Binding
{
    public class Binding
    {
        public BindingMode Mode { get; }

        public object Source { get; }

        public string SourcePath { get; }

        private BindingPath SourceBindingPath { get; set; }

        public object Target { get; }

        public string TargetPath { get; }

        private BindingPath TargetBindingPath { get; set; }

        internal Binding(object source, string sourcePath, object target, string targetPath, BindingMode mode)
        {
            Source = source;
            SourcePath = sourcePath;

            Target = target;
            TargetPath = targetPath;

            Mode = mode;
        }

        internal Binding SetSourceBindingPath(BindingPath path)
        {
            SourceBindingPath = path;
            return this;
        }

        internal Binding SetTargetBindingPath(BindingPath path)
        {
            TargetBindingPath = path;
            return this;
        }

        internal Binding InitValues()
        {
            var sourceValue = SourceBindingPath.GetPropertyValue();
            TargetBindingPath.SetPropertyValue(sourceValue);
            return this;
        }

        internal Binding Bind()
        {
            var sourceObservable = SourceBindingPath.Source as IObservableObject;
            if (sourceObservable != null)
            {
                return this;
            }
            //sourceObservable.
            return this;
        }
    }
}
