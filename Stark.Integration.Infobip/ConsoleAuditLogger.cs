using System;

namespace Stark.Integration.Infobip
{
    public class ConsoleAuditLogger : IAuditLogger
    {
        public void Log(string requestType, string requestUrl, string requestPayload, string responseCode, string responsePayload)
        {
            Console.WriteLine("RequestType: {0}\nRequestUrl: {1}\nRequestPayload: {2}\nResponseCode: {3}\nResponsePayload: {4}", requestType, requestUrl, requestPayload, responseCode, responsePayload);
        }
    }
}