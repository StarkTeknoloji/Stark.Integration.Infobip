﻿using System.Runtime.Serialization;

namespace Stark.Integration.Infobip.Models.Responses
{
    [DataContract]
    public class Status
    {
        [DataMember(Name = "groupId")]
        public int GroupId { get; set; }

        [DataMember(Name = "groupName")]
        public string GroupName { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "action")]
        public string Action { get; set; }
    }
}