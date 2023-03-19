﻿using System;
using System.Runtime.CompilerServices;
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
        /// Gets Entity ID (normally will be same as class's name + hashcode)
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets Entity Type ID (normally will be same as class's name)
        /// </summary>
        string TypeId { get; }

        /// <summary>
        /// Gets Entity Config
        /// </summary>
        IEntityConfig Config { get; }

        /// <summary>
        /// Sets the boolean property value
        /// </summary>
        /// <param name="value">The boolean value</param>
        /// <param name="name">The name of the property</param>
        void SetPropertyValue(bool? value, [CallerMemberName] string name = "");

        /// <summary>
        /// Sets the numeric property value
        /// </summary>
        /// <param name="value">The numeric value</param>
        /// <param name="name">The name of the property</param>
        void SetPropertyValue(double? value, [CallerMemberName] string name = "");

        /// <summary>
        /// Set the textual property value
        /// </summary>
        /// <param name="value">The textual value</param>
        /// <param name="name">The name of the property</param>
        void SetPropertyValue(string value, [CallerMemberName] string name = "");

        /// <summary>
        /// Get the property value
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <returns></returns>
        PropertyValueHolder GetPropertyValue([CallerMemberName] string name = "");

        /// <summary>
        /// Applies numeric property modifier
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="modifier">The type of the modifier</param>
        /// <param name="value">The value to apply</param>
        /// <returns>Modified numeric value</returns>
        double? ApplyPropertyModifier(string name, PropertyModifierType modifier, double value);

        /// <summary>
        /// Resets property value
        /// </summary>
        /// <param name="name">The name of the property</param>
        void ResetPropertyValue([CallerMemberName] string name = "");

        /// <summary>
        /// Init this instance with values from the <see cref="config"/>
        /// </summary>
        /// <param name="config">The entity config instance</param>
        void Init(IEntityConfig config);
    }
}
