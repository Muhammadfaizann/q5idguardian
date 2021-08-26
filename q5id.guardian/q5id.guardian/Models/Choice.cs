using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class Choice
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("items")]
        public List<ItemChoice> Items { get; set; }
    }

    public class ItemChoice
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("label")]
        public string Name { get; set; }
    }
}
