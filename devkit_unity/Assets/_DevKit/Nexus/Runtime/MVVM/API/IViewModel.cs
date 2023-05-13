using System;
using DevKit.Core.Objects;
using DevKit.Core.Observables.API;

namespace DevKit.Nexus.MVVM.API
{
    /// <summary>
    /// Interface for base entity class
    /// </summary>
    public interface IViewModel
        : IObservableObject
            , IInitializable
            , IDataErrorInfo
    {
        /// <summary>
        /// Gets Entity ID (normally will be same as class's name + hashcode)
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets / Sets Entity's Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets Entity's Update Time (normally auto-updated on any property change)
        /// </summary>
        DateTime? UpdateTime { get; }
    }
}
