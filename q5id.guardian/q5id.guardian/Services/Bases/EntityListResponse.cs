using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace q5id.guardian.Services.Bases
{

    public class EntityListResponse<T>
    {
        [JsonProperty("@odata.context")]
        public string DataContext { get; set; }

        [JsonProperty("value")]
        public List<T> Value { get; set; }
    }

    public class EntityListResponse
    {
        [JsonProperty("@odata.context")]
        public string DataContext { get; set; }
    }
}
