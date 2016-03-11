using System.Net;

namespace Stark.Integration.Infobip.Models.Responses
{
    public class HttpResponse<T> : HttpResponse
    {
        public T Data { get; set; }
    }

    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public string RawResponse { get; set; }
    }
}