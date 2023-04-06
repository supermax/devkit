using System;
using System.Text;

namespace DevKit.Entities
{
    /// <summary>
    /// Performance friendly, strongly typed, holder of property values
    /// </summary>
    /// <remarks>
    /// Mainly used in RPG entities with engine initialization and optimal calculations
    /// </remarks>
    [Serializable]
    public class PropertyValueHolder
    {
        /// <summary>
        /// Holds <see cref="bool"/> value
        /// </summary>
        public bool? Bool { get; set; }

        /// <summary>
        /// Holds <see cref="double"/> value
        /// </summary>
        public double? Number { get; set; }

        /// <summary>
        /// Holds <see cref="string"/> value
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Holds <see cref="DateTime"/> value
        /// </summary>
        public DateTime? Time { get; set; }

        /// <summary>
        /// Default ctor
        /// </summary>
        public PropertyValueHolder()
        {
        }

        /// <summary>
        /// Ctor accepting <see cref="bool"/> value
        /// </summary>
        /// <param name="value"><see cref="bool"/> value</param>
        public PropertyValueHolder(bool value)
        {
            SetValue(value);
        }

        /// <summary>
        /// Ctor accepting <see cref="double"/> value
        /// </summary>
        /// <param name="value"><see cref="double"/> value</param>
        public PropertyValueHolder(double value)
        {
            SetValue(value);
        }

        /// <summary>
        /// Ctor accepting <see cref="string"/> value
        /// </summary>
        /// <param name="value"><see cref="string"/> value</param>
        public PropertyValueHolder(string value)
        {
            SetValue(value);
        }

        /// <summary>
        /// Ctor accepting <see cref="DateTime"/> value
        /// </summary>
        /// <param name="value"><see cref="DateTime"/> value</param>
        public PropertyValueHolder(DateTime value)
        {
	        SetValue(value);
        }

        /// <summary>
        /// Sets <see cref="double"/> value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PropertyValueHolder SetValue(double? value)
        {
            Number = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="bool"/> value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PropertyValueHolder SetValue(bool? value)
        {
            Bool = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="string"/> value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PropertyValueHolder SetValue(string value)
        {
            Text = value;
            return this;
        }

        /// <summary>
        /// Sets <see cref="DateTime"/> value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PropertyValueHolder SetValue(DateTime? value)
        {
	        Time = value;
	        return this;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append('{')
               .AppendFormat("\"bool:\" {0}", Bool)
               .AppendFormat(", \"num:\" {0}", Number)
               .AppendFormat(", \"time:\" \"{0}\"", Time)
               .AppendFormat(", \"txt:\" \"{0}\"", Text)
               .Append('}');
            return str.ToString();
        }

        #region Implicit Conversions

		/// <summary>
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static implicit operator PropertyValueHolder(bool data)
		{
			return new PropertyValueHolder(data);
		}

		/// <summary>
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static implicit operator PropertyValueHolder(double data)
		{
			return new PropertyValueHolder(data);
		}

		/// <summary>
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static implicit operator PropertyValueHolder(int data)
		{
			return new PropertyValueHolder(data);
		}

		/// <summary>
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static implicit operator PropertyValueHolder(string data)
		{
			return new PropertyValueHolder(data);
		}

		/// <summary>
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public static implicit operator PropertyValueHolder(DateTime data)
		{
			return new PropertyValueHolder(data);
		}

		#endregion

		#region Explicit Conversions

		/// <summary>
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		/// <exception cref="System.InvalidCastException">Instance of JsonData doesn't hold a double</exception>
		public static explicit operator bool(PropertyValueHolder data)
		{
			if (!data.Bool.HasValue)
			{
				throw new InvalidCastException(
					$"Instance of {nameof(PropertyValueHolder)} doesn't hold a {typeof(bool)}");
			}
			return data.Bool.GetValueOrDefault();
		}

		/// <summary>
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		/// <exception cref="System.InvalidCastException">Instance of JsonData doesn't hold a double</exception>
		public static explicit operator double(PropertyValueHolder data)
		{
			if (!data.Number.HasValue)
			{
				throw new InvalidCastException(
					$"Instance of {nameof(PropertyValueHolder)} doesn't hold a {typeof(double)}");
			}
			return data.Number.GetValueOrDefault();
		}

		/// <summary>
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		/// <exception cref="System.InvalidCastException">Instance of JsonData doesn't hold an int</exception>
		public static explicit operator int(PropertyValueHolder data)
		{
			if (!data.Number.HasValue)
			{
				throw new InvalidCastException(
					$"Instance of {nameof(PropertyValueHolder)} doesn't hold a {typeof(int)}");
			}
			return (int)data.Number.GetValueOrDefault();
		}

		/// <summary>
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		/// <exception cref="System.InvalidCastException">Instance of JsonData doesn't hold an long</exception>
		public static explicit operator long(PropertyValueHolder data)
		{
			if (!data.Number.HasValue)
			{
				throw new InvalidCastException(
					$"Instance of {nameof(PropertyValueHolder)} doesn't hold a {typeof(long)}");
			}
			return (long)data.Number.GetValueOrDefault();
		}

		/// <summary>
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		/// <exception cref="System.InvalidCastException">Instance of JsonData doesn't hold a string</exception>
		public static explicit operator string(PropertyValueHolder data)
		{
			if (data.Text != null)
			{
				throw new InvalidCastException(
					$"Instance of {nameof(PropertyValueHolder)} doesn't hold a {typeof(string)}");
			}
			return data.Text;
		}

		/// <summary>
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		/// <exception cref="System.InvalidCastException">Instance of JsonData doesn't hold a string</exception>
		public static explicit operator DateTime(PropertyValueHolder data)
		{
			if (data.Text != null)
			{
				throw new InvalidCastException(
					$"Instance of {nameof(PropertyValueHolder)} doesn't hold a {typeof(DateTime)}");
			}
			return data.Time.GetValueOrDefault();
		}

		#endregion
    }
}
