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
    public abstract class EngineConfig : IEngineConfig, IDisposable
    {
        /// <summary>
        /// Contains config values for entities
        /// </summary>
        public Dictionary<Type, EntityConfig> EntitiesConfig { get; set; } = new();

        public abstract void Init();

        public virtual void Reset()
        {
            foreach (var entityConfig in EntitiesConfig)
            {
                entityConfig.Value.Dispose();
            }
            EntitiesConfig.Clear();
        }

        public void Init(Dictionary<Type, EntityConfig> entitiesConfig)
        {
            EntitiesConfig = entitiesConfig;
        }

        public PropertyValueHolder? GetPropertyInitialValue<T>(string name)
        {
            var type = typeof(T);
            var value = GetPropertyInitialValue(type, name);
            return value;
        }

        public PropertyValueHolder? GetPropertyInitialValue(Type type, string name)
        {
            type.ThrowIfNull(nameof(type));
            name.ThrowIfNull(nameof(name));

            var entityConfig = EntitiesConfig[type];
            if (!EntitiesConfig.ContainsKey(type))
            {
                return null;
            }
            var value = entityConfig.GetPropertyInitialValue(name);
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
            if (!EntitiesConfig.ContainsKey(type))
            {
                return null;
            }
            var entityConfig = EntitiesConfig[type];
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
