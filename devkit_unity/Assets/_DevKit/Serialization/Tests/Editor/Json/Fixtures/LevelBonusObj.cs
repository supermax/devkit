#region

using System.Collections.Generic;
using System.Text;
using DevKit.Serialization.Json.API;
using TMS.Common.Tests.Serialization.Json.TestClasses;

#endregion

namespace DevKit.Serialization.Tests.Editor.Json.Fixtures
{
	[JsonDataContract]
	public class LevelBonusObj
	{
		[JsonDataMember(Name = "level")]
		public int Level { get; set; }

		[JsonDataMember(Name = "select")]
		public int Select { get; set; }

		[JsonDataMember(Name = "outOf")]
		public int OutOf { get; set; }

		[JsonDataMember(Name = "prizes")]
		public List<PrizeObj> Prizes { get; set; }

		public override string ToString()
		{
			var builder = new StringBuilder();

			builder.Append("Level: " + Level + "\n");
			builder.Append("Select: " + Select + "\n");
			builder.Append("OutOf: " + OutOf + "\n");

			foreach (var prizeVo in Prizes)
			{
				builder.Append("prize: \n" + prizeVo.ToString());
			}

			return builder.ToString();
		}
	}
}
