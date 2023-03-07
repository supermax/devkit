using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DevKit.Core.Observables;
using DevKit.Entities.API;

namespace DevKit.Entities
{
    /// <summary>
    /// Collection of entities (based on <see cref="Dictionary{TKey,TValue}"/>)
    /// </summary>
    [Serializable]
    [DataContract]
    public class EntityCollection<TKey, TValue> : ObservableCollection<TKey, TValue>, IEntityCollection<TKey, TValue> where TValue : class, IEntity<TValue>
    {

    }
}
