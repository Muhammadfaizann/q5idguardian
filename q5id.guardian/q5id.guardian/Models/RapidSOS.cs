using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace q5id.guardian.Models
{
    public class RapidSOSRequest
    {
        [JsonProperty("alertId")]
        public string AlertId { get; set; }
    }

    public class RapidSOSResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
