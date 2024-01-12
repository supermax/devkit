using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DevKit.Core.Extensions;
using DevKit.Core.Observables;
using DevKit.Nexus.MVVM.API;

namespace DevKit.Nexus.MVVM
{
    /// <summary>
    /// Base entity class
    /// </summary>
    [Serializable]
    public abstract class BaseViewModel : ObservableComponent, IViewModel
    {
        protected string NameValue;

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual string Id { get; set; }

        /// <inheritdoc/>
        public virtual string Name
        {
            get { return NameValue; }
            set { SetPropertyValue(ref NameValue, value); }
        }

        /// <inheritdoc/>
        public virtual string Error { get; protected set; }

        /// <inheritdoc/>
        public virtual DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Default ctor
        /// </summary>
        protected BaseViewModel()
        {
            IsUpdateSuspended = true;
        }

        /// <summary>
        /// Validates property's value and returns the text with error or empty/null if value is valid
        /// </summary>
        /// <param name="propertyName">The name of the property</param>
        /// <returns><see cref="string"/> with validation text</returns>
        public virtual string Validate([NotNull] string propertyName)
        {
            return null;
        }

        /// <summary>
        /// Sets property value
        /// </summary>
        /// <param name="value">Current value</param>
        /// <param name="newValue">New value</param>
        /// <param name="firePropertyChangedEvent">If set 'true' then fires `PropertyChangedEvent'</param>
        /// <param name="propName">Property name</param>
        /// <typeparam name="T">The type of value</typeparam>
        protected virtual void SetPropertyValue<T>(
            ref T value
            , T newValue
            , bool firePropertyChangedEvent = true
            , [CallerMemberName] string propName = "")
        {
            propName.ThrowIfNullOrEmpty(nameof(propName));

            if (Equals(value, newValue))
            {
                return;
            }

            value = newValue;
            if (name != nameof(UpdateTime))
            {
                UpdateTime = DateTime.UtcNow;
            }

            if (!firePropertyChangedEvent)
            {
                return;
            }
            InvokePropertyChanged(propName, value, newValue);
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
