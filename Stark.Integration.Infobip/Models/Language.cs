using System.Runtime.Serialization;

namespace Stark.Integration.Infobip.Models
{
    [DataContract]
    public class Language
    {
        [DataMember(Name = "languageCode")]
        public string LanguageCode { get; set; }

        [DataMember(Name = "singleShift")]
        public bool SingleShift { get; set; }

        [DataMember(Name = "lockingShift")]
        public bool LockingShift { get; set; }
    }
}