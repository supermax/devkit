namespace DevKit.Serialization.Json.API
{
	/// <summary>
	/// Serializable object to JSON string format
	/// </summary>
	public interface IJsonSerializable
	{
		/// <summary>
		/// Get JSON string that represents this object instance
		/// </summary>
		/// <returns>JSON string that represents this object instance</returns>
		string ToJson();
	}
}
