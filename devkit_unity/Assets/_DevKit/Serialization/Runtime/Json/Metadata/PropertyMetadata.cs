#region

using System;
using System.Reflection;
using DevKit.Core.Extensions;
using DevKit.Serialization.Json.API;

#endregion

namespace DevKit.Serialization.Json.Metadata
{
	/// <summary>
	/// Property Metadata
	/// </summary>
	internal sealed class PropertyMetadata
	{
		/// <summary>
		///     The info
		/// </summary>
		public MemberInfo Info
		{
			get;
			private set;
		}

		/// <summary>
		///     The is field
		/// </summary>
		public bool IsField
		{
			get;
			private set;
		}

		/// <summary>
		///     The type
		/// </summary>
		public Type Type
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the attribute.
		/// </summary>
		/// <value>
		/// The attribute.
		/// </value>
		public JsonDataMemberAttribute Attribute
		{
			get;
			private set;
		}

		public string MemberName
		{
			get
			{
				var memberName = Attribute != null && !Attribute.Name.IsNullOrEmpty() ? Attribute.Name : Info.Name;
				return memberName;
			}
		}

		internal PropertyMetadata(Type type, MemberInfo info, bool isField, JsonDataMemberAttribute attribute)
		{
			Type = type;
			Info = info;
			IsField = isField;
			Attribute = attribute;
		}

		/// <summary>
		/// Gets the type of the member.
		/// </summary>
		/// <returns></returns>
		public Type GetMemberType()
		{
			Type res;
			if (IsField)
			{
				var fieldInfo = (FieldInfo) Info;
				res = fieldInfo.FieldType;
			}
			else
			{
				var propInfo = (PropertyInfo) Info;
				res = propInfo.PropertyType;
			}
			return res;
		}

	    public override string ToString()
	    {
	        return $"Type: {GetMemberType()}, Info: {Info}, IsField: {IsField}";
	    }
	}
}
