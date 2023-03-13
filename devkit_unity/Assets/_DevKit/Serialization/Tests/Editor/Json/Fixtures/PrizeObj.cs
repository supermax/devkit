#region

using System.Text;
using DevKit.Serialization.Json.API;

#endregion

namespace DevKit.Serialization.Tests.Editor.Json.Fixtures
{
	[JsonDataContract]
	public class PrizeObj
	{
		[JsonDataMember(Name = "pt")]
		public string PrizeType { get; set; }

		[JsonDataMember(Name = "v")]
		public int Value { get; set; }

		public override string ToString()
		{
			var builder = new StringBuilder();

			builder.Append("PrizeType: " + PrizeType + "\n");
			builder.Append("Value: " + Value + "\n");

			return builder.ToString();
		}
	}
}
