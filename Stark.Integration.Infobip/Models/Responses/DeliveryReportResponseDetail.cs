using System;
using System.Runtime.Serialization;

namespace Stark.Integration.Infobip.Models.Responses
{
    [DataContract]
    public class DeliveryReportResponseDetail
    {
        [DataMember(Name = "messageId")]
        public string MessageId { get; set; }

        [DataMember(Name = "bulkId")]
        public string PackageId { get; set; }

        [DataMember(Name = "to")]
        public string Number { get; set; }

        [DataMember(Name = "smsCount")]
        public int SmsCount { get; set; }

        [DataMember(Name = "status")]
        public Status Status { get; set; }

        [DataMember(Name = "sentAt")]
        public DateTime CreatedOn { get; set; }

        [DataMember(Name = "doneAt")]
        public DateTime DeliveredOn { get; set; }

        [DataMember(Name = "mccMnc")]
        public string OperatorId { get; set; }

        [DataMember(Name = "error")]
        public Error Error { get; set; }
    }
}