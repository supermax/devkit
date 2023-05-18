using DevKit.Core.Observables.API;

namespace DevKit.Nexus.Binding.Internals
{
    public class CollectionBindingPath : BindingPath, ICollectionObserver
    {
        internal CollectionBindingPath(object source) : base(source)
        {

        }

        public override string ToString()
        {
            return $"{nameof(Source)}: {Source}";
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
