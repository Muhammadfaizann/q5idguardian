using System;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class User : BaseEntity
    {
        [JsonProperty("entityimage")]
        public string ImageUrl { get; set; }
        [JsonProperty("nickname")]
        public string NickName { get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("fullname")]
        public string FullName { get; set; }
        [JsonProperty("birthdate")]
        public string BirthDate { get; set; }
        [JsonProperty("contactid")]
        public string ContactId { get; set; }
        [JsonProperty("statecode")]
        public string Statecode { get; set; }
        [JsonProperty("anniversary")]
        public string Anniversary { get; set; }
        [JsonProperty("lastusedincampaign")]
        public string Lastusedincampaign { get; set; }
        [JsonProperty("overriddencreatedon")]
        public string Overriddencreatedon { get; set; }

        [JsonIgnore]
        public UserRole Role { get; set; }

        public User()
        {
            Statecode = "Active";
            Anniversary = DateTime.UtcNow.ToString(Utils.Constansts.BIRTHDAY_DATE_FORMAT);
            Lastusedincampaign = DateTime.UtcNow.ToString(Utils.Constansts.BIRTHDAY_DATE_FORMAT);
            Overriddencreatedon = DateTime.UtcNow.ToString(Utils.Constansts.BIRTHDAY_DATE_FORMAT);
        }

        public override object GetParam()
        {
            return new
            {
                entityimage = ImageUrl,
                nickname = NickName,
                firstname = FirstName,
                lastname = LastName,
                birthdate = BirthDate,
                contactid = ContactId,
                anniversary = Anniversary,
                lastusedincampaign = Lastusedincampaign,
                overriddencreatedon = Overriddencreatedon,
            };
        }
    }
}
