using System;
using DevKit.Core.Observables.API;

namespace DevKit.Entities.API
{
    /// <summary>
    /// Interface for base entity class
    /// </summary>
    public interface IEntity<T>
        : IObservableObject<T>
            , IInitializable
            , IDataErrorInfo
        where T : class
    {
        /// <summary>
        /// Entity ID (<see cref="Guid"/> format)
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Entity Type ID (<see cref="Guid"/> format)
        /// </summary>
        Guid TypeId { get; }

        /// <summary>
        /// Sets the boolean property value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void SetPropertyValue(string name, bool? value);

        /// <summary>
        /// Sets the numeric property value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void SetPropertyValue(string name, double? value);

        /// <summary>
        /// Set the textual property value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void SetPropertyValue(string name, string value);

        /// <summary>
        /// Get the property value
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        PropertyValueHolder GetPropertyValue(string name);

        /// <summary>
        /// Applies numeric property modifier
        /// </summary>
        /// <param name="name"></param>
        /// <param name="modifier"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        double? ApplyPropertyModifier(string name, PropertyModifierType modifier, double value);

        /// <summary>
        /// Resets property value
        /// </summary>
        /// <param name="name"></param>
        void ResetPropertyValue(string name);
    }
}
