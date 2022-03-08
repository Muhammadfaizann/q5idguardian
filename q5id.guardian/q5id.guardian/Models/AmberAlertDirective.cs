using System;
using System.Collections.Generic;

namespace q5id.guardian.Models
{
	public class County
	{
		public List<int> ZipCode { get; set; }
	}

	public class State
	{
		public County County { get; set; }
	}

	public class Target
	{
		public State State { get; set; }
	}

	public class LastSeenAddress
	{
		public string Address { get; set; }
		public string AddressStateName { get; set; }
		public string AddressCityName { get; set; }
		public string LocaleNeighborhoodName { get; set; }
	}

	public class PhoneNumber
	{
		public string Number { get; set; }
		public string Type { get; set; }
	}

	public class PointOfContact
	{
		public string ContactPersonName { get; set; }
		public string ContactOrganizationName { get; set; }
		public PhoneNumber PhoneNumber { get; set; }
		public string EmailAddress { get; set; }
		public string Website { get; set; }
	}

	public class IncidentInformation
	{
		public string MissingPersonCircumstanceText { get; set; }
		public DateTime MissingPersonLastSeenDate { get; set; }
		public DateTime MissingPersonLastSeenTime { get; set; }
		public LastSeenAddress LastSeenAddress { get; set; }
		public PointOfContact PointOfContact { get; set; }
	}

	public class EmbeddedPicture
	{
		public int ImageHeightValue { get; set; }
		public int ImageWidthValue { get; set; }
		public string BinarystringText { get; set; }
	}

	public class ExternalPicture
	{
		public int ImageHeightValue { get; set; }
		public int ImageWidthValue { get; set; }
		public string ImageUrl { get; set; }
	}

	public class Picture
	{
		public EmbeddedPicture EmbeddedPicture { get; set; }
		public ExternalPicture ExternalPicture { get; set; }
		public string PictureDescription { get; set; }
		public string PictureFormat { get; set; }
	}

	public class PhysicalDescription
	{
		public string PersonEyeColorCode { get; set; }
		public string PersonHairColorCode { get; set; }
		public string PersonSkinToneCode { get; set; }
		public string Height { get; set; }
		public string Weight { get; set; }
		public string Description { get; set; }
		public Picture Picture { get; set; }
	}

	public class MissingChild
	{
		public string PersonGivenName { get; set; }
		public string PersonMiddleName { get; set; }
		public string PersonSurName { get; set; }
		public string PersonSuffixName { get; set; }
		public string PersonMonikerName { get; set; }
		public string Gender { get; set; }
		public string Age { get; set; }
		public PhysicalDescription PhysicalDescription { get; set; }
	}

	public class SuspectPerson
	{
		public string PersonGivenName { get; set; }
		public string PersonMiddleName { get; set; }
		public string PersonSurName { get; set; }
		public string PersonSuffixName { get; set; }
		public string PersonMonikerName { get; set; }
		public string Gender { get; set; }
		public string Age { get; set; }
		public PhysicalDescription PhysicalDescription { get; set; }
	}

	public class LicensePlate
	{
		public string LicensePlateText { get; set; }
		public string LicensePlateState { get; set; }
	}

	public class SuspectVehicle
	{
		public string VehicleMakeCode { get; set; }
		public string VehicleModelCode { get; set; }
		public string VehicleModelYearText { get; set; }
		public string VehicleStyleCode { get; set; }
		public string VehicleColorPrimaryCode { get; set; }
		public string VehicleColorSecondaryCode { get; set; }
		public string VehicleColorInteriorText { get; set; }
		public LicensePlate LicensePlate { get; set; }
		public string VehicleDescription { get; set; }
	}

	public class Suspect
	{
		public SuspectPerson SuspectPerson { get; set; }
		public SuspectVehicle SuspectVehicle { get; set; }
	}

	public class FullMessage
	{
		public IncidentInformation IncidentInformation { get; set; }
		public MissingChild MissingChild { get; set; }
		public Suspect Suspect { get; set; }
	}

	public class SMS160
	{
		public string Message { get; set; }
	}

	public class SMS270
	{
		public string Message { get; set; }
	}

	public class AmberAlert
	{
		public Target Target { get; set; }
		public FullMessage FullMessage { get; set; }
		public SMS160 SMS160 { get; set; }
		public SMS270 SMS270 { get; set; }
	}

	public class AmberAlertDirective
	{
		public string Directive { get; set; }
		public int AmberAlertId { get; set; }
		public DateTime MessageTimestamp { get; set; }
		public AmberAlert AmberAlert { get; set; }
	}


}
