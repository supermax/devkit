using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevKit.Entities.Runtime.API;

namespace DevKit.Entities.API
{
    /// <summary>
    /// Interface for base engine class
    /// </summary>
    public interface IEngine : IDisposable
    {
        /// <summary>
        /// Get existing entity instance in case it's registered as singleton or creates new entity instance and initializes with config values with context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetInstanceAsync<T>(string id, IEntityFactoryContext context) where T : class, IEntity;

        /// <summary>
        /// Get existing entity instance in case it's registered as singleton or creates new entity instance and initializes with config values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetInstanceAsync<T>(string id) where T : class, IEntity;

        /// <summary>
        /// Get existing entity instance in case it's registered as singleton or creates new entity instance and initializes with config values
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEntity> GetInstanceAsync(string id);

        /// <summary>
        /// Get entity config by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool TryGetEntityConfig(string id, out IEntityConfig entityConfig);

        /// <summary>
        /// Registers entity type and maps it to implementation
        /// </summary>
        /// <param name="entityFactory">Entity's Factory interface</param>
        /// <param name="singleton">If set `true` then the type will be mapped as singleton</param>
        /// <typeparam name="TInterface">Entity's interface</typeparam>
        /// <typeparam name="TImplementation">Entity's implementation</typeparam>
        void Register<TInterface, TImplementation>(bool singleton = false, IEntityFactory<TInterface> entityFactory = null)
            where TInterface : class, IEntity
            where TImplementation : class, TInterface;
        
        void AddEntityConfig(string id, IEntityConfig config);
        void AddEntityConfig<T>(Dictionary<string, T> configs) where T : class, IEntityConfig;
        Task<EntityCollection<string, T>> ResolveRelatedConfigsAsync<T>(string id, List<string> relatedConfigIds, IEntity entity) where T : class, IEntity;
    }
}
