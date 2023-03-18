using System;
using System.Collections.Generic;
using System.Reflection;

namespace DevKit.Serialization.Json.API
{
    /// <summary>
	/// Interface for TypeWrapper
	/// </summary>
	public interface ITypeWrapper
	{
		/// <summary>
		/// Gets the declaring type.
		/// </summary>
		/// <value>
		/// The declaring type.
		/// </value>
		Type DeclaringType { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is enum.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is enum; otherwise, <c>false</c>.
		/// </value>
		bool IsEnum { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is array.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is array; otherwise, <c>false</c>.
		/// </value>
		bool IsArray { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is class.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is class; otherwise, <c>false</c>.
		/// </value>
		bool IsClass { get; }

		/// <summary>
		/// Determines whether [is assignable from] [the specified type].
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		///   <c>true</c> if [is assignable from] [the specified type]; otherwise, <c>false</c>.
		/// </returns>
		bool IsAssignableFrom(Type type);

		/// <summary>
		/// Gets the properties.
		/// </summary>
		/// <returns></returns>
		IEnumerable<PropertyInfo> GetProperties();

		/// <summary>
		/// Gets the property.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		PropertyInfo GetProperty(string name);

		/// <summary>
		/// Gets the fields.
		/// </summary>
		/// <returns></returns>
		IEnumerable<FieldInfo> GetFields();

		/// <summary>
		/// Gets the implemented interfaces.
		/// </summary>
		/// <returns></returns>
		IEnumerable<Type> GetImplementedInterfaces();

		/// <summary>
		/// Gets the interface.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
		/// <returns></returns>
		Type GetInterface(string name, bool ignoreCase);

		/// <summary>
		/// Gets the method.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="types">The types.</param>
		/// <returns></returns>
		MethodInfo GetMethod(string name, Type[] types);

		/// <summary>
		/// Gets the event.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		EventInfo GetEvent(string name);

		/// <summary>
		/// Gets the custom attributes.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="inherit">if set to <c>true</c> [inherit].</param>
		/// <returns></returns>
		T[] GetCustomAttributes<T>(bool inherit) where T : Attribute;
	}
}
