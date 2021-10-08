﻿using System;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class User : BaseEntity
    {
        [JsonProperty("Entityimage")]
        public string ImageUrl { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("Phone")]
        public string Phone { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [JsonProperty("fullname")]
        public string FullName { get; set; }
        [JsonProperty("UserId")]
        public string UserId { get; set; }
        [JsonProperty("Subscriptionexpireddate")]
        public string SubscriptionExpiredDate { get; set; }
        [JsonIgnore]
        public UserRole Role
        {
            get
            {
                if(SubscriptionExpiredDateTime != null)
                {
                    return SubscriptionExpiredDateTime.Value > DateTime.UtcNow ? UserRole.Subscriber : UserRole.Volunteer;
                }
                return UserRole.Volunteer;
            }
        }

        [JsonIgnore]
        public DateTime? SubscriptionExpiredDateTime
        {
            get
            {
                if (SubscriptionExpiredDate != null && SubscriptionExpiredDate != "")
                {
                    return DateTime.Parse(SubscriptionExpiredDate);
                }
                return null;
            }
        }

        public User()
        {
        }

        public override object GetParam()
        {
            return new
            {
                Entityimage = ImageUrl,
                Email = Email,
                Phone = Phone,
                FirstName = FirstName,
                LastName = LastName,
                UserId = UserId,
                Subscriptionexpireddate = SubscriptionExpiredDate
            };
        }
    }
}
