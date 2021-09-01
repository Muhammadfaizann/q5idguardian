using System;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class Love
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("ProfileId")]
        public string ProfileId { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [JsonProperty("DateofBirth")]
        public string DateofBirth { get; set; }
        [JsonProperty("HairColor")]
        public string HairColor { get; set; }
        [JsonProperty("EyeColor")]
        public string EyeColor { get; set; }
        [JsonProperty("Weight")]
        public string Weight { get; set; }
        [JsonProperty("HeightFeet")]
        public string HeightFeet { get; set; }
        [JsonProperty("HeightInches")]
        public string HeightInches { get; set; }
        [JsonProperty("OtherInformation")]
        public string OtherInformation { get; set; }
        [JsonProperty("Image")]
        public string Image { get; set; }
        [JsonProperty("Image2")]
        public string Image2 { get; set; }
        [JsonProperty("Image3")]
        public string Image3 { get; set; }
        [JsonProperty("Image4")]
        public string Image4 { get; set; }
        [JsonProperty("Image5")]
        public string Image5 { get; set; }
        [JsonProperty("createdon")]
        public string CreatedOn { get; set; }
        [JsonProperty("modifiedon")]
        public string ModifiedOn { get; set; }

        public Love()
        {
           
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


        public DateTime? UpdatedTime
        {
            get
            {
                if (ModifiedOn != null && ModifiedOn != "")
                {
                    return DateTime.Parse(ModifiedOn);
                }
                return null;
            }
        }
        public Boolean IsLongTime { get; set; } = false;
        public DateTime? BirthDay
        {
            get
            {
                if(DateofBirth != null && DateofBirth != "")
                {
                    return DateTime.Parse(DateofBirth);
                }
                return null;
            }
        }

        public string FullName
        {
            get => FirstName + " " + LastName;
        }

        public int Age
        {
            get
            {
                if (BirthDay.HasValue)
                {
                    TimeSpan timeSpanToBirthDay = DateTime.Now.Subtract(BirthDay.Value);
                    DateTime zeroTime = new DateTime(1, 1, 1);
                    return (zeroTime + timeSpanToBirthDay).Year - 1;
                }
                return 0;
            }
        }

        public string LastUpdatedDuration
        {
            get
            {
                if(AddedTime == null && UpdatedTime == null)
                {
                    return "";
                }
                String mResult = "Added ";
                DateTime TimeToCheck = (DateTime)AddedTime;
                if (UpdatedTime != null)
                {
                    mResult = "Updated ";
                    TimeToCheck = (DateTime)UpdatedTime;
                }
                
                return mResult + Utils.Utils.GetTimeAgoFrom(TimeToCheck);
            }
        }
    }
}
