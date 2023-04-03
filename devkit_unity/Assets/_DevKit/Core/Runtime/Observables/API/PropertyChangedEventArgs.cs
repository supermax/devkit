namespace DevKit.Core.Observables.API
{
    public class PropertyChangedEventArgs : BaseEventArgs
    {
        public object Source { get; }

        public string PropertyName { get; }

        public object PrevValue { get; }

        public object NewValue { get; }

        public PropertyChangedEventArgs() { }

        public PropertyChangedEventArgs(object source, string propertyName, object prevValue, object newValue)
        {
            Source = source;
            PropertyName = propertyName;
            PrevValue = prevValue;
            NewValue = newValue;
        }

        public static PropertyChangedEventArgs Create(object source, string propertyName, object prevValue, object newValue)
        {
            var args = new PropertyChangedEventArgs(source, propertyName, prevValue, newValue);
            return args;
        }
    }

    public class PropertyChangedEventArgs<T> : PropertyChangedEventArgs where T : class
    {
        public new IObservableObject<T> Source { get; }

        public PropertyChangedEventArgs() { }

        public PropertyChangedEventArgs(
            IObservableObject<T> source
            , string propertyName
            , object prevValue
            , object newValue)
            : base(source, propertyName, prevValue, newValue)
        {
            Source = source;
        }

        public static PropertyChangedEventArgs<T> Create(IObservableObject<T> source, string propertyName, object prevValue, object newValue)
        {
            var args = new PropertyChangedEventArgs<T>(source, propertyName, prevValue, newValue);
            return args;
        }
    }
}
