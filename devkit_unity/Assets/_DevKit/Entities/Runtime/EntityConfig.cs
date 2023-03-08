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
        [DataMember(Name = "values")]
        public Dictionary<string, PropertyValueHolder> PropertyValues { get; set; } = new();

        public void Init(Dictionary<string, PropertyValueHolder> propertyValues)
        {
            PropertyValues = propertyValues;
        }

        /// <summary>
        /// Gets initial value for entity property
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PropertyValueHolder GetPropertyInitialValue(string name)
        {
            var value = PropertyValues[name];
            return value;
        }

        public void Dispose()
        {
            if (PropertyValues == null)
            {
                return;
            }

            PropertyValues.Clear();
            PropertyValues = null;
        }
    }
}
