using System.Reflection;
using DevKit.Core.Observables.API;

namespace DevKit.Nexus.Binding.Internals
{
    public class PropertyBindingPath : BindingPath, IObserver
    {
        private PropertyInfo Property { get; set; }

        internal PropertyBindingPath(object source, PropertyInfo propertyInfo) : base(source)
        {
            Property = propertyInfo;
        }

        internal override void SetValue(object value)
        {
            Property.SetValue(Source, value);
        }

        internal override object GetValue()
        {
            var value = Property.GetValue(Source);
            return value;
        }

        public override string ToString()
        {
            return $"{nameof(Source)}: {Source}, {nameof(Property)}: {Property}";
        }

        public override void Dispose()
        {
            Property = null;
            base.Dispose();
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.IsHandled || args.Error != null)
            {
                return;
            }

            var value = GetValue();
            if (Equals(args.NewValue, value))
            {
                return;
            }

            SetValue(args.NewValue);
        }

        public void OnError(object sender, PropertyChangedEventArgs args)
        {
            Error = args.Error;
        }
    }

}
