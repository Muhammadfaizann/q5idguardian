using System;
namespace q5id.guardian.Models
{
    public class UserSession
    {
        public string UserId { get; set; }

        public string Session { get; set; }

        public string SessionExpiredDate { get; set; }
    }
}
