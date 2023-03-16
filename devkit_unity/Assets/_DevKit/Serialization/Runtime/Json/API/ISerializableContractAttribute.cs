namespace DevKit.Serialization.Json.API
{
    public interface ISerializableContractAttribute
    {
        object BeforeSerialization(object value);

        string BeforeDeserialization(string value);

        string AfterSerialization(string value);

        object AfterDeserialization(object value);
    }

}
