namespace Stark.Integration.Infobip
{
    public interface IAuditLogger
    {
        void Log(string requestType, string requestUrl, string requestPayload, string responseCode, string responsePayload);
    }
}