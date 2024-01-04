using System;
using System.Runtime.CompilerServices;
using DevKit.Core.Observables;
using DevKit.Entities.API;

namespace DevKit.Entities
{
    /// <summary>
    /// Base entity class
    /// </summary>
    //[Serializable]
    public abstract class Entity : Observable, IEntity
    {
        protected string NameValue;

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual string Id { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual string TypeId { get; set; }

        /// <inheritdoc/>
        public virtual string Name
        {
            get { return NameValue; }
            set { SetPropertyValue(ref NameValue, value); }
        }

        /// <inheritdoc/>
        public virtual IEntityConfig Config { get; protected set; }

        /// <inheritdoc/>
        public virtual string Error { get; protected set; }

        /// <inheritdoc/>
        public virtual DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Default ctor
        /// </summary>
        protected Entity()
        {
            IsUpdateSuspended = true;
        }

        /// <summary>
        /// Ctor that accepts ID
        /// </summary>
        /// <param name="id">entity's id</param>
        protected Entity(string id) : this()
        {
            Id = id;
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

        /// <summary>
        /// Sets property value
        /// </summary>
        /// <param name="value">Current value</param>
        /// <param name="newValue">New value</param>
        /// <param name="firePropertyChangedEvent">If set 'true' then fires `PropertyChangedEvent'</param>
        /// <param name="name">Property name</param>
        /// <typeparam name="T">The type of value</typeparam>
        protected virtual void SetPropertyValue<T>(ref T value, T newValue, bool firePropertyChangedEvent = true, [CallerMemberName] string name = "")
        {
            if (Equals(value, newValue))
            {
                return;
            }

            var oldValue = value;
            value = newValue;
            if (name != nameof(UpdateTime))
            {
                UpdateTime = DateTime.UtcNow;
            }

            if (!firePropertyChangedEvent)
            {
                return;
            }
            InvokePropertyChanged(name, oldValue, newValue);
        }

        /// <inheritdoc/>
        public virtual T ApplyPropertyModifier<T>(string name, PropertyModifierType modifier, T value, bool firePropertyChangedEvent = true)
        {
            // TODO complete implementation

            // switch (modifier)
            // {
            //     case PropertyModifierType.Multiplication:
            //         SetPropertyValue(propValue.Number.Value*value, firePropertyChangedEvent, name);
            //         break;
            //
            //     case PropertyModifierType.Division:
            //         SetPropertyValue(propValue.Number.Value/value, firePropertyChangedEvent, name);
            //         break;
            //
            //     case PropertyModifierType.Addition:
            //         SetPropertyValue(propValue.Number.Value+value, firePropertyChangedEvent, name);
            //         break;
            //
            //     case PropertyModifierType.Subtraction:
            //         SetPropertyValue(propValue.Number.Value-value, firePropertyChangedEvent, name);
            //         break;
            //
            //     default:
            //         return propValue.Number;
            // }
            // return propValue.Number;
            return value;
        }

        /// <inheritdoc/>
        public abstract void Init(IEntityConfig config);

        public virtual void Init<T>(T instance) where T : IEntity
        {

        }

        public virtual void Init()
        {
            IsUpdateSuspended = false;
        }

        public virtual void Reset()
        {
            // TODO reset props / fields values
        }
    }
}
