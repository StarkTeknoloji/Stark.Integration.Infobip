using System.Runtime.Serialization;

namespace Stark.Integration.Infobip.Models.Responses
{
    [DataContract]
    public class SmsResponseDetail
    {
        [DataMember(Name = "messageId")]
        public string MessageId { get; set; }

        [DataMember(Name = "to")]
        public string Number { get; set; }

        [DataMember(Name = "smsCount")]
        public int SmsCount { get; set; }

        [DataMember(Name = "status")]
        public Status Status { get; set; }
    }
}
