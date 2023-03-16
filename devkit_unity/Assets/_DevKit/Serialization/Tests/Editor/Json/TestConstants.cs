namespace DevKit.Serialization.Tests.Editor.Json
{
	internal static class TestConstants
	{
		#region Properties

		#region Person[]

		public const string PersonArray =
			"[{\"firstName\":\"Name 1\", \"lastName\":\"Last Name 1\", \"age\" : 27, \"numbers\" : [1, 2, 3, 4, 5, 4, 3, 21]}, " +
			"{\"firstName\":\"Name 2\", \"lastName\":\"lastName 2\", \"age\" : 25, \"numbers\" : [9, 8, 7, 6, 5, 4, 3, 2, 1]}]";

		public const int PersonsArrayCount = 2;

		#endregion

		#region Person

		public const string Person = "{\"firstName\":\"First Name\", \"lastName\":\"Last Name\", \"age\" : 120, \"numbers\": [1, 2, 3]}";

		public const int PersonPropertiesCount = 4;

		#endregion

		#region String

		public const string TestString = "$afddf-32wd;=jmjn";

		#endregion

		#endregion

		#region Methods

		public static string GetPerson(string name, string surname, int age)
		{
			return $"{{ \"firstName\": \"{name}\", \"lastName\": \"{surname}\", \"age\": {age} }}";
		}

		#endregion
	}
}
