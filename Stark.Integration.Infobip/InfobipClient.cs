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
        private readonly TimeSpan _timeOut;
        private readonly IJsonSerializer _serializer;
        private readonly IPhoneNumberValidator _phoneNumberValidator;

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
                throw new ArgumentNullException("serializer");
            }

            _userName = userName;
            _password = password;
            _timeOut = timeOut;
            _serializer = serializer;
            _phoneNumberValidator = phoneNumberValidator;
        }

        public void Send(List<Message> messages)
        {
            SmsRequest request = new SmsRequest(messages);
            SmsResponse response = Post<SmsResponse>("https://api.infobip.com/sms/1/text/multi", request);
        }

        public decimal GetBalance()
        {
            BalanceDetailResponse response = Get<BalanceDetailResponse>("https://api.infobip.com/account/1/balance");

            decimal balance = 0M;

            if (response != null)
            {
                balance = response.Balance;
            }

            return balance;
        }

        #region Helpers

        private T Post<T>(string url, object payload)
        {
            T defaultResponse = default(T);

            try
            {
                string requestString = _serializer.Serialize(payload);
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

                WebResponse response = request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null && responseStream != Stream.Null)
                    {
                        using (StreamReader sr = new StreamReader(responseStream))
                        {
                            string responseString = sr.ReadToEnd().Trim();
                            defaultResponse = _serializer.Deserialize<T>(responseString);
                            return defaultResponse;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                defaultResponse = default(T);
            }

            return defaultResponse;
        }

        private T Get<T>(string url, Dictionary<string, string> parameters = null)
        {
            T defaultResponse = default(T);

            try
            {
                if (parameters != null && parameters.Count > 0)
                {
                    string query = String.Empty;

                    foreach (KeyValuePair<string, string> pair in parameters)
                    {
                        if (!String.IsNullOrEmpty(query))
                        {
                            query = String.Concat(query, "&");
                        }

                        query = String.Concat(query, pair.Key, "=", pair.Value);
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
                    

                    if (responseStream != null && responseStream != Stream.Null)
                    {
                        using (StreamReader sr = new StreamReader(responseStream))
                        {
                            string responseString = sr.ReadToEnd().Trim();
                            defaultResponse = _serializer.Deserialize<T>(responseString);
                            return defaultResponse;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                defaultResponse = default(T);
            }

            return defaultResponse;
        }

        #endregion
    }
}