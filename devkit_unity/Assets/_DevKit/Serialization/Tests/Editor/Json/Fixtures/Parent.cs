#region

using System;
using DevKit.Serialization.Json.API;

#endregion

namespace DevKit.Serialization.Tests.Editor.Json.Fixtures
{
	public class Parent
	{
		public string Name { get; set; }

		[JsonDataMemberIgnore]
		public int ID { get; set; }

		public DateTime DOB { get; set; }

		[JsonDataMember("age", FallbackValue = 50, DefaultValue = 5)]
		public int Age { get; set; }

		[JsonDataMember("child")]
		public Child MyChild { get; set; }

		public class Child : Parent
		{

		}
	}
}
