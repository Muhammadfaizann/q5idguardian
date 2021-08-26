using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class Structure
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("entities")]
        public List<StrutureEntity> Entities { get; set; }
    }

    public class StrutureEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("entityName")]
        public string EntityName { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}
