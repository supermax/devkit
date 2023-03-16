using System.Runtime.CompilerServices;

namespace DevKit.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if string is null or empty
        /// </summary>
        /// <param name="source">The source string</param>
        /// <returns>"True" if string is null or empty</returns>
        public static bool IsNullOrEmpty(this string source)
        {
            var isEmpty = string.IsNullOrEmpty(source);
            return isEmpty;
        }

        /// <summary>
        /// Returns <see cref="string"/> in JSON field name format
        /// </summary>
        /// <param name="propName">Source property name</param>
        /// <returns><see cref="string"/> in JSON field name format</returns>
        public static string ToJsonPropName(this string propName)
        {
            if (propName.IsNullOrEmpty() || propName.Length < 2)
            {
                return propName;
            }

            propName = propName.Trim();
            propName = propName.Replace("_", string.Empty);
            var firstChar = propName[0].ToString().ToLowerInvariant();
            propName = propName.Remove(0, 1);
            var jPropName = propName.Insert(0, firstChar);
            return jPropName;
        }
    }
}
