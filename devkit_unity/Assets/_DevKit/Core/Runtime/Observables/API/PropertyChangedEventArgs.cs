namespace DevKit.Core.Observables.API
{
    public class PropertyChangedEventArgs<T> : BaseEventArgs where T : class
    {
        public IObservableObject<T> Source { get; }

        public string PropertyName { get; }

        public object PrevValue { get; }

        public object NewValue { get; }

        public PropertyChangedEventArgs() { }

        public PropertyChangedEventArgs(IObservableObject<T> source, string propertyName, object prevValue, object newValue)
        {
            Source = source;
            PropertyName = propertyName;
            PrevValue = prevValue;
            NewValue = newValue;
        }

        public static PropertyChangedEventArgs<T> Create(IObservableObject<T> source, string propertyName, object prevValue, object newValue)
        {
            var args = new PropertyChangedEventArgs<T>(source, propertyName, prevValue, newValue);
            return args;
        }
    }
}
