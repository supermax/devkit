using DevKit.Core.Observables.API;

namespace DevKit.Entities.API
{
    /// <summary>
    /// Interface for base entity class
    /// </summary>
    public interface IEntity<T> : IObservableObject<T>, IInitializable where T : class
    {
        /// <summary>
        /// Sets the boolean property value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void SetPropertyValue(string name, bool? value);

        /// <summary>
        /// Sets the numeric property value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void SetPropertyValue(string name, double? value);

        /// <summary>
        /// Set the textual property value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void SetPropertyValue(string name, string value);

        /// <summary>
        /// Get the property value
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        PropertyValueHolder GetPropertyValue(string name);

        /// <summary>
        /// Applies numeric property modifier
        /// </summary>
        /// <param name="name"></param>
        /// <param name="modifier"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        double? ApplyPropertyModifier(string name, PropertyModifierType modifier, double value);

        /// <summary>
        /// Resets property value
        /// </summary>
        /// <param name="name"></param>
        void ResetPropertyValue(string name);
    }
}
