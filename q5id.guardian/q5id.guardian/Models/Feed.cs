using System;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class Feed : BaseEntity
    {
        [JsonProperty("AlertFeedId")]
        public string AlertFeedId { get; set; }
        [JsonProperty("AlertId")]
        public string AlertId { get; set; }
        [JsonProperty("VolunteerName")]
        public string VolunteerName { get; set; }
        [JsonProperty("Comment")]
        public string Comment { get; set; }
        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; }
        [JsonProperty("Latitude")]
        public string Latitude { get; set; }
        [JsonProperty("Lognitude")]
        public string Lognitude { get; set; }
        [JsonProperty("Action")]
        public string Action { get; set; }

        public override object GetParam()
        {
            return new
            {
                createdon = CreatedOn,
                modifiedon = ModifiedOn,
                createdby = CreatedBy,
                AlertFeedId = AlertFeedId,
                alertid = AlertId,
                VolunteerName = VolunteerName,
                Comment = Comment,
                Timestamp = Timestamp,
                Latitude = Latitude,
                Lognitude = Lognitude,
                Action = Action,
            };
        }
    }
}
