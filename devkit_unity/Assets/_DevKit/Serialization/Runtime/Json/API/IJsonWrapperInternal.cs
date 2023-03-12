using DevKit.Serialization.Json.Interpreters;

namespace DevKit.Serialization.Json.API
{
	internal interface IJsonWrapperInternal : IJsonWrapper
	{
		/// <summary>
		///     To the json.
		/// </summary>
		/// <param name="writer">The writer.</param>
		void ToJson(JsonWriter writer);
	}
}
