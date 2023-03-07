using System;
using System.Collections.Generic;

namespace DevKit.Entities.API
{
    public interface IEntityConfig : IDisposable
    {
        void Init(Dictionary<string, PropertyValueHolder> propertyValues);

        /// <summary>
        /// Gets initial value for entity property
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        PropertyValueHolder GetPropertyInitialValue(string name);
    }
}
