#region

using System;
using DevKit.Serialization.Json.Interpreters;

#endregion

namespace DevKit.Serialization.Json.API
{
	/// <summary>
	/// </summary>
	public class JsonException : Exception
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="JsonException" /> class.
		/// </summary>
		public JsonException()
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="JsonException" /> class.
		/// </summary>
		/// <param name="token">The token.</param>
		internal JsonException(ParserToken token) :
			base($"Invalid token '{token}' in input string")
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="JsonException" /> class.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <param name="innerException">The inner exception.</param>
		internal JsonException(ParserToken token,
			Exception innerException) :
				base($"Invalid token '{token}' in input string",
					innerException)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="JsonException" /> class.
		/// </summary>
		/// <param name="c">The c.</param>
		internal JsonException(int c) :
			base($"Invalid character '{(char)c}' in input string")
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="JsonException" /> class.
		/// </summary>
		/// <param name="c">The c.</param>
		/// <param name="innerException">The inner exception.</param>
		internal JsonException(int c, Exception innerException) :
			base($"Invalid character '{(char)c}' in input string",
				innerException)
		{
		}


		/// <summary>
		///     Initializes a new instance of the <see cref="JsonException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public JsonException(string message) : base(message)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="JsonException" /> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		///     The exception that is the cause of the current exception, or a null reference (Nothing in
		///     Visual Basic) if no inner exception is specified.
		/// </param>
		public JsonException(string message, Exception innerException) :
			base(message, innerException)
		{
		}
	}
}
