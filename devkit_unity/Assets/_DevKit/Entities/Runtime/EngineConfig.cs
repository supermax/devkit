using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DevKit.Entities.API;

namespace DevKit.Entities
{
    /// <summary>
    /// Base config class
    /// </summary>
    [Serializable]
    [DataContract]
    public abstract class EngineConfig : IEngineConfig, IDisposable
    {
        /// <summary>
        /// Contains config values for entities
        /// </summary>
        protected IDictionary<Type, IEntityConfig> EntitiesConfig;

        public abstract void Init();

        public virtual void Reset()
        {
            foreach (var entityConfig in EntitiesConfig)
            {
                entityConfig.Value.Dispose();
            }
            EntitiesConfig.Clear();
        }

        public void Init(IDictionary<Type, IEntityConfig> entitiesConfig)
        {
            EntitiesConfig = entitiesConfig;
        }

        public PropertyValueHolder GetPropertyInitialValue<T>(string name)
        {
            var type = typeof(T);
            var value = GetPropertyInitialValue(type, name);
            return value;
        }

        public PropertyValueHolder GetPropertyInitialValue(Type type, string name)
        {
            var entityConfig = EntitiesConfig[type];
            var value = entityConfig.GetPropertyInitialValue(name);
            return value;
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
