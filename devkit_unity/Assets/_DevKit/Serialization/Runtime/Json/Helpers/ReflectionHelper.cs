using System;
using System.Reflection;
using DevKit.Core.Extensions;
using DevKit.Serialization.Json.API;

namespace DevKit.Serialization.Json.Helpers
{
    internal static class ReflectionHelper
    {
        /// <summary>
		/// Gets the data member attribute.
		/// </summary>
		/// <param name="info">The information.</param>
		/// <returns></returns>
		internal static JsonDataMemberAttribute[] GetDataMemberAttributes(MemberInfo info)
		{
			var attributes = info.GetCustomAttributes(typeof (JsonDataMemberAttribute), true);
			if (attributes.IsNullOrEmpty())
			{
				return null;
			}
			var jsonAttributes = new JsonDataMemberAttribute[attributes.Length];
			Array.Copy(attributes, jsonAttributes, attributes.Length);
			return jsonAttributes;
		}

		/// <summary>
		///     Determines whether [is ignorable member] [by the specified member information].
		/// </summary>
		/// <param name="info">The member information.</param>
		/// <returns></returns>
		internal static bool IsIgnorableMember(MemberInfo info)
		{
			var attributes = info.GetCustomAttributes(typeof (JsonDataMemberIgnoreAttribute), true);
			var isIgnorable = !attributes.IsNullOrEmpty();
			return isIgnorable;
		}
    }
}
