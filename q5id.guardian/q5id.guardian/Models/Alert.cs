using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace q5id.guardian.Models
{
    public class Alert
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("ProfileId")]
        public string ProfileId { get; set; }
        [JsonProperty("AlertId")]
        public string AlertId { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Comments")]
        public string Comments { get; set; }
        [JsonProperty("Latitude")]
        public string Latitude { get; set; }
        [JsonProperty("Lognitude")]
        public string Lognitude { get; set; }
        [JsonProperty("Address")]
        public string Address { get; set; }
        [JsonProperty("IsClosed")]
        public string IsClosed { get; set; }
        [JsonProperty("createdon")]
        public string CreatedOn { get; set; }
        [JsonProperty("modifiedon")]
        public string ModifiedOn { get; set; }
        

        public string DistanceFromUser { get; set; }

        public static string GetDistanceFrom(Alert alert, Location sourceCoordinates)
        {
            if(sourceCoordinates != null && alert.Lognitude != "" && alert.Latitude != "")
            {
                try
                {
                    Location destinationCoordinates = new Location(double.Parse(alert.Latitude), double.Parse(alert.Lognitude));
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

        public DateTime? AddedTime
        {
            get
            {
                if (CreatedOn != null && CreatedOn != "")
                {
                    return DateTime.Parse(CreatedOn);
                }
                return null;
            }
        }

        public string Title { get; set; }

        public Position Position
        {
            get
            {
                if(Latitude != "" && Lognitude != "")
                {
                    try
                    {
                        return new Position(double.Parse(Latitude), double.Parse(Lognitude));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Cannot get alert location: ", ex);
                    }
                }
                return new Position(0, 0);
            }
        }

        public Love Love { get; set; }

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

    }
}
