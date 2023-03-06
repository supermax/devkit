using System;

namespace DevKit.Core.Observables.API
{
    /// <summary>
    /// Interface for observable class
    /// </summary>
    public interface IObservableObject<T> : IObservableObject where T : class
    {
        // TODO use WeakRefDelegate
        /// <summary>
        /// Event that is triggered by property setter
        /// </summary>
        event PropertyChangedEventHandler<T> PropertyChanged;

        /// <summary>
        ///
        /// </summary>
        /// <param name="observer">The subscriber</param>
        /// <returns>Current instance of <see cref="IObservable{T}"/></returns>
        IObservableObject<T> Subscribe(IObserver<T> observer);

        IObservableObject<T> Unsubscribe(IObserver<T> observer);

        IObservableObject<T> BeginUpdate();

        IObservableObject<T> EndUpdate();
    }

    public interface IObservableObject : IDisposable
    {

    }
}
