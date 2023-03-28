using System.Text;
using DevKit.Core.Extensions;

namespace DevKit.Entities.Demo.Extensions
{
    public static class PropertyValueHolderExtensions
    {
			private const string JsonBoolValueFieldName = "\"bool\":";

    		private const string JsonNumberValueFieldName = "\"num\":";

    		private const string JsonTextValueFieldName = "\"txt\":";

    		private const string JsonValueFieldIndex = "{0}";

    		public static string ToJson(this PropertyValueHolder holder)
    		{
    			var str = new StringBuilder();
    			str.Append('{');
    			if (holder.Bool.HasValue)
    			{
    				str.AppendFormat(JsonBoolValueFieldName + JsonValueFieldIndex, holder.Bool.Value);
    			}
    			else if (holder.Number.HasValue)
    			{
    				str.AppendFormat(JsonNumberValueFieldName + JsonValueFieldIndex, holder.Number.Value);
    			}
    			else if (holder.Text != null)
    			{
    				str.AppendFormat($"{JsonTextValueFieldName}: \"{JsonValueFieldIndex}\"", holder.Text);
    			}
    			str.Append('}');
    			return str.ToString();
    		}

    		public static PropertyValueHolder FromJson(this PropertyValueHolder holder, string json)
    		{
    			json.ThrowIfNullOrEmpty(nameof(json));

                json = json.TrimStart('{').TrimEnd('}');

                if (json.StartsWith(JsonBoolValueFieldName))
    			{
    				json = json.Replace(JsonBoolValueFieldName, string.Empty).Trim();
                    holder.Bool = bool.Parse(json);
    			}
    			else if (json.StartsWith(JsonNumberValueFieldName))
    			{
    				json = json.Replace(JsonNumberValueFieldName, string.Empty).Trim();
                    holder.Number = double.Parse(json);
    			}
    			else if (json.StartsWith(JsonTextValueFieldName))
    			{
    				json = json.Replace(JsonTextValueFieldName, string.Empty).Trim();
                    holder.Text = json;
    			}
                return holder;
            }
    }
}
