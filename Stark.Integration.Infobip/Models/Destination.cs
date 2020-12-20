using System.Runtime.Serialization;

namespace Stark.Integration.Infobip.Models
{
    [DataContract]
    public class Destination
    {
        [DataMember(Name = "to")]
        public string Number { get; set; }
    }
}