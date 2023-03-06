using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DevKit.Core.Observables;
using DevKit.Core.Observables.API;
using DevKit.Entities.API;

namespace DevKit.Entities
{
    /// <summary>
    /// Base entity class
    /// </summary>
    [Serializable]
    [DataContract]
    public abstract class Entity<T> : Observable<T>, IEntity<T>
        where T : class
    {
        /// <summary>
        /// Property values container
        /// </summary>
        public Dictionary<string, PropertyValueHolder> PropertyValues { get; } = new ();

        public void SetPropertyValue(string name, bool? value)
        {
            var holder = PropertyValues[name];
            var oldValue = holder.Bool;
            holder.SetValue(value);
            InvokePropertyChanged(name, oldValue, value);
        }

        public virtual void SetPropertyValue(string name, double? value)
        {
            var holder = PropertyValues[name];
            var oldValue = holder.Number;
            holder.SetValue(value);
            InvokePropertyChanged(name, oldValue, value);
        }

        public void SetPropertyValue(string name, string value)
        {
            var holder = PropertyValues[name];
            var oldValue = holder.Text;
            holder.SetValue(value);
            InvokePropertyChanged(name, oldValue, value);
        }

        public PropertyValueHolder GetPropertyValue(string name)
        {
            var holder = PropertyValues[name];
            return holder;
        }

        public virtual double? ApplyPropertyModifier(string name, PropertyModifierType modifier, double value)
        {
            // TODO implement each modifier operation (see "Multiply" as sample)
            // TODO validate "value" param and its operation result

            var propValue = GetPropertyValue(name);
            if (!propValue.Number.HasValue)
            {
                return propValue.Number;
            }

            switch (modifier)
            {
                case PropertyModifierType.Multiply:
                    propValue.SetValue(propValue.Number.Value*value);
                    break;

                case PropertyModifierType.Devide:
                    propValue.SetValue(propValue.Number.Value/value);
                    break;

                case PropertyModifierType.Add:
                    propValue.SetValue(propValue.Number.Value+value);
                    break;

                case PropertyModifierType.Substruct:
                    propValue.SetValue(propValue.Number.Value-value);
                    break;

                default:
                    return propValue.Number;
            }
            return propValue.Number;
        }

        public virtual void ResetPropertyValue(string name)
        {
            PropertyValues.Remove(name);

            // TODO restore initial value from config
            InvokePropertyChanged(name, null, null);
        }

        public virtual void Init()
        {
            // TODO
        }

        public virtual void Reset()
        {
            // TODO
            PropertyValues.Clear();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing)
            {
                return;
            }
            Reset();
        }
    }
}
