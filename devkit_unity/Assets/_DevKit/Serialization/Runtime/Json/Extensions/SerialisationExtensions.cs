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
        /// <returns><see cref="string"/></returns>
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
            var text = JsonMapper.Default.ToJson(graph);
            var result = JsonMapper.Default.ToObject<T>(text);
            return result;
        }
    }
}
