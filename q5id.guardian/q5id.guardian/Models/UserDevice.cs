using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class UserDevice : BaseEntity
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }
        [JsonProperty("devicePushId")]
        public string DevicePushId { get; set; }
        [JsonProperty("platform")]
        public string Platform { get; set; }
        [JsonProperty("isDeleted")]
        public Boolean IsDeleted { get; set; }
        [JsonProperty("isAppPurchaseToken")]
        public string IsAppPurchaseToken { get; set; }
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }
        [JsonProperty("userDeviceId")]
        public string UserDeviceId { get; set; }
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("DeviceUUID")]
        public string DeviceUUID { get; set; }

        public UserDevice()
        {
        }

        public override object GetParam()
        {
            return new
            {
                userId = UserId,
                subscriptionId = SubscriptionId,
                DevicePushId = DevicePushId,
                platform = Platform,
                isDeleted = IsDeleted,
                UserId = UserId,
                DeviceId = DeviceId,
                DeviceUUID = DeviceUUID,
                Tags = Tags,
                Latitude = Latitude,
                Longitude = Longitude
            };
        }
    }
}
