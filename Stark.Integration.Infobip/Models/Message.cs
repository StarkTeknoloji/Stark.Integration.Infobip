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

        [DataMember(Name = "to")]
        public List<string> Numbers { get; set; }

        [DataMember(Name = "text")]
        public string Content { get; set; }

        public Message(string originator, List<string> numbers, string content)
        {
            if (String.IsNullOrEmpty(originator) || numbers == null || !numbers.Any())
            {
                throw new ArgumentNullException("dodlur meyhaneci");
            }

            Originator = originator;
            Numbers = numbers;
            Content = content;
        }
    }
}
