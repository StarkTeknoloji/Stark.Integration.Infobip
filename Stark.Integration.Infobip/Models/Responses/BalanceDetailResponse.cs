using System.Runtime.Serialization;

namespace Stark.Integration.Infobip.Models.Responses
{
    [DataContract]
    public class BalanceDetailResponse
    {
        [DataMember(Name = "balance")]
        public decimal Balance { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }
    }
}