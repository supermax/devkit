using System;

namespace DevKit.Core.Observables.API
{
    /// <summary>
    /// Interface for observable class
    /// </summary>
    public interface IObservableObject : IDisposable
    {
        // TODO use WeakRefDelegate
        /// <summary>
        /// Event that is triggered by property setter
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///
        /// </summary>
        /// <param name="observer">The subscriber</param>
        /// <returns>Current instance of <see cref="IObservable{T}"/></returns>
        IObservableObject Subscribe(IObserver observer);

        IObservableObject Unsubscribe(IObserver observer);

        IObservableObject BeginUpdate();

        IObservableObject EndUpdate();
    }
}
