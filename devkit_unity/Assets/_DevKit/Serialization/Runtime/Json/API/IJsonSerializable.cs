namespace DevKit.Serialization.Json.API
{
    /// <summary>
    /// Interface for classes that can be serialized to JSON
    /// </summary>
    public interface IJsonSerializable
    {
        /// <summary>
        /// Returns JSON string with instance's values
        /// </summary>
        /// <returns>
        /// Returns JSON string
        /// </returns>
        string ToJson();
    }
}
