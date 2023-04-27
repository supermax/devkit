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
    }

    public class PropertyChangedEventArgs<T> : PropertyChangedEventArgs where T : class
    {
        public new T Source { get; }

        public PropertyChangedEventArgs() { }

        public PropertyChangedEventArgs(
            T source
            , string propertyName
            , object prevValue
            , object newValue)
            : base(source, propertyName, prevValue, newValue)
        {
            Source = source;
        }
    }
}
