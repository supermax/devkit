using System;
using DevKit.Core.Extensions;

namespace DevKit.Serialization.Json.Extensions
{
    public static class SerialisationExtensions
    {
        /// <summary>
        /// Serialize the object to JSON string
        /// </summary>
        /// <param name="graph">Source object</param>
        /// <typeparam name="T">The type of object</typeparam>
        /// <returns><see cref="string"/></returns>
        public static string ToJson<T>(this T graph)
        {
            graph.ThrowIfDefault(nameof(graph));

            var json = JsonMapper.Default.ToJson(graph);
            return json;
        }

        /// <summary>
        /// Deserialize string to object
        /// </summary>
        /// <param name="json">Source JSON string</param>
        /// <typeparam name="T">The type of target object</typeparam>
        public static T ToObject<T>(this string json)
        {
            json.ThrowIfNullOrEmpty(json);

            var graph = JsonMapper.Default.ToObject<T>(json);
            return graph;
        }

        /// <summary>
        /// Clones the specified object.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="graph">The graph.</param>
        /// <returns> </returns>
        public static T Clone<T>(this T graph)
        {
            // TODO use JsonData and not Json String
            var text = JsonMapper.Default.ToJson(graph);
            var result = JsonMapper.Default.ToObject<T>(text);
            return result;
        }

        /// <summary>
        /// Clones the specified object.
        /// </summary>
        /// <param name="graph"></param>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <returns></returns>
        public static TT Clone<TS, TT>(this TS graph)
        {
            // TODO use JsonData and not Json String
            var text = JsonMapper.Default.ToJson(graph);
            var result = JsonMapper.Default.ToObject(text, typeof(TT));
            return (TT)result;
        }

        /// <summary>
        /// Clones the specified object.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="targetType"></param>
        /// <typeparam name="TS"></typeparam>
        /// <returns></returns>
        public static object Clone<TS>(this TS graph, Type targetType)
        {
            // TODO use JsonData and not Json String
            var text = JsonMapper.Default.ToJson(graph);
            var result = JsonMapper.Default.ToObject(text, targetType);
            return result;
        }
    }
}
