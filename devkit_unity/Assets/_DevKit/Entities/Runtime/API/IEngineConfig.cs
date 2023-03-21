using System;
using System.Collections.Generic;

namespace DevKit.Entities.API
{
    /// <summary>
    /// Interface for base entity config
    /// </summary>
    public interface IEngineConfig : IInitializable
    {
        /// <summary>
        /// Initialize config values
        /// </summary>
        /// <param name="entitiesConfig"></param>
        void Init(Dictionary<string, EntityConfig> entitiesConfig);

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
