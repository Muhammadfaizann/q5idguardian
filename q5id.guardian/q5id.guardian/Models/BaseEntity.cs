using System;
using System.Linq;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public abstract class BaseEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("createdon")]
        public string CreatedOn { get; set; }
        [JsonProperty("modifiedon")]
        public string ModifiedOn { get; set; }
        [JsonProperty("createdby")]
        public string CreatedBy { get; set; }

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

        public DateTime? UpdatedTime
        {
            get
            {
                if (ModifiedOn != null && ModifiedOn != "")
                {
                    return DateTime.Parse(ModifiedOn);
                }
                return null;
            }
        }

        public abstract object GetParam();
    }
}
