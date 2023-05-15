using System;
using System.Reflection;
using DevKit.Core.Observables.API;

namespace DevKit.Nexus.Binding.Internals
{
    internal class CollectionBindingPath : ICollectionObserver
    {
        internal object Source { get; private set; }

        internal Exception Error { get; set; }

        internal CollectionBindingPath(object source)
        {
            Source = source;
        }

        public override string ToString()
        {
            return $"{nameof(Source)}: {Source}";
        }

        public void Dispose()
        {
            Source = null;
        }

        public void OnCollectionChanged(object sender, CollectionChangedEventArgs args)
        {
            if (args.IsHandled || args.Error != null)
            {
                return;
            }


        }

        public void OnError(object sender, CollectionChangedEventArgs args)
        {
            Error = args.Error;
        }
    }
}
