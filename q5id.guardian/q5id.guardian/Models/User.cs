using System;
namespace q5id.guardian.Models
{
    public class User
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }

        public User()
        {
        }
    }
}
