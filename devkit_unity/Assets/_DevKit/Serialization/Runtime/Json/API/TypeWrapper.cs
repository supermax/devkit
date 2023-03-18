using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevKit.Core.Extensions;

namespace DevKit.Serialization.Json.API
{
	internal class TypeWrapper : ITypeWrapper
	{
		private readonly Type _type;

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeWrapper"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		public TypeWrapper(Type type)
		{
			_type = type;
		}

		public Type DeclaringType
		{
			get { return _type; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance is enum.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is enum; otherwise, <c>false</c>.
		/// </value>
		public bool IsEnum
		{
			get { return _type.IsEnum; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance is array.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is array; otherwise, <c>false</c>.
		/// </value>
		/// <exception cref="System.NotImplementedException"></exception>
		public bool IsArray
		{
			get { return _type.IsArray; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance is class.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is class; otherwise, <c>false</c>.
		/// </value>
		/// <exception cref="System.NotImplementedException"></exception>
		public bool IsClass
		{
			get { return _type.IsClass; }
		}

		/// <summary>
		/// Determines whether [is assignable from] [the specified type].
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		///   <c>true</c> if [is assignable from] [the specified type]; otherwise, <c>false</c>.
		/// </returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public bool IsAssignableFrom(Type type)
		{
			return _type.IsAssignableFrom(type);
		}

		/// <summary>
		/// Gets the properties.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public IEnumerable<PropertyInfo> GetProperties()
		{
			return _type.GetProperties();
		}

		/// <summary>
		/// Gets the fields.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public IEnumerable<FieldInfo> GetFields()
		{
			return _type.GetFields();
		}

		/// <summary>
		/// Gets the implemented interfaces.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Type> GetImplementedInterfaces()
		{
			return _type.GetInterfaces();
		}

		/// <summary>
		/// Gets the interface.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
		/// <returns></returns>
		public Type GetInterface(string name, bool ignoreCase)
		{
			return _type.GetInterface(name, ignoreCase);
		}

		/// <summary>
		/// Gets the method.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="types">The types.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public MethodInfo GetMethod(string name, Type[] types)
		{
			return _type.GetMethod(name, types);
		}

		/// <summary>
		/// Gets the event.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public EventInfo GetEvent(string name)
		{
			return _type.GetEvent(name);
		}

		/// <summary>
		/// Gets the custom attributes.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="inherit">if set to <c>true</c> [inherit].</param>
		/// <returns></returns>
		public T[] GetCustomAttributes<T>(bool inherit) where T : Attribute
		{
			var attribs = _type.GetCustomAttributes(typeof(T), inherit);
			if (attribs.IsNullOrEmpty())
			{
				return null;
			}

			var res = attribs.Cast<T>();
			return res.ToArray();

		}

		/// <summary>
		/// Gets the property.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public PropertyInfo GetProperty(string name)
		{
			return _type.GetProperty(name);
		}
	}
}
