using System;
using System.Collections.Generic;
using q5id.guardian.Models;

namespace q5id.guardian.Utils
{
    public static class Constansts
    {
        public static int SEC_TIMEOUT_LOAD_LOCATION = 10;
        public static int KM_DEFAULT_MAP_ZOOM_DISTANCT = 10;

        public static string DEFAULT_DATE_FORMAT = "MM/dd/yyyy";
        public static string BIRTHDAY_DATE_FORMAT = "yyyy-MM-dd";

        public static string HAIR_COLORS_SETTING_KEY = "HairColor";
        public static string EYE_COLORS_SETTING_KEY = "EyeColor";
        public static string HEIGHT_FEETS_SETTING_KEY = "HeightFeet";
        public static string HEIGHT_LIST_INCHES_SETTING_KEY = "HeightInches";

        public static string LOVED_ONES_ENTITY_SETTING_KEY = "Profile";
        public static string ALERT_ENTITY_SETTING_KEY = "Alert";
        public static string ALERT_FEED_ENTITY_SETTING_KEY = "AlertFeed";
        public static string ACCOUNT_ENTITY_SETTING_KEY = "Account";
        public static string CONTACT_ENTITY_SETTING_KEY = "Contact";
        public static string USER_ENTITY_SETTING_KEY = "User";

        public static string YES_KEY = "Yes";
        public static string NO_KEY = "No";

        public static string API_TOKEN_TYPE = "Bearer";
    }
}
