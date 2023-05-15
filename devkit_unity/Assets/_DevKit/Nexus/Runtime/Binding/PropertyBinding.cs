using DevKit.Core.Observables.API;
using DevKit.Nexus.Binding.API;
using DevKit.Nexus.Binding.Internals;

namespace DevKit.Nexus.Binding
{
    public class PropertyBinding
    {
        public BindingMode Mode { get; }

        public object Source { get; }

        public string SourcePath { get; }

        private BindingPath SourceBindingPath { get; set; }

        public object Target { get; }

        public string TargetPath { get; }

        private BindingPath TargetBindingPath { get; set; }

        internal PropertyBinding(object source, string sourcePath, object target, string targetPath, BindingMode mode)
        {
            Source = source;
            SourcePath = sourcePath;

            Target = target;
            TargetPath = targetPath;

            Mode = mode;
        }

        public override string ToString()
        {
            var mode = Mode switch
                {
                    BindingMode.OneWay => "=>",
                    BindingMode.TwoWay => "<=>",
                    BindingMode.OneTime => "=1",
                    _ => ":"
                };
            return $"{SourceBindingPath} {mode} {TargetBindingPath}";
        }

        internal PropertyBinding SetSourceBindingPath(BindingPath path)
        {
            SourceBindingPath = path;
            return this;
        }

        internal PropertyBinding SetTargetBindingPath(BindingPath path)
        {
            TargetBindingPath = path;
            return this;
        }

        internal PropertyBinding InitValues()
        {
            var sourceValue = SourceBindingPath.GetPropertyValue();
            TargetBindingPath.SetPropertyValue(sourceValue);
            return this;
        }

        internal PropertyBinding Bind()
        {
            if (Mode == BindingMode.OneTime)
            {
                return this;
            }
            if (SourceBindingPath.Source is not IObservableObject sourceObservable)
            {
                return this;
            }
            sourceObservable.Subscribe(TargetBindingPath);

            if (Mode != BindingMode.TwoWay)
            {
                return this;
            }

            if (TargetBindingPath.Source is not IObservableObject targetObservable)
            {
                return this;
            }
            targetObservable.Subscribe(SourceBindingPath);
            return this;
        }
    }
}
