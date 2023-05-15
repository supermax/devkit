using System;

namespace DevKit.Core.Observables.API
{
    public interface ICollectionObserver : IDisposable
    {
        void OnCollectionChanged(object sender, CollectionChangedEventArgs args);

        void OnError(object sender, CollectionChangedEventArgs args);
    }
}
