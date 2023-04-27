using DevKit.Core.Observables.API;

namespace DevKit.Entities.API
{
    /// <summary>
    /// Interface for entity collection
    /// </summary>
    public interface IEntityCollection<TKey, TValue>
        : IObservableCollection<TKey, TValue>
        where TValue
        : class, IEntity
    {

    }
}
