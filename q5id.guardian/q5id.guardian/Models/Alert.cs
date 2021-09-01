using System;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;

namespace q5id.guardian.Models
{
    public class Alert
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("ProfileId")]
        public string ProfileId { get; set; }
        [JsonProperty("AlertId")]
        public string AlertId { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Comments")]
        public string Comments { get; set; }
        [JsonProperty("Latitude")]
        public string Latitude { get; set; }
        [JsonProperty("Lognitude")]
        public string Lognitude { get; set; }
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

        public string Title { get; set; }

        public string Address { get; set; }

        public Position Position { get; set; }

        public Love Love { get; set; }

        public string UpdatedTimeDescription
        {
            get
            {
                if (AddedTime == null)
                {
                    return "";
                }
                String mResult = "Started ";
                DateTime TimeToCheck = (DateTime)AddedTime;
                return mResult + Utils.Utils.GetTimeAgoFrom(TimeToCheck);
            }
        }

    }
}
