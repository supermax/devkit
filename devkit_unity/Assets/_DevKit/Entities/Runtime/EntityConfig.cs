using System;
using System.Collections.Generic;
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
        public string TypeId { get; private set;}

        /// <summary>
        /// Properties' values container
        /// </summary>
        public EntityPropertiesContainer Values { get; set; } = new();

        public Dictionary<string, List<string>> RelatedConfigs { get; private set; } = new Dictionary<string, List<string>>();

        /// <inheritdoc/>
        public void Init(IDictionary<string, PropertyValueHolder> propertyValues)
        {
            Values = (EntityPropertiesContainer)propertyValues;
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

        public List<string> GetRelatedConfigIds(string name)
        {
            if (!Values.ContainsKey(name))
            {
                name = name.ToJsonPropName();
            }
            
            if (!RelatedConfigs.ContainsKey(name))
            {
                return new List<string>();
            }
            return new List<string>(RelatedConfigs[name]);
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

        public IEntityConfig Copy()
        {
            var config = new EntityConfig
            {
                Id = Id,
                Version = Version,
                UpdateTime = UpdateTime,
                Name = Name,
                TypeId = TypeId,
                Values = new(Values),
                RelatedConfigs = new Dictionary<string, List<string>>(RelatedConfigs)
            };
            return config;
        }
    }
}
