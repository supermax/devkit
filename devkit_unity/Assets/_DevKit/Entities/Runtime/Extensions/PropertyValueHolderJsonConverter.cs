// using System;
//
// namespace DevKit.Entities
// {
//     public class PropertyValueHolderJsonConverter : JsonConverter
//     {
//         public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//         {
//             var holder = (PropertyValueHolder)value;
//             var result = GetValue(holder);
//             writer.WriteValue(result);
//         }
//
//         public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
//             JsonSerializer serializer)
//         {
//             return serializer.Deserialize(reader, objectType);
//         }
//
//         public override bool CanConvert(Type objectType)
//         {
//             return objectType == typeof(PropertyValueHolder);
//         }
//         
//         private static object GetValue(PropertyValueHolder valueHolder)
//         {
//             if (valueHolder.Bool.HasValue)
//                 return valueHolder.Bool.Value;
//             if (valueHolder.Number.HasValue)
//                 return valueHolder.Number.Value;
//             if (valueHolder.Time.HasValue)
//                 return valueHolder.Time.Value;
//             return valueHolder.Text;
//         }
//     }
// }