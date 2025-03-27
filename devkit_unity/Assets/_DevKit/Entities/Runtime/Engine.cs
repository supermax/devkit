using System.Collections.Generic;
using System.Threading.Tasks;
using DevKit.Entities.API;
using DevKit.Entities.Runtime.API;

namespace DevKit.Entities
{
    /// <summary>
    /// Base engine class
    /// </summary>
    public abstract class Engine : IEngine
    {
        protected Dictionary<string, IEntityConfig> Configs { get; private set; } = new();

        /// <inheritdoc/>
        public abstract Task<T> GetInstanceAsync<T>(string id, IEntityFactoryContext context) where T : class, IEntity;

        /// <inheritdoc/>
        public abstract Task<T> GetInstanceAsync<T>(string id) where T : class, IEntity;

        /// <inheritdoc/>
        public abstract Task<IEntity> GetInstanceAsync(string id);

        /// <inheritdoc/>
        public abstract bool TryGetEntityConfig(string id, out IEntityConfig entityConfig);

        /// <inheritdoc/>
        public abstract void Register<TInterface, TImplementation>(bool singleton = false, IEntityFactory<TInterface> entityFactory = null)
            where TInterface : class, IEntity
            where TImplementation : class, TInterface;

        public abstract void AddEntityConfig(string id, IEntityConfig config);
        public abstract void AddEntityConfig<T>(Dictionary<string, T> configs) where T : class, IEntityConfig;
        public abstract Task<EntityCollection<string, T>> ResolveRelatedConfigsAsync<T>(string id, List<string> relatedConfigIds, IEntity entity) where T : class, IEntity;
        
        /// <inheritdoc/>
        public virtual void Dispose()
        {
            Configs.Clear();
            Configs = null;
        }
    }
}
