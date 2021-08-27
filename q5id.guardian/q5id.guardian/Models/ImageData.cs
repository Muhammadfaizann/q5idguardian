using System;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class ImageData
    {
        public byte[] ImageByteArray { get; set; }
        public string Extension { get; set; }

        public string GetBase64Data()
        {
            if(ImageByteArray != null)
            {
                return Convert.ToBase64String(ImageByteArray);
            }
            return null;
        }
    }

    public class ImageResponse
    {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
