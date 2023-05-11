#region Usings
using System;
#endregion

namespace DevKit.Serialization.Json.API
{
	/// <summary>
	/// Interface for JsonMapper
	/// </summary>
	public interface IJsonMapper
	{
		/// <summary>
		///     To the json.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <returns></returns>
		string ToJson(object obj);

		/// <summary>
		///     To the object.
		/// </summary>
		/// <param name="json">The json.</param>
		/// <returns></returns>
		JsonData ToObject(string json);

		/// <summary>
		///     To the object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="json">The json.</param>
		/// <returns></returns>
		T ToObject<T>(string json);

		/// <summary>
		/// To the object.
		/// </summary>
		/// <param name="json"></param>
		/// <param name="targetObjectType"></param>
		/// <returns></returns>
		object ToObject(string json, Type targetObjectType);

		/// <summary>
		///     To the object
		/// </summary>
		/// <typeparam name="T">Type to conversion</typeparam>
		/// <param name="jsonData">The json data.</param>
		/// <returns></returns>
		T ToObject<T>(JsonData jsonData);

		/// <summary>
		///     Registers the exporter.
		/// </summary>
		/// <param name="converter">The conversion function</param>
		/// <typeparam name="TValue">Source Type</typeparam>
		/// <typeparam name="TJson">Json Type</typeparam>
		void RegisterExporter<TValue, TJson>(Func<TValue, TJson> converter);

		/// <summary>
		///     Registers the importer.
		/// </summary>
		/// <typeparam name="TJson">The type of the json.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="importer">The importer.</param>
		void RegisterImporter<TJson, TValue>(ImporterFunc<TJson, TValue> importer);

		/// <summary>
		/// Registers <see cref="JsonDataMemberAttribute"/> per given object type <see cref="T"/>
		/// </summary>
		/// <param name="propName">The name of the property to map to the attribute</param>
		/// <param name="attribute">The instance of the attribute</param>
		/// <param name="overrideOtherAttribs">If set `true` then will override other similar attributes</param>
		/// <typeparam name="T">The type of the object</typeparam>
		void RegisterJsonDataMemberAttribute<T>(
			string propName
			, JsonDataMemberAttribute attribute
			, bool overrideOtherAttribs = true);
	}
}
