#region

using System;
using System.Collections.Generic;
using DevKit.Serialization.Json;
using DevKit.Serialization.Json.API;
using DevKit.Serialization.Tests.Editor.Json.Fixtures;
using NUnit.Framework;

#endregion

namespace DevKit.Serialization.Tests.Editor.Json
{
	[TestFixture]
	public sealed class JsonMapperTest
	{
		#region Fields

		private string _jsonTest;

		#endregion

		#region Methods

		[OneTimeSetUp]
		public void Startup()
		{
			_jsonTest = FileHelper.GetJsonTextAsset("JsonTest").text;
		}

		#endregion

		#region Tests

		[Test]
		public void JsonMapper_State_SingletonProperty_NotNull()
		{
			var mapper = JsonMapper.Default;
			Assert.IsNotNull(mapper);
		}

		[Test]
		public void JsonMapper_Conversion_ClassProperties_Setting()
		{
			const string name = "person_name";
			const string surname = "person_surname";
			const int age = 27;

			var personJsonString = TestConstants.GetPerson(name, surname, age);
			var jsonDataPerson = JsonMapper.Default.ToObject(personJsonString);
			Assert.NotNull(jsonDataPerson);
			Assert.AreEqual(name, jsonDataPerson["firstName"].GetPrimitiveValue());
			Assert.AreEqual(surname, jsonDataPerson["lastName"].GetPrimitiveValue());

			var ageVal = jsonDataPerson["age"];
			Assert.True(ageVal.IsInt);
			Assert.AreEqual(age, ageVal.GetPrimitiveValue());

			var person = JsonMapper.Default.ToObject<Person>(jsonDataPerson);
			Assert.NotNull(person);
			Assert.AreEqual(name, person.Name);
			Assert.AreEqual(surname, person.Surname);
			Assert.AreEqual(age, person.Age);

			jsonDataPerson = JsonMapper.Default.ToJson(person);
			Assert.NotNull(jsonDataPerson);

			var jd = new JsonData
			{
				{"name", "New Name"},
				{"age", 1},
				{"person", jsonDataPerson}, // this should be ignored when serialized to Person
				//{"arr", new [] {1, 2, 3}} // TODO fix array init in JsonData
			};

			var jsonStr = jd.ToJson();
			Assert.NotNull(jsonStr);

			var newPerson = JsonMapper.Default.ToObject<Person>(jsonStr);
			Assert.NotNull(newPerson);
			Assert.AreEqual(jd["name"].GetPrimitiveValue(), newPerson.Name);
			Assert.AreEqual(jd["age"].GetPrimitiveValue(), newPerson.Age);
		}

		[Test]
		public void JsonMapper_Conversion_GetJsonDataObjectFromString_NotNull()
		{
			var person = JsonMapper.Default.ToObject(TestConstants.Person);
			Assert.IsNotNull(person);
		}

		[Test]
		public void JsonMapper_Conversion_JsonDataObjectToSpecifiedType_NotNull()
		{
			var jsonDataPerson = JsonMapper.Default.ToObject(TestConstants.Person);
			var person = JsonMapper.Default.ToObject<Person>(jsonDataPerson);
		}

		[Test]
		public void JsonMapper_Conversion_GetArrayOfJsonData_WithRightCount()
		{
			var persons = JsonMapper.Default.ToObject(TestConstants.PersonArray);
			Assert.IsNotNull(persons);
			Assert.AreEqual(persons.Count, TestConstants.PersonsArrayCount);
		}

		[Test]
		public void JsonMapper_Conversion_CountOfObjectsProperties_EqualsToTypesPropertiesCount()
		{
			var jsonDataPerson = JsonMapper.Default.ToObject(TestConstants.Person);
			Assert.AreEqual(TestConstants.PersonPropertiesCount, jsonDataPerson.Count);
		}

		[Test]
		public void JsonMapper_Conversion_ArrayOfJsonData_ToSpecifiedType_RightElementCount()
		{
			var jsonDataPersons = JsonMapper.Default.ToObject(TestConstants.PersonArray);
			var persons = JsonMapper.Default.ToObject<Person[]>(jsonDataPersons);
			Assert.AreEqual(jsonDataPersons.Count, persons.Length);
		}

		[Test]
		public void JsonMapper_Conversion_LoginDataFormatToJsonDataObject()
		{
			var loginJsonData = JsonMapper.Default.ToObject(_jsonTest);
			Assert.IsNotNull(loginJsonData);
		}

		[Test]
		public void JsonMapper_Conversion_LoginDataFormatToClass()
		{
			JsonMapper.Default.RegisterImporter<IList<UserLoginData>,List<UserLoginData>>(
				lst =>
					new List<UserLoginData>(lst));
			var obj = JsonMapper.Default.ToObject<UserLoginData>(_jsonTest);
			Assert.IsNotNull(obj);
		}

		[Test]
		public void JsonMapper_Conversion_Subclasses_Initialized()
		{
			var jsonStr = FileHelper.GetJsonTextAsset("Parent").text;
			var parent = JsonMapper.Default.ToObject<Parent>(jsonStr);

			Assert.IsNotNull(parent);
			Assert.IsNotNull(parent.MyChild);
		}

		[Test]
		public void JsonMapper_Conversion_LoginDataFormat()
		{
			var loginJsonData = JsonMapper.Default.ToObject(_jsonTest);
			Assert.AreEqual(12, loginJsonData.Count);

			var o1 = loginJsonData[0];
			var o2 = loginJsonData[1];

			var id = JsonMapper.Default.ToObject<string>(o1);
			var name = JsonMapper.Default.ToObject<string>(o2);

			Assert.IsNotNull(id);
			Assert.IsNotNull(name);
		}

