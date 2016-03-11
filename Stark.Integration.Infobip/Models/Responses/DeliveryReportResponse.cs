using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Stark.Integration.Infobip.Models.Responses
{
    [DataContract]
    public class DeliveryReportResponse
    {
        [DataMember(Name = "results")]
        public List<DeliveryReportResponseDetail> Details { get; set; }
    }
}