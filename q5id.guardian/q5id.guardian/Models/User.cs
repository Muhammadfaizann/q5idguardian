using System;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("accountid")]
        public string AccountId { get; set; }
        [JsonProperty("name")]
        public string AccountName { get; set; }
        [JsonProperty("overriddencreatedon")]
        public string OverriddenCreatedon { get; set; }
        [JsonProperty("lastusedincampaign")]
        public string LastUseDinCampaign { get; set; }
        [JsonProperty("statecode")]
        public string Statecode { get; set; }
        [JsonProperty("createdon")]
        public string CreatedOn { get; set; }
        [JsonProperty("modifiedon")]
        public string ModifiedOn { get; set; }

        public UserRole Role { get; set; }

        public User()
        {
            OverriddenCreatedon = "2001-03-03";
            LastUseDinCampaign = "2001-03-03";
            Statecode = "Active";
        }
    }
}
