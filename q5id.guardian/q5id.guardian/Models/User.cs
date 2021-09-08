using System;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class User : BaseEntity
    {
        [JsonProperty("accountid")]
        public string AccountId { get; set; }
        [JsonProperty("name")]
        public string AccountName { get; set; }
        [JsonProperty("accountnumber")]
        public string AccountNumber { get; set; }
        [JsonProperty("overriddencreatedon")]
        public string OverriddenCreatedon { get; set; }
        [JsonProperty("lastusedincampaign")]
        public string LastUseDinCampaign { get; set; }
        [JsonProperty("statecode")]
        public string Statecode { get; set; }

        [JsonIgnore]
        public UserRole Role { get; set; }

        public User()
        {
            OverriddenCreatedon = "2001-03-03";
            LastUseDinCampaign = "2001-03-03";
            Statecode = "Active";
        }

        public override object GetParam()
        {
            return new
            {

            };
        }
    }
}
