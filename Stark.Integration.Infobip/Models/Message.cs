using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Stark.Integration.Infobip.Models
{
    [DataContract]
    public class Message
    {
        [DataMember(Name = "from")]
        public string Originator { get; set; }

        [DataMember(Name = "destinations")]
        public List<Destination> Destinations { get; set; } = new List<Destination>();

        [DataMember(Name = "text")]
        public string Content { get; set; }

        [DataMember(Name = "transliteration")]
        public string Transliteration { get; set; } = "TURKISH";

        [DataMember(Name = "language")]
        public Language Language { get; set; } = new Language();

        [DataMember(Name = "notifyContentType")]
        public string NotifyContentType { get; set; }

        [DataMember(Name = "notifyUrl")]
        public string NotifyUrl { get; set; }

        [DataMember(Name = "callbackData")]
        public string CallbackData { get; set; }

        [DataMember(Name = "notify")]
        public bool Notify { get; set; }

        [DataMember(Name = "validityPeriod")]
        public int ValidityPeriod { get; set; } = 2880;

        public Message(string originator, List<string> numbers, string content)
        {
            if (String.IsNullOrEmpty(originator) || numbers == null || !numbers.Any())
            {
                throw new ArgumentNullException("dodlur meyhaneci");
            }

            Originator = originator;
            Content = content;
            Destinations = numbers.Select(n => new Destination() { Number = n }).ToList();
        }
    }
}
