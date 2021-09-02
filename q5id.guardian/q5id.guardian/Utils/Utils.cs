using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using q5id.guardian.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace q5id.guardian.Utils
{
    public static class Utils
    {
        private static string SETTING_KEY = "settings";

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

        public static async Task<PermissionStatus> CheckAndRequestPermissionAsync<T>(T permission)
            where T : BasePermission
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }

            return status;
        }

        public static async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await CheckAndRequestPermissionAsync<Permissions.LocationAlways>(new LocationAlways());
            if (status == PermissionStatus.Granted)
                return status;
            else
                return await CheckAndRequestLocationPermission();
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

        public static void SaveSetting(List<StructureEntity> settings)
        {
            Preferences.Set(SETTING_KEY, JsonConvert.SerializeObject(settings));
        }

        public static List<StructureEntity> GetSettings()
        {
            try
            {
                var strSettings = Preferences.Get(SETTING_KEY, "");
                return JsonConvert.DeserializeObject<List<StructureEntity>>(strSettings);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Cannot get settings: " + ex.Message);
            }
            return new List<StructureEntity>();
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

        public static async Task<Position> GetLocalLocation()
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
    }
}
