namespace DevKit.Core.Observables.API
{
    /// <summary>
    /// Interface to be used in object's validation APIs
    /// </summary>
    public interface IDataErrorInfo
    {
        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        string Error { get; }

        /// <summary>
        /// Validates the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        string Validate(string propertyName);
    }
}
