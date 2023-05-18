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

        internal void SetPropertyValue(object value)
        {
            Property.SetValue(Source, value);
        }

        internal object GetPropertyValue()
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
            if (Property.Name != args.PropertyName)
            {
                return;
            }

            var value = GetPropertyValue();
            if (Equals(args.NewValue, value))
            {
                return;
            }

            SetPropertyValue(args.NewValue);
        }

        public void OnError(object sender, PropertyChangedEventArgs args)
        {
            Error = args.Error;
        }
    }

}
