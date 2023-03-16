using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DevKit.Core.Extensions
{
    /// <summary>
    /// Collection Extensions
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Returns first element or default value of <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static T GetFirstOrDefault<T>(this IEnumerable<T> source)
        {
            source.ThrowIfNull(nameof(source));

            if (source is IList<T> list)
            {
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
            else
            {
                using var enumerator = source.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    return enumerator.Current;
                }
            }
            return default;
        }

        /// <summary>
        /// Convert array to array of strings
        /// </summary>
        /// <param name="source">The source array</param>
        /// <param name="converter"><see cref="T"/> to <see cref="string"/> conversion function</param>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <returns>The array of strings</returns>
        public static string[] ToStringArray<T>(this T[] source, Func<T, string> converter)
        {
            source.ThrowIfNull(nameof(source));

            var copy = new string[source.Length];
            for (var i = 0; i < source.Length; i++)
            {
                copy[i] = converter(source[i]);
            }
            return copy;
        }

        /// <summary>
        /// Convert array to array of objects
        /// </summary>
        /// <param name="source">The source array</param>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <returns>The array of objects</returns>
        public static object[] ToObjectArray<T>(this T[] source)
        {
            source.ThrowIfNull(nameof(source));

            var copy = new object[source.Length];
            Array.Copy(source, copy, source.Length);
            return copy;
        }

        /// <summary>
        /// Check if collection is null or empty
        /// </summary>
        /// <param name="source">The source collection</param>
        /// <typeparam name="T">The item type</typeparam>
        /// <returns>"true" in case array us null or empty</returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            if(source == null)
            {
                return true;
            }
            if(source.Count < 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Iterate thru the collection and execute the given action
        /// </summary>
        /// <param name="collection">The source collection</param>
        /// <param name="action">The action to execute during the iteration</param>
        /// <typeparam name="T">The type of the item</typeparam>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        /// <summary>
        /// Iterate thru the array and execute the given action
        /// </summary>
        /// <remarks>
        /// Using more effective for(i) loop instead of the expensive enumerator
        /// </remarks>
        /// <param name="collection">The source array</param>
        /// <param name="action">The action to execute during the iteration</param>
        /// <typeparam name="T">The type of the item</typeparam>
        public static void ForEach<T>(this T[] collection, Action<T> action)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < collection.Length; i++)
            {
                action(collection[i]);
            }
        }

        /// <summary>
        /// Iterate thru the list and execute the given action
        /// </summary>
        /// <remarks>
        /// Using more effective for(i) loop instead of the expensive enumerator
        /// </remarks>
        /// <param name="collection">The source list</param>
        /// <param name="action">The action to execute during the iteration</param>
        /// <typeparam name="T">The type of the item</typeparam>
        public static void ForEach<T>(this IList<T> collection, Action<T> action)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < collection.Count; i++)
            {
                action(collection[i]);
            }
        }

        /// <summary>
        /// IsNullOrEmpty
        /// </summary>
        /// <param name="src"></param>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<TK, TV>(this Dictionary<TK, TV> src)
        {
            if (src == null)
            {
                return true;
            }

            if (src.Count < 1)
            {
                return true;
            }

            return false;
        }

        public static bool IsNullOrEmpty<TK, TV>(this IDictionary<TK, TV> src)
        {
            if (src == null)
            {
                return true;
            }

            if (src.Count < 1)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// IsNullOrEmpty
        /// </summary>
        /// <param name="src"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> src)
        {
            if (src == null)
            {
                return true;
            }

            if (!src.Any())
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// IsNullOrEmpty
        /// </summary>
        /// <param name="src"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T> src)
        {
            if (src == null)
            {
                return true;
            }

            if (src.Count < 1)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Checks if list is null or empty
        /// </summary>
        /// <param name="src"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>True is case list is null or has not items</returns>
        public static bool IsNullOrEmpty<T>(this List<T> src)
        {
            if (src == null)
            {
                return true;
            }

            if (src.Count < 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// IsNullOrEmpty
        /// </summary>
        /// <param name="src"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IList<T> src)
        {
            if (src == null)
            {
                return true;
            }

            if (src.Count < 1)
            {
                return true;
            }

            return false;
        }

        public static T Find<T>(this T[] src, Predicate<T> predicate)
        {
            if (src == null)
            {
                return default;
            }

            var res = Array.Find(src, predicate);
            return res;
        }

        public static bool IsNullOrEmpty<T>(this T[] src)
        {
            if (src == null)
            {
                return true;
            }

            if (src.Length < 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     IsNullOrEmpty handling for nullable Guid objects
        /// </summary>
        /// <param name="src">The Guid we are checking</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this Guid? src)
        {
            if (src == null)
            {
                return true;
            }

            if (src == Guid.Empty)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     IsNullOrEmpty handling for non-nullable Guid objects
        /// </summary>
        /// <param name="src">The Guid we are checking</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this Guid src)
        {
            if (src == Guid.Empty)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns new trimmed list with items based on the given range
        /// </summary>
        /// <param name="list">Source list</param>
        /// <param name="pageIndex">page index</param>
        /// <param name="maxListSize">max num of items per page</param>
        /// <typeparam name="T">Type of item</typeparam>
        /// <returns>New trimmed list with items based on the given range</returns>
        /// <exception cref="OperationCanceledException">list != null</exception>
        public static List<T> GetTrimmedList<T>(this IList<T> list, int pageIndex, int maxListSize)
        {
            if (list.IsNullOrEmpty())
            {
                throw new OperationCanceledException($"{nameof(list)}is null/empty!");
            }

            var newData = new List<T>();
            var count = list.Count;
            var startingNumber = (pageIndex-1) * maxListSize;
            var endingNumber = pageIndex * maxListSize;

            if(endingNumber > count)
            {
                endingNumber = count;
            }

            if (startingNumber >= count)
            {
                return newData;
            }

            for(var i = startingNumber; i < endingNumber; i++)
            {
                var element = list.ElementAt(i);
                newData.Add(element);
            }
            return newData;
        }

        /// <summary>
        /// Returns number of pages required for the given list
        /// </summary>
        /// <param name="list">Source list</param>
        /// <param name="maxListSize">Max num of items per page</param>
        /// <typeparam name="T">Type of item</typeparam>
        /// <returns>Number of pages required for the given list</returns>
        /// <exception cref="OperationCanceledException">list != null</exception>
        public static int GetPagesCount<T>(this IList<T> list, int maxListSize)
        {
            if (list.IsNullOrEmpty())
            {
                throw new OperationCanceledException($"{nameof(list)}is null/empty!");
            }

            if (maxListSize < 1)
            {
                maxListSize = 1;
            }
            var page = (double)list.Count / maxListSize;
            var pagesRequired = Math.Ceiling(page);

            return (int)pagesRequired;
        }

        /// <summary>
        /// Generates Key with pattern [TypeName] {Prefix} {args} with "," separator
        /// </summary>
        /// <param name="source">The key prefix: Source to define [TypeName]</param>
        /// <param name="prefix">Prefix to add before args</param>
        /// <param name="args">The key postfix</param>
        /// <returns>Complex Key</returns>
        /// <exception cref="ArgumentNullException">source != null</exception>
        public static string GetKey<T>(this T source, string prefix = null, params object[] args) where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (args.IsNullOrEmpty())
            {
                Debug.Log($"{nameof(args)} is null/empty");
            }

            var type = source.GetType().Name;
            var str = new StringBuilder();
            str.AppendFormat("[{0}]", type);
            if (!string.IsNullOrEmpty(prefix))
            {
                str.AppendFormat(" {0}", prefix);
            }
            if (args.IsNullOrEmpty())
            {
                return str.ToString();
            }
            str.Append(" {");
            str.AppendJoin(", ", args);
            str.Append('}');
            return str.ToString();
        }

        private static readonly IDictionary<Type, Func<string, object>> Convertors
            = new Dictionary<Type, Func<string, object>>
            {
                { typeof(int), x => int.Parse(x) },
                { typeof(float), x => float.Parse(x) },
                { typeof(decimal), x => decimal.Parse(x) },
                { typeof(double), x => double.Parse(x) },
                { typeof(bool), x => bool.Parse(x) },
                { typeof(DateTime), x => DateTime.Parse(x) },
                { typeof(TimeSpan), x => TimeSpan.Parse(x) },

            };

        public static bool TryGetValue<T>(this IDictionary<string, string> src, string key, out T value)
        {
            value = default;
            if (!src.TryGetValue(key, out var str))
            {
                return false;
            }

            if (str.IsNullOrEmpty())
            {
                return false;
            }

            var type = typeof(T);
            if (!Convertors.ContainsKey(type))
            {
                return false;
            }
            value = (T)Convertors[type](str);
            return true;
        }

        public static T GetValueOrDefault<T>(this IDictionary<string, string> src, string key, T defaultValue = default)
        {
            if (!src.TryGetValue(key, out T value))
            {
                value = defaultValue;
            }
            return value;
        }

                /// <summary>
        /// Returns 1st element in the array
        /// </summary>
        /// <param name="collection">The source array</param>
        /// <typeparam name="T">The type of the array elements</typeparam>
        /// <returns>1st element</returns>
        public static T First<T>(this T[] collection)
        {
            if (collection.IsNullOrEmpty())
            {
                return default;
            }
            var first = collection[0];
            return first;
        }

        /// <summary>
        /// Returns 1st element in the array
        /// </summary>
        /// <param name="collection">The source array</param>
        /// <typeparam name="T">The type of the array elements</typeparam>
        /// <returns>1st element</returns>
        public static T First<T>(this IList<T> collection)
        {
            if (collection.IsNullOrEmpty())
            {
                return default;
            }
            var first = collection[0];
            return first;
        }

        /// <summary>
        /// Returns last element in the array
        /// </summary>
        /// <param name="collection">The source array</param>
        /// <typeparam name="T">The type of the array elements</typeparam>
        /// <returns>Last element</returns>
        public static T Last<T>(this T[] collection)
        {
            if (collection.IsNullOrEmpty())
            {
                return default;
            }
            var last = collection[^1];
            return last;
        }

        /// <summary>
        /// Returns last element in the array
        /// </summary>
        /// <param name="collection">The source array</param>
        /// <typeparam name="T">The type of the array elements</typeparam>
        /// <returns>Last element</returns>
        public static T Last<T>(this IList<T> collection)
        {
            if (collection.IsNullOrEmpty())
            {
                return default;
            }
            var last = collection[^1];
            return last;
        }

        /// <summary>
        /// Randomly shuffles array and returns same instance
        /// </summary>
        /// <param name="arr">The source to shuffle</param>
        /// <typeparam name="T">The type of the items</typeparam>
        /// <returns>Shuffled array</returns>
        public static T[] Shuffle<T>(this T[] arr)
        {
            // init standard randomizer
            var rnd = new System.Random();

            // capture array length
            var n = arr.Length;
            // loop to shuffle array items in random order
            while (n > 1)
            {
                var i = rnd.Next(n--);
                (arr[n], arr[i]) = (arr[i], arr[n]);
            }
            return arr;
        }
    }
}
