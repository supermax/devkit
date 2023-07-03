using System;
using DevKit.Core.Extensions;
using DevKit.Entities.API;

namespace DevKit.Entities
{
    /// <summary>
    /// Config container for entity
    /// </summary>
    [Serializable]
    public class EntityConfig : BaseConfiguration, IEntityConfig
    {
        /// <summary>
        /// Properties' values container
        /// </summary>
        public EntityPropertiesContainer Values { get; set; } = new();

        /// <inheritdoc/>
        public void Init(EntityPropertiesContainer propertyValues)
        {
            Values = propertyValues;
        }

        /// <inheritdoc/>
        public PropertyValueHolder GetValue(string name)
        {
            if (!Values.ContainsKey(name))
            {
                name = name.ToJsonPropName();
            }
            if (!Values.ContainsKey(name))
            {
                Values[name] = new PropertyValueHolder();
            }
            var value = Values[name];
            return value;
        }

        /// <inheritdoc/>
        public PropertyValueHolder SetValue(string name, PropertyValueHolder valueHolder)
        {
            if (!Values.ContainsKey(name))
            {
                name = name.ToJsonPropName();
            }
            Values.TryAdd(name, valueHolder);
            return valueHolder;
        }

        public void Dispose()
        {
            if (Values == null)
            {
                return;
            }

            Values.Clear();
            Values = null;
        }
    }
}
