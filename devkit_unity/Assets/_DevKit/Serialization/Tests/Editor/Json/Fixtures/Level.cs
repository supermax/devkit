#region

using System.Text;
using DevKit.Serialization.Json.API;
using TMS.Common.Tests.Serialization.Json.TestClasses;

#endregion

namespace DevKit.Serialization.Tests.Editor.Json.Fixtures
{
	[JsonDataContract]
	public class Level
	{
		[JsonDataMember(Name = "levelBonus")]
		public LevelBonusObj LevelBonus { get; set; }

		[JsonDataMember(Name = "basicLevelInfo")]
		public BasicLevelInfoObj BasicLevelInfo { get; set; }

		[JsonDataMemberIgnore]
		public string Name { get; set; }

		public override string ToString()
		{
			var builder = new StringBuilder();

			builder.Append("LevelBonus:\n" + LevelBonus + "\n");
			builder.Append("BasicLevelInfo:\n" + BasicLevelInfo + "\n");

			return builder.ToString();
		}
	}
}
