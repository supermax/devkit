using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DevKit.Entities.API;

namespace DevKit.Entities
{
    /// <summary>
    /// Config container for entity
    /// </summary>
    [Serializable]
    [DataContract]
    public class EntityConfig : IEntityConfig
    {
        private IDictionary<string, PropertyValueHolder> _propertyValues;

        public void Init(IDictionary<string, PropertyValueHolder> propertyValues)
        {
            _propertyValues = propertyValues;
        }

        /// <summary>
        /// Gets initial value for entity property
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PropertyValueHolder GetPropertyInitialValue(string name)
        {
            var value = _propertyValues[name];
            return value;
        }

        public void Dispose()
        {
            if (_propertyValues == null)
            {
                return;
            }

            _propertyValues.Clear();
            _propertyValues = null;
        }
    }
}
