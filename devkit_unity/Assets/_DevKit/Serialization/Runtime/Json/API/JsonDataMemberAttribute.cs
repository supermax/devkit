#region Usings

using System;

#endregion

namespace DevKit.Serialization.Json.API
{
	/// <summary>
	///     JSON Data Member Attribute
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
	public class JsonDataMemberAttribute : Attribute, ISerializableMemberAttribute
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="JsonDataMemberAttribute" /> class.
		/// </summary>
		public JsonDataMemberAttribute()
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="JsonDataMemberAttribute" /> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public JsonDataMemberAttribute(string name)
		{
			Name = name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JsonDataMemberAttribute"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="fallbackValue">The fallback value.</param>
		public JsonDataMemberAttribute(string name, object fallbackValue) : this(name)
		{
			FallbackValue = fallbackValue;
		}

		/// <summary>
		///     Gets or sets the name of the JSON field.
		/// </summary>
		/// <value>
		///     The name of JSON field.
		/// </value>
		public virtual string Name { get; set; }

		/// <summary>
		/// Gets or sets the fallback value in case of casting or deserialization error.
		/// </summary>
		/// <value>
		/// The fallback value in case of error.
		/// </value>
		public virtual object FallbackValue { get; set; }

		/// <summary>
		/// Gets or sets the default value in case of serialization error.
		/// </summary>
		/// <value>
		/// The default value.
		/// </value>
		/// TODO implement usage of this prop
		public virtual object DefaultValue { get; set; }

		/// <summary>
		/// The type of the converter to use for given member
		/// </summary>
		public Type ConverterType { get; set; }

		/// <summary>
		/// Ignore this member is it's value is null or default
		/// </summary>
		/// <remarks>
		///	<para>
		///	Serialization: JSON will not include this member
		/// </para>
		/// </remarks>
		/// TODO implement usage of this prop
		public bool IgnoreIfNullOrDefault { get; set; }

		/// <summary>
		/// Called before serialization.
		/// </summary>
		/// <param name="src">The source.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public virtual object BeforeSerialization(object src, object value)
		{
			return value; // TODO
		}

		/// <summary>
		/// Called before deserialization.
		/// </summary>
		/// <param name="src">The source.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public virtual string BeforeDeserialization(object src, string value)
		{
			return value; // TODO
		}

		/// <summary>
		/// Called after serialization.
		/// </summary>
		/// <param name="src">The source.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public virtual string AfterSerialization(object src, string value)
		{
			return value; // TODO
		}

		/// <summary>
		/// Called after deserialization.
		/// </summary>
		/// <param name="src">The source.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public virtual object AfterDeserialization(object src, object value)
		{
			return value; // TODO
		}
	}
}
