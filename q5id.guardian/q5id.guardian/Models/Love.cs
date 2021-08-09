using System;
namespace q5id.guardian.Models
{
    public class Love
    {
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? AddedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public Boolean IsLongTime { get; set; } = false;

        public string FullName
        {
            get => FirstName + " " + LastName;
        }

        public string LastUpdatedDuration
        {
            get
            {
                DateTime CurrentDateTime = DateTime.Now;
                String mResult = "Added ";
                DateTime TimeToCheck = (DateTime)AddedTime;
                if (UpdatedTime != null)
                {
                    mResult = "Updated ";
                    TimeToCheck = (DateTime)UpdatedTime;
                }
                TimeSpan timeSpan = CurrentDateTime.Subtract(TimeToCheck);
                int numberOfMonths = (int)(CurrentDateTime.Subtract(TimeToCheck).Days / (365.25 / 12));
                mResult += numberOfMonths;
                if(numberOfMonths == 1)
                {
                    mResult += " month ago";
                }
                else
                {
                    mResult += " months ago";
                }
                return mResult;
            }
        }
    }
}
