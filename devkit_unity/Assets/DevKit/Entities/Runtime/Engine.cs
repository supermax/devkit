using DevKit.Entities.API;

namespace DevKit.Entities
{
    /// <summary>
    /// Base engine class
    /// </summary>
    public abstract class Engine : IEngine
    {
        protected IEngineConfig Config;

        public void Init(IEngineConfig config)
        {
            Config = config;
        }

        /// <summary>
        /// Create entity instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract T Create<T>() where T : class, IEntity<T>;

        public abstract TInterface Register<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        public virtual void Dispose()
        {
            Config = null;
        }
    }
}
