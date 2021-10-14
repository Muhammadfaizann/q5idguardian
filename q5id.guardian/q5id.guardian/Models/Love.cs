using System;
using System.Collections.Generic;
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
        [JsonProperty("HairColor")]
        public string HairColorId { get; set; }
        [JsonIgnore]
        public string HairColorName
        {
            get
            {
                return GetChoiceNameById(Utils.Constansts.HAIR_COLORS_SETTING_KEY, HairColorId);
            }
        }
        [JsonProperty("EyeColor")]
        public string EyeColorId { get; set; }
        [JsonIgnore]
        public string EyeColorName
        {
            get
            {
                return GetChoiceNameById(Utils.Constansts.EYE_COLORS_SETTING_KEY, EyeColorId);
            }
        }
        [JsonProperty("Weight")]
        public string Weight { get; set; }
        [JsonProperty("HeightFeet")]
        public string HeightFeetId { get; set; }
        [JsonIgnore]
        public string HeightFeetName
        {
            get
            {
                return GetChoiceNameById(Utils.Constansts.HEIGHT_FEETS_SETTING_KEY, HeightFeetId);
            }
        }
        [JsonProperty("HeightInches")]
        public string HeightInchesId { get; set; }
        [JsonIgnore]
        public string HeightInchesName
        {
            get
            {
                return GetChoiceNameById(Utils.Constansts.HEIGHT_LIST_INCHES_SETTING_KEY, HeightInchesId);
            }
        }
        [JsonProperty("OtherInformation")]
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

        private static List<Choice> mChoices = null;
        public static List<Choice> Choices
        {
            get
            {
                if(mChoices == null)
                {
                    mChoices = Utils.Utils.GetChoices();
                }
                return mChoices;
            }
        }

        public Love()
        {
           
        }

        private string GetChoiceNameById(string choiceKey, string choiceId)
        {
            Choice elementChoice = Choices.Find((Choice obj) =>
            {
                return obj.Name == choiceKey;
            });
            if (elementChoice != null)
            {
                var listChoice = elementChoice.Items;
                var itemChoice = listChoice.Find((ItemChoice item) =>
                {
                    return item.Id == choiceId;
                });
                if(itemChoice != null)
                {
                    return itemChoice.Name;
                }
            }
            return "";
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
                HairColor = HairColorId,
                EyeColor = EyeColorId,
                Weight = Weight,
                HeightFeet = HeightFeetId,
                HeightInches = HeightInchesId,
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
