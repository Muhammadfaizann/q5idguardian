using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace q5id.guardian.Models
{
    public class AuthResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("statusUri")]
        public string StatusUri { get; set; }
    }
}
