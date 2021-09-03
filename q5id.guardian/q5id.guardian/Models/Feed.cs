using System;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class Feed
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("AlertFeedId")]
        public string AlertFeedId { get; set; }
        [JsonProperty("AlertId")]
        public string AlertId { get; set; }
        [JsonProperty("createdby")]
        public string CreatedBy { get; set; }
        [JsonProperty("VolunteerName")]
        public string VolunteerName { get; set; }
        [JsonProperty("Comment")]
        public string Comment { get; set; }
        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; }
        [JsonProperty("Action")]
        public string Action { get; set; }
        [JsonProperty("createdon")]
        public string CreatedOn { get; set; }
        [JsonProperty("modifiedon")]
        public string ModifiedOn { get; set; }

        public DateTime? AddedTime
        {
            get
            {
                if (CreatedOn != null && CreatedOn != "")
                {
                    return DateTime.Parse(CreatedOn);
                }
                return null;
            }
        }
    }
}
