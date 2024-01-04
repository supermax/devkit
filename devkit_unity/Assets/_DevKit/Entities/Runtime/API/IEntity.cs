using System;
using System.Runtime.CompilerServices;
using DevKit.Core.Objects;
using DevKit.Core.Observables.API;

namespace DevKit.Entities.API
{
    /// <summary>
    /// Interface for base entity class
    /// </summary>
    public interface IEntity
        : IObservableObject
            , IInitializable
            , IDataErrorInfo
    {
        /// <summary>
        /// Gets Entity ID (normally will be same as class's name + hashcode)
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Gets Entity Type ID (normally will be same as class's name)
        /// </summary>
        string TypeId { get; set; }

        /// <summary>
        /// Gets / Sets Entity's Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets Entity's Update Time (normally auto-updated on any property change)
        /// </summary>
        DateTime? UpdateTime { get; }

        /// <summary>
        /// Gets Entity Config
        /// </summary>
        IEntityConfig Config { get; }

        /// <summary>
        /// Applies numeric property modifier
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="modifier">The type of the modifier</param>
        /// <param name="value">The value to apply</param>
        /// <param name="firePropertyChangedEvent">If set 'true' then fires `PropertyChangedEvent'</param>
        /// <returns>Modified numeric value</returns>
        T ApplyPropertyModifier<T>(string name, PropertyModifierType modifier, T value, bool firePropertyChangedEvent = true);

        /// <summary>
        /// Init this instance with values from the <see cref="config"/>
        /// </summary>
        /// <param name="config">The entity config instance</param>
        void Init(IEntityConfig config);

        /// <summary>
        /// Init this instance with values from the <see cref="instance"/>
        /// </summary>
        /// <param name="instance">The entity instance</param>
        /// <typeparam name="T">Type of entity</typeparam>
        void Init<T>(T instance) where T : IEntity;
    }
}
