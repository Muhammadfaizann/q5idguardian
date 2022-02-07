using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace q5id.guardian.Models
{
    public class Alert : BaseEntity
    {
        [JsonProperty("ProfileId")]
        public string ProfileId { get; set; }
        [JsonProperty("UserId")]
        public string UserId { get; set; }
        [JsonProperty("AlertId")]
        public string AlertId { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Comments")]
        public string Comments { get; set; }
        [JsonProperty("Latitude")]
        public double Latitude { get; set; }
        [JsonProperty("Longitude")]
        public double Longitude { get; set; }
        [JsonProperty("Address")]
        public string Address { get; set; }
        [JsonProperty("IsClosed")]
        public bool IsClosed { get; set; }
        [JsonProperty("alertFeeds")]
        public List<Feed> AlertFeeds { get; set; }
        [JsonProperty("profile")]
        public Love Love { get; set; }
        public Data data { get; set; }

        [JsonIgnore]
        public string DistanceFromUser { get; set; }

        public static string GetDistanceFrom(Alert alert, Location sourceCoordinates)
        {
            if(sourceCoordinates != null)
            {
                try
                {
                    Location destinationCoordinates = new Location(alert.Latitude, alert.Longitude);
                    double distance = Location.CalculateDistance(sourceCoordinates, destinationCoordinates, DistanceUnits.Miles);
                    return String.Format("{0:0.00} miles", distance);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("Cannot get alert location: ", ex);
                }
                
            }
            return "Unkown";
        }

        public override object GetParam()
        {
            return new
            {
                CreatedOn = CreatedOn,
                ModifiedOn = ModifiedOn,
                CreatedBy = CreatedBy,
                ProfileId = ProfileId,
                AlertId = AlertId,
                UserId = UserId,
                FirstName = FirstName,
                Description = Description,
                Comments = Comments,
                Latitude = Latitude,
                Longitude = Longitude,
                Address = Address,
                IsClosed = IsClosed,
            };
        }

        [JsonIgnore]
        public Position Position
        {
            get
            {
                return new Position(Latitude, Longitude);
            }
        }

        [JsonIgnore]
        public string UpdatedTimeDescription
        {
            get
            {
                if (AddedTime == null)
                {
                    return "";
                }
                String mResult = "Started ";
                DateTime TimeToCheck = (DateTime)AddedTime;
                return mResult + Utils.Utils.GetTimeAgoFrom(TimeToCheck);
            }
        }

        [JsonIgnore]
        public bool IsEnded
        {
            get
            {
                return this.IsClosed;
            }
        }

    }

    class AlertComparer : IEqualityComparer<Alert>
    {
        public bool Equals(Alert x, Alert y)
        {
            if (x.Id == y.Id)
                return true;

            return false;
        }

        public int GetHashCode(Alert obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public class Data
    {
        public string profileId { get; set; }
        public string userId { get; set; }
        public string alertId { get; set; }
        public string firstName { get; set; }
        public string description { get; set; }
        public string comments { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string address { get; set; }
        public bool isClosed { get; set; }
        public string photo { get; set; }
        public object timestamp { get; set; }
        public object alertFeeds { get; set; }
        public object profile { get; set; }
        public string rapidSOSId { get; set; }
        public object id { get; set; }
        public object documentTypeInstanceId { get; set; }
        public object dataVaultId { get; set; }
        public object entityId { get; set; }
        public string createdBy { get; set; }
        public string createdOn { get; set; }
        public object modifiedOn { get; set; }
        public string modifiedBy { get; set; }
        public DateTime addedTime { get; set; }
        public object updatedTime { get; set; }
    }

}
