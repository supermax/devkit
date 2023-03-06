using System;

namespace DevKit.Core.Observables.API
{
    public interface IObserver<T> : IObserver  where T : class
    {
        void OnPropertyChanged(IObservableObject<T> sender, PropertyChangedEventArgs<T> args);

        void OnError(IObservableObject<T> sender, PropertyChangedEventArgs<T> args);
    }

    public interface IObserver : IDisposable
    {

    }
}
