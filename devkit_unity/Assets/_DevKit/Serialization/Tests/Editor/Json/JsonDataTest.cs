using System;
using System.Linq;
using DevKit.Core.Extensions;
using DevKit.Serialization.Json;
using DevKit.Serialization.Json.API;
using DevKit.Serialization.Tests.Editor.Json.Fixtures;
using NUnit.Framework;
using TMS.Common.Tests.Serialization.Json.TestClasses;

namespace DevKit.Serialization.Tests.Editor.Json
{
	[TestFixture]
	public class JsonDataTest
	{
		#region Fields
		#endregion

		#region Constructors
		#endregion

		#region Tests

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
			const string value = StringResources.TestString;
			var jsonData = new JsonData(value);
			Assert.IsTrue(jsonData.IsString);
		}

		[Test]
		public void JsonData_Conversion_Explicit_String()
		{
			const string value = StringResources.TestString;
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
			var jsonData = JsonMapper.Default.ToObject(StringResources.PersonArray);
			var s = jsonData.ToJson();
			Assert.IsFalse(string.IsNullOrEmpty(s));
		}

		public TestContext TestContext { get; set; }

		[Test]
		public void ReadEmpireJson()
		{
			var str = FileResources.GetJsonTextAsset("_FullEmpireTest").text;
				//File.ReadAllText(@"E:\ws\dev\unity\infr\TMS\TMS\TMS.Common.Test\Serialization\Json\TestResources\_FullEmpireTest.json");

			var time = DateTime.Now;

			var res = JsonMapper.Default.ToObject(str);

			TestContext.WriteLine("Parsing time: " + (DateTime.Now - time).TotalMilliseconds + " ms");

			Assert.IsNotNull(res);
		}

		[Test]
		public void ReadEmpireJsonAsClass()
		{
			var str = FileResources.GetJsonTextAsset("_FullEmpireTest").text;
				//File.ReadAllText(@"E:\ws\dev\unity\infr\TMS\TMS\TMS.Common.Test\Serialization\Json\TestResources\_FullEmpireTest.json");

			var time = DateTime.Now;

			var empire = JsonMapper.Default.ToObject<Empire>(str);

			TestContext.WriteLine("Parsing time: " + (DateTime.Now - time).TotalMilliseconds + " ms");

			Assert.IsNotNull(empire);
		}

		[Test]
		public void ReadEmpireJsonAsDic()
		{
			var str = FileResources.GetJsonTextAsset("_FullEmpireTest").text;

			var time = DateTime.Now;

			var empire = JsonMapper.Default.ToObject(str);

			var hero = JsonMapper.Default.ToObject<Hero>(empire["Hero"]);
			Assert.That(hero, Is.Not.Null);
			//var city = JsonMapper.Default.ToObject<City>(empire["city"]);

			// if (empire["city"][0].IsInt)
			// {
			// 	var id = (int) empire["city"][0];
			// 	id = empire["city"][0].Cast<int>().FirstOrDefault();
			// }

			str = empire.ToJson();

			TestContext.WriteLine("Parsing time: " + (DateTime.Now - time).TotalMilliseconds + " ms");

			Assert.IsNotNull(empire);
		}

		/// <summary>
		/// Writes the empire json as class.
		/// </summary>
		[Test]
		public void WriteEmpireJsonAsClass()
		{
			Empire empire = null;

			var str = FileResources.GetJsonTextAsset("_FullEmpireTest").text;

			if (!str.IsNullOrEmpty())
			{
				empire = JsonMapper.Default.ToObject<Empire>(str);
			}

			var time = DateTime.Now;

			if(time < DateTime.Now) return;


			TestContext.WriteLine("Parsing time: " + (DateTime.Now - time).TotalMilliseconds + " ms");

			str = JsonMapper.Default.ToJson(empire);

			var ary = new JsonData();
			for (int i = 0; i < 20; i++)
			{
				ary.Add(i);
			}

			var data = new JsonData
			{
				{"a", new JsonData
							{
								{"b",  1},
								{"c", "str"}
							}},
				{"ary", ary }
			};
			data.ToJson();

			Assert.IsNotNull(empire);
		}

		[Test]
		public void ReadConfigFileJson()
		{
			var jsonStr = FileResources.GetJsonTextAsset("_Config").text;
			var jsonData = JsonMapper.Default.ToObject(jsonStr);
			Assert.IsNotNull(jsonData);
		}

		[Test]
		public void ReadNetworkConfigFileJson()
		{
			var jsonStr = FileResources.GetJsonTextAsset("_NetworkConfig").text;
			var jsonData = JsonMapper.Default.ToObject(jsonStr);
			Assert.IsNotNull(jsonData);

			var config = JsonMapper.Default.ToObject<NetworkConfig>(jsonStr);
			Assert.IsNotNull(config);
		}

		#endregion
	}
}
