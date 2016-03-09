namespace Stark.Integration.Infobip
{
    public interface IJsonSerializer
    {
        string Serialize(object data);

        T Deserialize<T>(string serializedString);
    }
}