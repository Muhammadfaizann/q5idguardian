using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using q5id.guardian.Services.Bases;
using q5id.guardian.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace q5id.guardian.Services
{
    public class BackgroundLocation : IBackgroundLocation
    {
        public async Task StartListening()
        {
            if (await Utils.Utils.CheckAndRequestLocationPermission() != PermissionStatus.Granted)
                return;
;
            if (CrossGeolocator.Current.IsListening)
                return;

            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromMinutes(Utils.Utils.GetUpdateFrequency()), 
                Utils.Utils.GetDistanceForDeviceUpdate(), true, new Plugin.Geolocator.Abstractions.ListenerSettings
            {
                ActivityType = Plugin.Geolocator.Abstractions.ActivityType.OtherNavigation,
                AllowBackgroundUpdates = true,
                DeferLocationUpdates = true,
                DeferralDistanceMeters = 1,
                DeferralTime = TimeSpan.FromSeconds(30),
                ListenForSignificantChanges = true,
                PauseLocationUpdatesAutomatically = false
            }) ;
            CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
            CrossGeolocator.Current.PositionError += Current_PositionError;
        }

        private void Current_PositionError(object sender, PositionErrorEventArgs e)
        {
            Debug.WriteLine(e.Error);
        }

        private void Current_PositionChanged(object sender, PositionEventArgs e)
        {
            var position = e.Position;
            if (position == null)
                return;

            var user = Utils.Utils.GetUserDevice();
            if (user == null)
                return;
            user.Latitude = position.Latitude;
            user.Longitude = position.Longitude;
            Utils.Utils.SaveUserDevice(user);   
        }

       

        public async Task StopListening()
        {
            if(!CrossGeolocator.Current.IsListening)
                return;
            await CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= Current_PositionChanged;
            CrossGeolocator.Current.PositionError -= Current_PositionError;
        }
    }
}
