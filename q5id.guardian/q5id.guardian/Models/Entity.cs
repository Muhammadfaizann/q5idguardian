using System;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class Entity<T>
    {
        [JsonProperty("entityId")]
        public string EntityId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }

    public class EntityResponse<T>
    {
        [JsonProperty("entity")]
        public Entity<T> Entity { get; set; }

        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
