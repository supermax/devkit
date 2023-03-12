using System;
using System.Runtime.Serialization;

namespace DevKit.Entities
{
    /// <summary>
    /// Performance friendly, strongly typed, holder of property values
    /// </summary>
    /// <remarks>
    /// Mainly used in RPG entities with engine initialization and optimal calculations
    /// </remarks>
    [DataContract]
    [Serializable]
    public struct PropertyValueHolder
    {
        /// <summary>
        /// Holds <see cref="bool"/> value
        /// </summary>
        [DataMember(Name = "boolean")]
        public bool? Bool { get; set; }

        /// <summary>
        /// Holds <see cref="double"/> value
        /// </summary>
        [DataMember(Name = "numeric")]
        public double? Number { get; set; }

        /// <summary>
        /// Holds <see cref="string"/> value
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }

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
    }
}
