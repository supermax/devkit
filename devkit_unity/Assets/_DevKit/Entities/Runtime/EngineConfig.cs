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
        public virtual Dictionary<string, IEntityConfig> EntitiesConfig { get; set; } = new();

        public abstract void Init();

        public virtual void Reset()
        {
            foreach (var entityConfig in EntitiesConfig)
            {
                entityConfig.Value.Dispose();
            }
            EntitiesConfig.Clear();
        }

        public void Init(Dictionary<string, IEntityConfig> entitiesConfig)
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

        public IEntityConfig GetEntityConfig(string id)
        {
            return EntitiesConfig.TryGetValue(id, out var entityConfig) ? entityConfig : null;
        }

        public void AddEntityConfig(string id, IEntityConfig entityConfig)
        {
            EntitiesConfig[id] = entityConfig;
        }

        public void AddEntityConfig<T>(Dictionary<string, T> configs) where T : class, IEntityConfig
        {
            if (configs == null)
            {
                return;
            }
            foreach (var pair in configs)
            {
                AddEntityConfig(pair.Key, pair.Value);
            }
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
