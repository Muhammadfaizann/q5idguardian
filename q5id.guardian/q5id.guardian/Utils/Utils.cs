using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using q5id.guardian.Models;
using q5id.guardian.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using static Xamarin.Essentials.Permissions;

namespace q5id.guardian.Utils
{
    public static class Utils
    {
        private static string CHOICES_KEY = "choices";
        private static string TOKEN_KEY = "token";

        public static byte[] ConvertStreamToByteArray(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public static async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var isGranted = await PermissionService.RequestPermissionAsync<Permissions.LocationAlways>(new LocationAlways());
            if (isGranted == true)
                return PermissionStatus.Granted;
            else
                return PermissionStatus.Denied;
        }

        public static Color GetColorFromResource(String key, Color defaultColor)
        {
            object pinColorObj;
            Application.Current.Resources.TryGetValue(key, out pinColorObj);
            if (pinColorObj is Color color)
            {
                return color;
            }
            return defaultColor;
        }

        public static void SaveChoices(List<Choice> choices)
        {
            Preferences.Set(CHOICES_KEY, JsonConvert.SerializeObject(choices));
        }

        public static List<Choice> GetChoices()
        {
            try
            {
                var strChoices = Preferences.Get(CHOICES_KEY, "");
                if(strChoices != "")
                {
                    return JsonConvert.DeserializeObject<List<Choice>>(strChoices);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot get choices: " + ex.Message);
            }
            return new List<Choice>();
        }

        public static void SaveToken(User user)
        {
            if(user != null)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes($"{user.Email}:{user.SessionToken}");
                var session = System.Convert.ToBase64String(plainTextBytes);
                var userSession = new UserSession()
                {
                    UserId = user.UserId,
                    Session = session,
                    SessionExpiredDate = user.UpdatedTime.ToString(),
                };
                Preferences.Set(TOKEN_KEY, JsonConvert.SerializeObject(userSession));
            }
            else
            {
                Preferences.Set(TOKEN_KEY, "");
            }
        }

        public static UserSession GetToken()
        {
            try
            {
                var userSessionJson = Preferences.Get(TOKEN_KEY, "");
                if(userSessionJson != "")
                {
                    UserSession userSession = JsonConvert.DeserializeObject<UserSession>(userSessionJson);
                    return userSession;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot get token: " + ex.Message);
            }
            return null;
        }

        public static string GetTimeAgoFrom(DateTime dateTime)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - dateTime.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }

        public static async Task<Plugin.Geolocator.Abstractions.Position> GetLocalLocation()
        {
            if (IsLocationAvailable())
            {
                try
                {
                    var locator = CrossGeolocator.Current;
                    return await locator.GetLastKnownLocationAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Cannot get local location: " + ex.Message);
                }
            }
            return null;
        }

        private static bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;
            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        public static async Task<string> FindPlaceByPosition(Xamarin.Forms.Maps.Position position)
        {
            Geocoder geoCoder = new Geocoder();
            IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
            string address = possibleAddresses.FirstOrDefault();
            return address;
        }
    }
}
