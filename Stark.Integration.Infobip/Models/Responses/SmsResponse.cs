using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Stark.Integration.Infobip.Models.Responses
{
    [DataContract]
    public class SmsResponse
    {
        [DataMember(Name = "bulkId")]
        public string PackageId { get; set; }

        [DataMember(Name = "messages")]
        public List<SmsResponseDetail> Details { get; set; }
    }
}