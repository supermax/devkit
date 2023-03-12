#region Usings

using System;

#endregion

namespace DevKit.Serialization.Json.API
{
	/// <summary>
	///     JSON Data Member Ignore Attribute
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class JsonDataMemberIgnoreAttribute : Attribute
	{

	}
}
