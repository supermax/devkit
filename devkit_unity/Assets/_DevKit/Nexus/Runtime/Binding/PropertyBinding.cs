using DevKit.Core.Observables.API;
using DevKit.Nexus.Binding.API;
using DevKit.Nexus.Binding.Internals;

namespace DevKit.Nexus.Binding
{
    public class PropertyBinding : Binding
    {
        private BindingPath SourceBindingPath { get; set; }

        private BindingPath TargetBindingPath { get; set; }

        internal PropertyBinding(
            object source
            , string sourcePath
            , object target
            , string targetPath
            , BindingMode mode)
        : base(source, sourcePath, target, targetPath, mode)
        {

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (SourceBindingPath != null)
                {
                    SourceBindingPath.Dispose();
                    SourceBindingPath = null;
                }
                if (TargetBindingPath != null)
                {
                    TargetBindingPath?.Dispose();
                    TargetBindingPath = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
