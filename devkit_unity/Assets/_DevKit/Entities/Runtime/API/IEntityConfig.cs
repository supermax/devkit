using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DevKit.Entities.API
{
    public interface IEntityConfig : IDisposable
    {
        void Init(EntityPropertiesContainer propertyValues);

        /// <summary>
        /// Gets initial value for entity property
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <returns></returns>
        PropertyValueHolder GetPropertyInitialValue(string name);
    }
}
