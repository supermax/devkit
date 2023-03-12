using System.IO;
using DevKit.Core.Extensions;

namespace DevKit.Serialization.Json.Extensions
{
    public static class SerialisationExtensions
    {
        public static string ToJson<T>(this T graph)
        {
            graph.ThrowIfDefault(nameof(graph));

            var json = JsonMapper.Default.ToJson(graph);
            return json;
        }

        public static T FromJson<T>(this string json)
        {
            json.ThrowIfNullOrEmpty(json);

            var graph = JsonMapper.Default.ToObject<T>(json);
            return graph;
        }

        /// <summary>
        ///     Deserializes from file.
        /// </summary>
        /// <typeparam name="T"> object type </typeparam>
        /// <param name="filePath"> The file path. </param>
        /// <returns> deserialized object </returns>
        public static T DeserializeFromFile<T>(this string filePath)
        {
            var txt = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
            var result = JsonMapper.Default.ToObject<T>(txt);
            return result;
        }

        /// <summary>
        ///     Serializes object to file.
        /// </summary>
        /// <typeparam name="T"> object type </typeparam>
        /// <param name="graph"> The graph. </param>
        /// <param name="filePath"> The file path. </param>
        public static void SerializeToFile<T>(this T graph, string filePath)
        {
            var text = JsonMapper.Default.ToJson(graph);
            File.WriteAllText(filePath, text);
        }

        /// <summary>
        ///     Clones the specified graph.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="graph"> The graph. </param>
        /// <returns> </returns>
        public static T Clone<T>(this T graph)
        {
            var text = JsonMapper.Default.ToJson(graph);
            var result = JsonMapper.Default.ToObject<T>(text);
            return result;
        }
    }
}
