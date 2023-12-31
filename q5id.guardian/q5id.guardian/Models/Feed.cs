﻿using System;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class Feed : BaseEntity
    {
        [JsonProperty("AlertFeedId")]
        public string AlertFeedId { get; set; }
        [JsonProperty("UserId")]
        public string UserId { get; set; }
        [JsonProperty("AlertId")]
        public string AlertId { get; set; }
        [JsonProperty("VolunteerName")]
        public string VolunteerName { get; set; }
        [JsonProperty("Comment")]
        public string Comment { get; set; }
        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; }
        [JsonProperty("Latitude")]
        public double Latitude { get; set; }
        [JsonProperty("Longitude")]
        public double Longitude { get; set; }
        [JsonProperty("Action")]
        public string Action { get; set; }

        public override object GetParam()
        {
            return new
            {
                CreatedOn = CreatedOn,
                ModifiedOn = ModifiedOn,
                CreatedBy = CreatedBy,
                AlertFeedId = AlertFeedId,
                UserId = UserId,
                AlertId = AlertId,
                VolunteerName = VolunteerName,
                Comment = Comment,
                Timestamp = Timestamp,
                Latitude = Latitude,
                Longitude = Longitude,
                Action = Action,
            };
        }
    }
}
