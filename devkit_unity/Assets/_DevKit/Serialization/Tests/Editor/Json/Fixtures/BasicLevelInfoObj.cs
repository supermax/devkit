#region

using System.Text;
using DevKit.Serialization.Json.API;
using TMS.Common.Tests.Serialization.Json.TestClasses;

#endregion

namespace DevKit.Serialization.Tests.Editor.Json.Fixtures
{
	[JsonDataContract]
	public class BasicLevelInfoObj
	{
		[JsonDataMember(Name = "lvl")]
		public LevelInfoObj CurrentLevel { get; set; }

		[JsonDataMember(Name = "nextLvl")]
		public LevelInfoObj NextLevel { get; set; }

		public override string ToString()
		{
			var builder = new StringBuilder();

			builder.Append("CurrentLevel:\n" + CurrentLevel + "\n");
			builder.Append("NextLevel:\n" + NextLevel + "\n");

			return builder.ToString();
		}
	}
}
