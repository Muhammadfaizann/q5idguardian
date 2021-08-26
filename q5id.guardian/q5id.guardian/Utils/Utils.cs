﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

        public static void SaveSetting(List<StrutureEntity> settings)
        {
            Preferences.Set(SETTING_KEY, JsonConvert.SerializeObject(settings));
        }

        public static List<StrutureEntity> GetSettings()
        {
            try
            {
                var strSettings = Preferences.Get(SETTING_KEY, "");
                return JsonConvert.DeserializeObject<List<StrutureEntity>>(strSettings);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Cannot get settings: " + ex.Message);
            }
            return new List<StrutureEntity>();
        }
    }
}
