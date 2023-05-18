using DevKit.Core.Observables.API;
using DevKit.Nexus.Binding.API;
using DevKit.Nexus.Binding.Internals;

namespace DevKit.Nexus.Binding
{
    public class PropertyBinding : Binding
    {
        internal PropertyBinding(
            object source
            , string sourcePath
            , object target
            , string targetPath
            , BindingMode mode)
        : base(source, sourcePath, target, targetPath, mode)
        {

        }

        public override IBinding InitValues()
        {
            var sourceValue = SourceBindingPath.GetValue();
            TargetBindingPath.SetValue(sourceValue);
            return this;
        }

        public override IBinding Bind()
        {
            if (Mode == BindingMode.OneTime)
            {
                return this;
            }
            if (SourceBindingPath.Source is not IObservableObject sourceObservable)
            {
                return this;
            }
            sourceObservable.Subscribe(TargetBindingPath as PropertyBindingPath);

            if (Mode != BindingMode.TwoWay)
            {
                return this;
            }

            if (TargetBindingPath.Source is not IObservableObject targetObservable)
            {
                return this;
            }
            targetObservable.Subscribe(SourceBindingPath as PropertyBindingPath);
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
