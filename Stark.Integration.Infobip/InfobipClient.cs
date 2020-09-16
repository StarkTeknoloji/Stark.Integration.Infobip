using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Stark.Integration.Infobip.Models;
using Stark.Integration.Infobip.Models.Requests;
using Stark.Integration.Infobip.Models.Responses;

namespace Stark.Integration.Infobip
{
    public class InfobipClient
    {
        private readonly string _userName;
        private readonly string _password;
        private readonly string _baseUrl;
        private readonly TimeSpan _timeOut;
        private readonly IJsonSerializer _serializer;
        private readonly IPhoneNumberValidator _phoneNumberValidator;
        private readonly IAuditLogger _auditLogger;

        public InfobipClient(string userName, string password)
            : this(userName, password, TimeSpan.FromSeconds(30))
        {
        }

        public InfobipClient(string userName, string password, TimeSpan timeOut)
            : this(userName, password, timeOut, null)
        {
        }

        public InfobipClient(string userName, string password, TimeSpan timeOut, IPhoneNumberValidator phoneNumberValidator)
            : this(userName, password, timeOut, phoneNumberValidator, new SimpleJsonSerializer())
        {
        }

        public InfobipClient(string userName, string password, TimeSpan timeOut, IPhoneNumberValidator phoneNumberValidator, IJsonSerializer serializer)
            : this(userName, password, timeOut, phoneNumberValidator, serializer, new ConsoleAuditLogger())
        {
            
        }

        public InfobipClient(string userName, string password, TimeSpan timeOut, IPhoneNumberValidator phoneNumberValidator, IJsonSerializer serializer, IAuditLogger logger)
            : this(userName, password, timeOut, phoneNumberValidator, serializer, new ConsoleAuditLogger(), "https://api.infobip.com")
        {
            
        }

        public InfobipClient(string userName, string password, TimeSpan timeOut, IPhoneNumberValidator phoneNumberValidator, IJsonSerializer serializer, IAuditLogger logger, string baseUrl)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            if (serializer == null)
            {
                _serializer = new SimpleJsonSerializer();
            }
            else
            {
                _serializer = serializer;
            }

            _userName = userName;
            _password = password;
            _timeOut = timeOut;
            _phoneNumberValidator = phoneNumberValidator;
            _auditLogger = logger;
            _baseUrl = baseUrl;
        }

        public HttpResponse<SmsResponse> Send(List<Message> messages)
        {
            SmsRequest request = new SmsRequest(messages);
            HttpResponse<SmsResponse> response = Post<SmsResponse>($"{_baseUrl.TrimEnd('/')}/sms/1/text/multi", request);
            return response;
        }

        public HttpResponse<DeliveryReportResponse> GetDeliveryReports(string packageId, string messageId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("bulkId", packageId);
            parameters.Add("messageId", messageId);
            parameters.Add("limit", "10000");

            HttpResponse<DeliveryReportResponse> response = Get<DeliveryReportResponse>($"{_baseUrl.TrimEnd('/')}/sms/1/reports", parameters);
            return response;
        }

        public HttpResponse<BalanceDetailResponse> GetBalance()
        {
            HttpResponse<BalanceDetailResponse> response = Get<BalanceDetailResponse>($"{_baseUrl.TrimEnd('/')}/account/1/balance");
            return response;
        }

        #region Helpers

        private HttpResponse<T> Post<T>(string url, object payload)
        {
            HttpResponse<T> result = new HttpResponse<T>();

            string requestString = String.Empty;
            string responseCode = String.Empty;
            string responseString = String.Empty;

            try
            {
                requestString = _serializer.Serialize(payload);
                byte[] byteArray = Encoding.UTF8.GetBytes(requestString);

                WebRequest request = WebRequest.Create(url);
                request.Timeout = (int)_timeOut.TotalMilliseconds;
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Headers.Add("Authorization", String.Join(" ", "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Join(":", _userName, _password)))));

                request.ContentLength = byteArray.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(byteArray, 0, byteArray.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    responseCode = response.StatusCode.ToString();
                    result.StatusCode = response.StatusCode;

                    if (responseStream != null && responseStream != Stream.Null)
                    {
                        using (StreamReader sr = new StreamReader(responseStream))
                        {
                            responseString = sr.ReadToEnd().Trim();
                            result.RawResponse = responseString;
                            result.Data = _serializer.Deserialize<T>(responseString);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Message = ex.Message;

                if (String.IsNullOrEmpty(responseCode))
                {
                    responseCode = "ExceptionCaught";
                }
                
                if (String.IsNullOrEmpty(responseString))
                {
                    responseString = ex.Message;
                }
            }

            _auditLogger.Log("POST", url, requestString, responseCode, responseString);

            return result;
        }

        private HttpResponse<T> Get<T>(string url, Dictionary<string, string> parameters = null)
        {
            HttpResponse<T> result = new HttpResponse<T>();

            string requestString = String.Empty;
            string responseCode = String.Empty;
            string responseString = String.Empty;

            try
            {
                if (parameters != null && parameters.Count > 0)
                {
                    string query = String.Empty;

                    foreach (KeyValuePair<string, string> pair in parameters)
                    {
                        if (!String.IsNullOrEmpty(pair.Value))
                        {
                            if (!String.IsNullOrEmpty(query))
                            {
                                query = String.Concat(query, "&");
                            }

                            query = String.Concat(query, pair.Key, "=", pair.Value);
                        }
                    }

                    url = String.Concat(url, "?", query);
                }

                WebRequest request = WebRequest.Create(url);
                request.Timeout = (int)_timeOut.TotalMilliseconds;
                request.ContentType = "application/json";
                request.Method = "GET";
                request.Headers.Add("Authorization", String.Join(" ", "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Join(":", _userName, _password)))));

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    result.StatusCode = response.StatusCode;

                    if (responseStream != null && responseStream != Stream.Null)
                    {
                        using (StreamReader sr = new StreamReader(responseStream))
                        {
                            responseString = sr.ReadToEnd().Trim();
                            result.RawResponse = responseString;
                            result.Data = _serializer.Deserialize<T>(responseString);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Message = ex.Message;

                if (String.IsNullOrEmpty(responseCode))
                {
                    responseCode = "ExceptionCaught";
                }

                if (String.IsNullOrEmpty(responseString))
                {
                    responseString = ex.Message;
                }
            }

            _auditLogger.Log("GET", url, requestString, responseCode, responseString);

            return result;
        }

        #endregion
    }
}