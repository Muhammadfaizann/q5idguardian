using System;
using System.Collections.Generic;

namespace q5id.guardian.Models
{
    //public class AmberAlert
    //{
    //    public string Id { get; set; }
    //    public int AmberAlertId { get; set; }
    //    public DateTime MessageTimestamp { get; set; }
    //    public string ZipCode { get; set; }
    //    public AmberAlertTypeFullMessage FullMessage { get; set; }
    //}
    public class AmberAlert
    {
        public int AmberAlertId { get; set; }
        // CosmosDB Prop
        public string id { get; set; }
        public string Circumstance { get; set; }
        public DateTime LastSeenDate { get; set; }
        public string LastSeenAddress { get; set; }
        public string LastSeenState { get; set; }
        public string LastSeenCity { get; set; }
        public AmberAlertPerson[] MissingPersons { get; set; }
        public AmberAlertPerson[] Companions { get; set; }
        public AmberAlertPerson[] Suspects { get; set; }
        public SuspectVehicle[] Vehicles { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        //This is for when the object gets saved to Cosmos, and when returned on query, it has 1+ zipcodes
        public string[] ZipCodes { get; set; }
        //this is for when we search by zip in Cosmos, it only returns one.
        public string ZipCode { get; set; }
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
    public class SuspectVehicle
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Style { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string InteriorColor { get; set; }
        public string LicensePlateText { get; set; }
        public string LicensePlateState { get; set; }
        public string Description { get; set; }
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
