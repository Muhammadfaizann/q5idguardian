using System;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class Love : BaseEntity
    {
        [JsonProperty("id")]
        public string PrimaryId { get; set; }
        [JsonProperty("profileId")]
        public string ProfileId { get; set; }
        [JsonProperty("contactId")]
        public string ContactId { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("dateofBirth")]
        public string DateofBirth { get; set; }
        [JsonProperty("hairColor")]
        public string HairColor { get; set; }
        [JsonProperty("eyeColor")]
        public string EyeColor { get; set; }
        [JsonProperty("weight")]
        public string Weight { get; set; }
        [JsonProperty("heightFeet")]
        public string HeightFeet { get; set; }
        [JsonProperty("heightInches")]
        public string HeightInches { get; set; }
        [JsonProperty("otherInformation")]
        public string OtherInformation { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("image2")]
        public string Image2 { get; set; }
        [JsonProperty("image3")]
        public string Image3 { get; set; }
        [JsonProperty("image4")]
        public string Image4 { get; set; }
        [JsonProperty("image5")]
        public string Image5 { get; set; }

        public Love()
        {
           
        }

        public override object GetParam()
        {
            return new
            {
                createdon = CreatedOn,
                modifiedon = ModifiedOn,
                createdby = CreatedBy,
                ProfileId = ProfileId,
                UserId = UserId,
                FirstName = FirstName,
                LastName = LastName,
                DateofBirth = DateofBirth,
                HairColor = HairColor,
                EyeColor = EyeColor,
                Weight = Weight,
                HeightFeet = HeightFeet,
                HeightInches = HeightInches,
                OtherInformation = OtherInformation,
                Image = Image,
                Image2 = Image2,
                Image3 = Image3,
                Image4 = Image4,
                Image5 = Image5,
            };
        }

        [JsonIgnore]
        public Boolean IsLongTime { get; set; } = false;

        [JsonIgnore]
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

        [JsonIgnore]
        public string FullName
        {
            get => FirstName + " " + LastName;
        }

        [JsonIgnore]
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

        [JsonIgnore]
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
