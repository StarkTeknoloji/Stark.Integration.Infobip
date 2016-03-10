using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Stark.Integration.Infobip.Models.Requests
{
    [DataContract]
    public class SmsRequest
    {
        [DataMember(Name = "messages")]
        public List<Message> Messages { get; set; }

        public SmsRequest(List<Message> messages)
        {
            Messages = messages;
        }
    }
}