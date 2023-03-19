using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DevKit.Core.Extensions;
using DevKit.Core.Observables;
using DevKit.Entities.API;

namespace DevKit.Entities
{
    /// <summary>
    /// Base entity class
    /// </summary>
    [Serializable]
    public abstract class Entity<T> : Observable<T>, IEntity<T>
        where T : class
    {
        /// <summary>
        /// ID's value holder
        /// </summary>
        protected string IdValue;

        /// <summary>
        /// Type ID's value holder
        /// </summary>
        protected string TypeIdValue;

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual string Id
        {
            get { return IdValue; }
            set
            {
                IdValue = value;
                SetPropertyValue(value);
            }
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual string TypeId
        {
            get { return TypeIdValue; }
            set
            {
                TypeIdValue = value;
                SetPropertyValue(value);
            }
        }

        /// <inheritdoc/>
        public IEntityConfig Config { get; protected set; }

        public virtual string Error { get; protected set; }

        /// <summary>
        /// Property values container
        /// </summary>
        public Dictionary<string, PropertyValueHolder> PropertyValues { get; } = new ();

        /// <summary>
        /// Default ctor
        /// </summary>
        protected Entity()
        {
        }

        /// <summary>
        /// Ctor that accepts ID
        /// </summary>
        /// <param name="id">entity's id</param>
        protected Entity(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Property's indexer
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// /// <returns><see cref="PropertyValueHolder"/> with property values</returns>
        public virtual PropertyValueHolder this[string propertyName]
        {
            get
            {
                return PropertyValues[propertyName];
            }
        }

        /// <summary>
        /// Validates property's value and returns the text with error or empty/null if value is valid
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <returns><see cref="string"/> with validation text</returns>
        public virtual string Validate(string propertyName)
        {
            return null;
        }

        /// <inheritdoc/>
        public void SetPropertyValue(bool? value, [CallerMemberName] string name = "")
        {
            var holder = GetValueHolder(name);
            var oldValue = holder.Bool;
            if (oldValue == value)
            {
                return;
            }
            holder.SetValue(value);
            InvokePropertyChanged(name, oldValue, value);
        }

        /// <inheritdoc/>
        public virtual void SetPropertyValue(double? value, [CallerMemberName] string name = "")
        {
            var holder = GetValueHolder(name);
            var oldValue = holder.Number;
            if (Equals(oldValue, value))
            {
                return;
            }
            holder.SetValue(value);
            InvokePropertyChanged(name, oldValue, value);
        }

        /// <inheritdoc/>
        public void SetPropertyValue(string value, [CallerMemberName] string name = "")
        {
            var holder = GetValueHolder(name);
            var oldValue = holder.Text;
            if (oldValue == value)
            {
                return;
            }
            holder.SetValue(value);
            InvokePropertyChanged(name, oldValue, value);
        }

        protected PropertyValueHolder GetValueHolder(string name)
        {
            if (!PropertyValues.ContainsKey(name))
            {
                name = name.ToJsonPropName();
            }
            if (!PropertyValues.ContainsKey(name))
            {
                PropertyValues[name] = new PropertyValueHolder();
            }
            var holder = PropertyValues[name];
            return holder;
        }

        /// <inheritdoc/>
        public PropertyValueHolder GetPropertyValue([CallerMemberName] string name = "")
        {
            if (!PropertyValues.ContainsKey(name))
            {
                name = name.ToJsonPropName();
            }
            if (!PropertyValues.ContainsKey(name))
            {
                PropertyValues[name] = new PropertyValueHolder();
            }
            var holder = PropertyValues[name];
            return holder;
        }

        /// <inheritdoc/>
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
                case PropertyModifierType.Multiplication:
                    SetPropertyValue(propValue.Number.Value*value, name);
                    break;

                case PropertyModifierType.Division:
                    SetPropertyValue(propValue.Number.Value/value, name);
                    break;

                case PropertyModifierType.Addition:
                    SetPropertyValue(propValue.Number.Value+value, name);
                    break;

                case PropertyModifierType.Subtraction:
                    SetPropertyValue(propValue.Number.Value-value, name);
                    break;

                default:
                    return propValue.Number;
            }
            return propValue.Number;
        }

        /// <inheritdoc/>
        public virtual void ResetPropertyValue([CallerMemberName] string name = "")
        {
            name = name.ToJsonPropName();
            PropertyValues.Remove(name);

            // TODO restore initial value from config
            InvokePropertyChanged(name, null, null);
        }

        /// <inheritdoc/>
        public abstract void Init(IEntityConfig config);

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
