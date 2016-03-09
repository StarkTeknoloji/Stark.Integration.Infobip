namespace Stark.Integration.Infobip
{
    public interface IPhoneNumberValidator
    {
        bool IsValid(string number);
    }
}