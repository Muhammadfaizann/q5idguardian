using System;
using Newtonsoft.Json;

namespace q5id.guardian.Services.Bases
{
    public class AppServiceResponse<T>
    {
        [JsonProperty("statusCode")]
        public bool StatusCode { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }
    }

    public class AppServiceResponse
    {
        [JsonProperty("statusCode")]
        public bool StatusCode { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
