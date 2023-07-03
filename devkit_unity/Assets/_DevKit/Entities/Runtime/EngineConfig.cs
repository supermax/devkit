using System;
using System.Collections.Generic;
using DevKit.Core.Extensions;
using DevKit.Entities.API;

namespace DevKit.Entities
{
    /// <summary>
    /// Base config class
    /// </summary>
    [Serializable]
    public abstract class EngineConfig : BaseConfiguration, IEngineConfig, IDisposable
    {
        /// <summary>
        /// Contains config values for entities
        /// </summary>
        public virtual Dictionary<string, EntityConfig> EntitiesConfig { get; set; } = new();

        public abstract void Init();

        public virtual void Reset()
        {
            foreach (var entityConfig in EntitiesConfig)
            {
                entityConfig.Value.Dispose();
            }
            EntitiesConfig.Clear();
        }

        public void Init(Dictionary<string, EntityConfig> entitiesConfig)
        {
            EntitiesConfig = entitiesConfig;
        }

        public PropertyValueHolder GetValue<T>(string name)
        {
            var type = typeof(T);
            var value = GetValue(type, name);
            return value;
        }

        public PropertyValueHolder GetValue(Type type, string name)
        {
            type.ThrowIfNull(nameof(type));
            name.ThrowIfNull(nameof(name));

            var typeName = type.Name.ToJsonPropName();
            var entityConfig = EntitiesConfig[typeName];
            if (!EntitiesConfig.ContainsKey(typeName))
            {
                return null;
            }
            var value = entityConfig.GetValue(name);
            return value;
        }

        public IEntityConfig GetEntityConfig<T>()
        {
            var type = typeof (T);
            return GetEntityConfig(type);
        }

        public IEntityConfig GetEntityConfig(Type type)
        {
            type.ThrowIfNull(nameof(type));

            var typeName = type.Name.ToJsonPropName();
            if (!EntitiesConfig.ContainsKey(typeName))
            {
                return null;
            }
            var entityConfig = EntitiesConfig[typeName];
            return entityConfig;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            Reset();
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
