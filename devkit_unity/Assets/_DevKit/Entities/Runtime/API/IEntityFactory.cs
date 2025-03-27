using System.Collections;
using System.Threading.Tasks;
using DevKit.Entities.API;
using UnityEngine;

namespace DevKit.Entities.Runtime.API
{
    public interface IEntityFactory<T> : IEntityFactory where T : class, IEntity
    {
        new Task<T> CreateAsync(string id, IEntityConfig entityConfig, IEntityFactoryContext context = null);
    }
    
    public interface IEntityFactory
    {
        //TODO remove this method after refactoring
        IEnumerator InitAsync(MonoBehaviour executeOn);

        void Initialize(IEngine engine, bool isSingleton);
        
        Task<object> CreateAsync(string id, IEntityConfig entityConfig, IEntityFactoryContext context = null);
    }
}
