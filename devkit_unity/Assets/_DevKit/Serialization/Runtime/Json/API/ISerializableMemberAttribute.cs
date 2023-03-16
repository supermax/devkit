namespace DevKit.Serialization.Json.API
{
    public interface ISerializableMemberAttribute
    {
        /// <summary>
        /// Called before serialization.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        object BeforeSerialization(object src, object value);

        /// <summary>
        /// Called before deserialization.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        string BeforeDeserialization(object src, string value);

        /// <summary>
        /// Called after serialization.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        string AfterSerialization(object src, string value);

        /// <summary>
        /// Called after deserialization.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        object AfterDeserialization(object src, object value);
    }
}