		internal class SomeClass
		{
			public DateTime date { get; set; }

			[JsonDataMember("processTime")]
			public long MyProcessTime { get; set; }

			public bool sucess { get; set; }

			public SomeErrorClass error { get; set; }

			public int serverMessage { get; set; }
		}

		internal class SomeErrorClass
		{
			public string error { get; set; }
		}

		[Test]
		public void JsonMapper_Deserialize_NullObject()
		{
			const string jsonStr = "{\"date\":\"2015-03-20 03:45:45\", \"processTime\":7, \"success\":true, \"error\":null, \"serverMessage\":0}";
			var obj = JsonMapper.Default.ToObject<SomeClass>(jsonStr);
			Assert.IsNotNull(obj);

			var jsonObj = JsonMapper.Default.ToObject(jsonStr);
			Assert.IsNotNull(jsonObj);
		}

		[Test]
		public void JsonMapper_Deserialize_Convert()
		{
			//JsonMapper.Default.RegisterImporter();
		}

//		public void TestDeserialize()
//		{
//			//const string jstr = "{\"name\": \"maxim\", \"id\": 1, \"num\": 0.123, \"another\" : 234, \"subsub\" : {\"num1\":10, \"str1\" : \"hello\"}, \"sub\": { \"num\": 0.00054, \"parent\": 2 }, \"lastint\": 11}";

//			var jObj = JsonMapper.Default.ToObject(StringResources.JsonClass);
//			Assert.IsNotNull(jObj);

//			//var r1 = JsonMapper.Default.ReadValue(typeof(JsonClass), jObj);
//			var r1 = JsonMapper.Default.ToObject<JsonClass>(jObj);
//			Assert.IsNotNull(r1);

//			//const string jclassarray = "[{\"name\":\"pavel\", \"sorname\":\"rumyancev\", \"age\" : 27, \"numbers\" : [1,2,3,4,5,4,3,21]}, {\"name\":\"marina\", \"sorname\":\"rumyancev\", \"age\" : 25, \"numbers\" : [9,8,7,6,5,4,3,2,1]}]";
//			var jClassArrayObject = JsonMapper.Default.ToObject(StringResources.PersonArray);
//			var r2 = JsonMapper.Default.ToObject<Person[]>(jClassArrayObject);
//			Assert.IsNotNull(r2);

//			var obj = JsonMapper.Default.ToObject<JsonClass>(StringResources.JsonClass);
//			Assert.IsNotNull(obj);

//			var obj1 = JsonMapper.Default.ToObject<JsonClass>(jObj);
//			Assert.IsNotNull(obj1);


//			//const string jperson = "{\"name\":\"pavel\", \"sorname\":\"rumyancev\", \"age\" : 27}";
//			var jObj2 = JsonMapper.Default.ToObject(StringResources.Person);
//			var person = JsonMapper.Default.ToObject<Person>(jObj2);

//			var j = JsonMapper.Default.ToJson(person);
//			var j2 = JsonMapper.Default.ToJson(r2);

//			const string jarray = "[\"one\",\"two\",\"three\",\"four\"]";
//			var jObj3 = JsonMapper.Default.ToObject(jarray);
//			var jObj4 = JsonMapper.Default.ToObject<string[]>(jObj3);

//			var res = JsonMapper.Default.ToObject<MachinesObj>(StringResources.JsonClass);
//			Assert.IsNotNull(res);

//			var res1 = JsonMapper.Default.ToObject(StringResources.JsonClass);
//			var jObj5 = JsonMapper.Default.ToObject<MachinesObj>(res1);


//			var aryJstr = File.ReadAllText(@"r:\winity\Infr\diwip.Infr\TMS.Common.Test\Serialization\Json\TestClasses\SlotsClubLogin.txt");
//			var jObj1 = JsonMapper.Default.ToObject(aryJstr);
//			var o1 = jObj1[0];
//			var o2 = jObj1[1];

//			var resultO1 = JsonMapper.Default.ToObject<SpinResultObj>(o1);
//			var resultO2 = JsonMapper.Default.ToObject<UserCurrencyObj>(o2);
////--

//			//var a1 = JsonMapper.ToObject<SpinResultObj>(o1);

//			//var emptyAry = JsonMapper.ToObject<SpinResultObj>(aryJstr);
//			//Assert.IsNotNull(emptyAry);

//			////var types = new Dictionary<string, Type>
//			////{
//			////	{"spin", typeof (SpinResultObj)},
//			////	{"GetUserCurrency", typeof (UserCurrencyObj)}
//			////};

//			////var wrapper = JsonMapper.ToObject(aryJstr);
//			////foreach (IJsonWrapper item in wrapper)
//			////{
//			////	if (!((IDictionary)item).Contains("cmd")) continue;

//			////	var cmd = ((IJsonWrapper)item["cmd"]).GetString();
//			////	var objType = types[cmd];

//			////	var json = wrapper.ToJson();

//			////	var reader = new JsonReader(json);
//			////	var res = JsonMapper.ReadValue(objType, reader);
//			////}

//			//var reader = new JsonReader(aryJstr);
//			//while (reader.Read())
//			//{
//			//	switch (reader.Token)
//			//	{
//			//		case JsonToken.ObjectEnd:
//			//			break;
//			//	}
//			//}
//		}

		#endregion
	}
}
