namespace DevKit.Core.Observables.API
{
    /// <summary>
    /// Defines property value change event
    /// </summary>
    public delegate void PropertyChangedEventHandler<T>(IObservableObject<T> sender, PropertyChangedEventArgs<T> args) where T : class;

}
