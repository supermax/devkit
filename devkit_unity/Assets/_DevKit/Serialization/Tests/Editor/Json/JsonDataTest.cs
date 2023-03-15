using DevKit.Serialization.Json;
using DevKit.Serialization.Json.API;
using NUnit.Framework;

namespace DevKit.Serialization.Tests.Editor.Json
{
	[TestFixture]
	public class JsonDataTest
	{
		[Test]
		public void JsonData_State_Int()
		{
			const int value = 273;
			var jsonData = new JsonData(value);
			Assert.IsTrue(jsonData.IsInt);
		}

		[Test]
		public void JsonData_Conversion_Explicit_Int()
		{
			const int value = 273;
			var jsonData = new JsonData(value);
			Assert.AreEqual((int)jsonData, value);
		}

		[Test]
		public void JsonData_State_Double()
		{
			const double value = 34.38;
			var jsonData = new JsonData(value);
			Assert.IsTrue(jsonData.IsDouble);
		}

		[Test]
		public void JsonData_Conversion_Explicit_Double()
		{
			const double value = 34.38;
			var jsonData = new JsonData(value);
			Assert.AreEqual((double)jsonData, value);
		}

		[Test]
		public void JsonData_State_Boolean()
		{
			const bool value = true;
			var jsonData = new JsonData(value);
			Assert.IsTrue(jsonData.IsBoolean);
		}

		[Test]
		public void JsonData_Conversion_Explicit_Boolean()
		{
			const bool value = true;
			var jsonData = new JsonData(value);
			Assert.AreEqual((bool)jsonData, value);
		}

		[Test]
		public void JsonData_State_String()
		{
			const string value = TestConstants.TestString;
			var jsonData = new JsonData(value);
			Assert.IsTrue(jsonData.IsString);
		}

		[Test]
		public void JsonData_Conversion_Explicit_String()
		{
			const string value = TestConstants.TestString;
			var jsonData = new JsonData(value);
			Assert.AreEqual((string)jsonData, value);
		}

		[Test]
		public void JsonData_State_Long()
		{
			const long value = 43324L;
			var jsonData = new JsonData(value);
			Assert.IsTrue(jsonData.IsLong);
		}

		[Test]
		public void JsonData_Conversion_Explicit_Long()
		{
			const long value = 43324L;
			var jsonData = new JsonData(value);
			Assert.AreEqual((long)jsonData, value);
		}

		[Test]
		public void JsonData_Conversion_ToString()
		{
			var jsonData = JsonMapper.Default.ToObject(TestConstants.PersonArray);
			var s = jsonData.ToJson();
			Assert.IsFalse(string.IsNullOrEmpty(s));
		}

		[Test]
		public void JsonData_SimpleObject()
		{
			var jsonStr = FileHelper.GetJsonTextAsset("Config").text;
			var jsonData = JsonMapper.Default.ToObject(jsonStr);
			Assert.IsNotNull(jsonData);
		}

		[Test]
		public void JsonData_ComplexObject()
		{
			var jsonStr = FileHelper.GetJsonTextAsset("NetworkConfig").text;
			var jsonData = JsonMapper.Default.ToObject(jsonStr);
			Assert.IsNotNull(jsonData);
		}
	}
}
