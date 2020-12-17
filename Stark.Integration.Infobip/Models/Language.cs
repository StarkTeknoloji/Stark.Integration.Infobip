using System.Runtime.Serialization;

namespace Stark.Integration.Infobip.Models
{
    [DataContract]
    public class Language
    {
        [DataMember(Name = "languageCode")]
        public string LanguageCode { get; set; } = "TR";

        [DataMember(Name = "singleShift")]
        public bool SingleShift { get; set; } = false;

        [DataMember(Name = "lockingShift")]
        public bool LockingShift { get; set; } = true;
    }
}