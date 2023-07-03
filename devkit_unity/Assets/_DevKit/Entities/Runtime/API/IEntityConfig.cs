using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DevKit.Entities.API
{
    public interface IEntityConfig : IDisposable, IConfiguration
    {
        void Init(EntityPropertiesContainer propertyValues);

        /// <summary>
        /// Gets value for entity's property
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <returns><see cref="PropertyValueHolder"/>Value holder</returns>
        PropertyValueHolder GetValue(string name);

        /// <summary>
        /// Gets value for entity's property
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="valueHolder">the value holder</param>
        /// <returns><see cref="PropertyValueHolder"/>Value holder</returns>
        PropertyValueHolder SetValue(string name, PropertyValueHolder valueHolder);
    }
}
