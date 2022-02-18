using System;
using System.Collections.Generic;
using q5id.guardian.Models;

namespace q5id.guardian.Utils
{
    public static class Constansts
    {
        public static int SEC_TIMEOUT_LOAD_LOCATION = 10;
        public static int KM_DEFAULT_MAP_ZOOM_DISTANCT = 25;//10;

        public static string DEFAULT_DATE_FORMAT = "MM/dd/yyyy";
        public static string BIRTHDAY_DATE_FORMAT = "yyyy-MM-dd";

        public static string HAIR_COLORS_SETTING_KEY = "HairColor";
        public static string EYE_COLORS_SETTING_KEY = "EyeColor";
        public static string HEIGHT_FEETS_SETTING_KEY = "HeightFeet";
        public static string HEIGHT_LIST_INCHES_SETTING_KEY = "HeightInches";
        public static string USER_ROLE_SETTING_KEY = "UserRole";
        public static string USER_ROLE_SUBSCRIBER_KEY = "Subscriber";

        public static string LOVED_ONES_ENTITY_SETTING_KEY = "Profile";
        public static string ALERT_ENTITY_SETTING_KEY = "Alert";
        public static string ALERT_FEED_ENTITY_SETTING_KEY = "AlertFeed";
        public static string ACCOUNT_ENTITY_SETTING_KEY = "Account";
        public static string CONTACT_ENTITY_SETTING_KEY = "Contact";
        public static string USER_ENTITY_SETTING_KEY = "User";

        public static string YES_KEY = "Yes";
        public static string NO_KEY = "No";

        public static string API_TOKEN_TYPE = "Bearer";

        public static string PRIVACY_POLICY_URL = "https://q5id.com/privacy-policy";
        public static string HELP_URL = "mailto:help@q5id.com";
        public static string FAQ_URL = "https://guardian.q5id.com/frequently-asked-questions";
        public static string ABOUT_URL = "https://guardian.q5id.com/about";
        public static string LICENSE_AGREEMENT = "https://q5id.com/enterprise-end-user-license-agreement";

        public static string DevicePushIDAlwaysEmpty = "";
        public static string FakeGuidAsIndicated = "00000000-0000-0000-0000-000000000000";
        public static string AppPurchaseIsFalse = "False";
        public static List<string> ServerPopulatesThisTagShouldBeEmpty = new List<string>();

        public static string DEVICE_UPDATE_STRING = "DeviceUpdate";
        public static string DEVICE_DISTANCE_STRING = "DeviceDistance";
    }
}
