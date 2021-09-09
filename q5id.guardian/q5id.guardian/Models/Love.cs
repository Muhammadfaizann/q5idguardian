using System;
using Newtonsoft.Json;

namespace q5id.guardian.Models
{
    public class Love : BaseEntity
    {
        [JsonProperty("Id")]
        public string PrimaryId { get; set; }
        [JsonProperty("ProfileId")]
        public string ProfileId { get; set; }
        [JsonProperty("ContactId")]
        public string ContactId { get; set; }
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
                ContactId = ContactId,
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
