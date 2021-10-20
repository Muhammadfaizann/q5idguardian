using System;
using Newtonsoft.Json;

namespace q5id.guardian.Services.Bases
{
    public class AppServiceResponse<T>
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }

        [JsonProperty("isError")]
        public Boolean IsError { get; set; }

        [JsonProperty("responseException")]
        public object ResponseException { get; set; }

        [JsonProperty("message")]
        public String Message { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }

    public class AppServiceResponse
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("isError")]
        public Boolean IsError { get; set; }

        [JsonProperty("responseException")]
        public object ResponseException { get; set; }

        [JsonProperty("message")]
        public String Message { get; set; }

        [JsonProperty("error")]
        public String Error { get; set; }
    }
}
