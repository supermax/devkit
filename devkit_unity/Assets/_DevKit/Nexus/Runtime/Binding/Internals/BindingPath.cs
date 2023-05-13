using System;
using System.Reflection;
using DevKit.Core.Observables.API;

namespace DevKit.Nexus.Binding.Internals
{
    internal class BindingPath : IObserver
    {
        internal object Source { get; set; }

        internal PropertyInfo Property { get; set; }

        internal Exception Error { get; set; }

        internal BindingPath(object source, PropertyInfo propertyInfo)
        {
            Source = source;
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

        public void Dispose()
        {
            Source = null;
            Property = null;
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
