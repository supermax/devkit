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
        /// ID's value holder
        /// </summary>
        protected Guid IdValue;

        /// <summary>
        /// Type ID's value holder
        /// </summary>
        protected Guid TypeIdValue;

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember(Name = "id")]
        public virtual Guid Id
        {
            get { return IdValue; }
            set
            {
                IdValue = value;
                SetPropertyValue(nameof(Id), Id.ToString());
            }
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember(Name = "typeId")]
        public virtual Guid TypeId
        {
            get { return TypeId; }
            set
            {
                IdValue = value;
                SetPropertyValue(nameof(TypeId), TypeId.ToString());
            }
        }

        [IgnoreDataMember]
        public virtual string Error { get; protected set; }

        /// <summary>
        /// Property values container
        /// </summary>
        [DataMember(Name = "properties")]
        public Dictionary<string, PropertyValueHolder> PropertyValues { get; } = new ();

        /// <summary>
        /// Default ctor
        /// </summary>
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Ctor that accepts ID
        /// </summary>
        /// <param name="id"></param>
        protected Entity(Guid id)
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

        public void SetPropertyValue(string name, bool? value)
        {
            var holder = PropertyValues[name];
            var oldValue = holder.Bool;
            if (oldValue == value)
            {
                return;
            }
            holder.SetValue(value);
            InvokePropertyChanged(name, oldValue, value);
        }

        public virtual void SetPropertyValue(string name, double? value)
        {
            var holder = PropertyValues[name];
            var oldValue = holder.Number;
            if (Equals(oldValue, value))
            {
                return;
            }
            holder.SetValue(value);
            InvokePropertyChanged(name, oldValue, value);
        }

        public void SetPropertyValue(string name, string value)
        {
            var holder = PropertyValues[name];
            var oldValue = holder.Text;
            if (oldValue == value)
            {
                return;
            }
            holder.SetValue(value);
            InvokePropertyChanged(name, oldValue, value);
        }

        public PropertyValueHolder GetPropertyValue(string name)
        {
            if (!PropertyValues.ContainsKey(name))
            {
                PropertyValues[name] = new PropertyValueHolder();
            }
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
                case PropertyModifierType.Multiplication:
                    SetPropertyValue(name, propValue.Number.Value*value);
                    break;

                case PropertyModifierType.Division:
                    SetPropertyValue(name, propValue.Number.Value/value);
                    break;

                case PropertyModifierType.Addition:
                    SetPropertyValue(name, propValue.Number.Value+value);
                    break;

                case PropertyModifierType.Subtraction:
                    SetPropertyValue(name, propValue.Number.Value-value);
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
