using System;
using DevKit.Serialization.Json.API;

namespace DevKit.Serialization.Tests.Editor.Json.Fixtures
{
    [JsonDataContract]
    public abstract class BaseDataObj
    {
        [JsonDataMember(Name = "error")]
        public string Error { get; set; }

		public bool IsErroneous()
        {
            if (Error == null)
                return false;

            return !Error.Equals(String.Empty);
        }
    }
}
