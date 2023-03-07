using System;

namespace DevKit.Entities.API
{
    /// <summary>
    /// Interface for base engine class
    /// </summary>
    public interface IEngine : IDisposable
    {
        /// <summary>
        /// Initialize engine with config
        /// </summary>
        /// <param name="config"></param>
        void Init(IEngineConfig config);

        /// <summary>
        /// Creates new entity instance and initializes with config values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Create<T>() where T : class, IEntity<T>;

        TInterface Register<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;
    }
}
