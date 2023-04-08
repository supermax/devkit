using System;

namespace DevKit.Entities.API
{
    /// <summary>
    /// Interface for base engine class
    /// </summary>
    public interface IEngine : IDisposable
    {
        /// <summary>
        /// Gets engine's config
        /// </summary>
        IEngineConfig Config { get; }

        /// <summary>
        /// Initialize engine with config
        /// </summary>
        /// <param name="config"></param>
        void Init(IEngineConfig config);

        /// <summary>
        /// Get existing entity instance in case it's registered as singleton or creates new entity instance and initializes with config values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetInstance<T>() where T : class, IEntity<T>;

        /// <summary>
        /// Registers entity type and maps it to implementation
        /// </summary>
        /// <param name="singleton">If set `true` then the type will be mapped as singleton</param>
        /// <typeparam name="TInterface">Entity's interface</typeparam>
        /// <typeparam name="TImplementation">Entity's implementation</typeparam>
        void Register<TInterface, TImplementation>(bool singleton)
            where TInterface : class
            where TImplementation : class, TInterface;
    }
}
