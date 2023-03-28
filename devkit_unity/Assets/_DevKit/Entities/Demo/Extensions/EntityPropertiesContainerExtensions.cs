using System.Text;
using DevKit.Core.Extensions;

namespace DevKit.Entities.Demo.Extensions
{
    public static class EntityPropertiesContainerExtensions
    {
        public static string ToJson(this EntityPropertiesContainer container)
        {
            var json = new StringBuilder()
                .Append('{');
            var i = 0;
            foreach (var pair in container)
            {
                json.AppendFormat("\"{0}\": {1}", pair.Key, pair.Value.ToJson());

                i++;
                if (i < container.Count)
                {
                    json.Append(',');
                }
            }
            json.Append('}');
            return json.ToString();
        }

        public static EntityPropertiesContainer FromJson(this EntityPropertiesContainer container, string json)
        {
            json.ThrowIfNullOrEmpty(nameof(json));

            json = json.TrimStart('{').TrimEnd('}');

            var items = json.Split(',');
            foreach (var str in items)
            {
                if (str.IsNullOrEmpty())
                {
                    continue;
                }

                var pair = str.Split(':');
                if (pair.IsNullOrEmpty() || pair.Length < 2)
                {
                    continue;
                }

                var key = pair[0];
                if (key.IsNullOrEmpty())
                {
                    continue;
                }

                var value = str.Replace($"{key}:", string.Empty).Trim();
                if (value.IsNullOrEmpty())
                {
                    continue;
                }

                var holder = new PropertyValueHolder().FromJson(value);
                key = key.TrimStart('"').TrimEnd('"');
                container[key] = holder;
            }
            return container;
        }
    }
}
