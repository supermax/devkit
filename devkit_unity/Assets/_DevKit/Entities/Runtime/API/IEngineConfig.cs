using System;
using System.Collections.Generic;
using DevKit.Core.Objects;

namespace DevKit.Entities.API
{
    /// <summary>
    /// Interface for base entity config
    /// </summary>
    public interface IEngineConfig : IInitializable
    {
        IDictionary<string, EntityConfig> EntitiesConfig { get; }

        /// <summary>
        /// Initialize config values
        /// </summary>
        /// <param name="entitiesConfig"></param>
        void Init(IDictionary<string, EntityConfig> entitiesConfig);

        /// <summary>
        /// Gets initial value for given property
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        PropertyValueHolder GetPropertyInitialValue<T>(string name);

        /// <summary>
        /// Gets initial value for given property
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        PropertyValueHolder GetPropertyInitialValue(Type type, string name);

        IEntityConfig GetEntityConfig<T>();

        IEntityConfig GetEntityConfig(Type type);
    }
}
