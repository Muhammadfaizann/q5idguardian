using System;
using System.Collections.Generic;

namespace q5id.guardian.Models
{
    public class AmberAlert
    {
        public string AmberAlertId { get; set; }
        public string Circumstance { get; set; }
        public DateTime LastSeenDate { get; set; }
        public string LastSeenAddress { get; set; }
        public string LastSeenState { get; set; }
        public string LastSeenCity { get; set; }
        public AmberAlertPerson[] MissingPersons { get; set; }
        public AmberAlertPerson[] Companions { get; set; }
        public AmberAlertPerson[] Suspects { get; set; }

    }
    public class AmberAlertPerson
    {
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }
        public string SuffixName { get; set; }
        public string MonikerName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string SkinTone { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Description { get; set; }
        public PersonPicture[] Picture { get; set; }
    }

    public class PersonPicture
    {
        public string PictureDescription { get; set; }
        public string PictureFormat { get; set; }
        public PersonEmbeddedPicture EmbeddedPicture { get; set; }
        public PersonExternalPicture ExternalPicture { get; set; }
    }
    public class PersonEmbeddedPicture
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public byte[] BinaryObject { get; set; }
    }
    public class PersonExternalPicture
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public string Url { get; set; }
    }

}
