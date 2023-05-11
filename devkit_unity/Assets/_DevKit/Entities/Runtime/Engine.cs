using DevKit.Entities.API;

namespace DevKit.Entities
{
    /// <summary>
    /// Base engine class
    /// </summary>
    public abstract class Engine : IEngine
    {
        /// <inheritdoc/>
        public IEngineConfig Config { get; private set; }

        public virtual void Init(IEngineConfig config)
        {
            config.Init();
            Config = config;
        }

        /// <inheritdoc/>
        public abstract T GetInstance<T>() where T : class, IEntity;

        /// <summary>
        /// Instantiates entity's instance based on given type <see cref="T"/>
        /// </summary>
        /// <remarks>If entity is registered as singleton, then first instance will be returned</remarks>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>Entity's instance</returns>
        protected abstract T Instantiate<T>() where T: class, IEntity;

        /// <inheritdoc/>
        public abstract void Register<TInterface, TImplementation>(bool singleton)
            where TInterface : class
            where TImplementation : class, TInterface;

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            Config = null;
        }
    }
}
