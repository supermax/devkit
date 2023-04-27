using System;

namespace DevKit.Core.Observables.API
{
    public interface IObserver : IDisposable
    {
        void OnPropertyChanged(object sender, PropertyChangedEventArgs args);

        void OnError(object sender, PropertyChangedEventArgs args);
    }
}
