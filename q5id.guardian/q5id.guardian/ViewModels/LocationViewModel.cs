using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class LocationViewModel:BaseViewModel
    {
        public string TitlePage
        {
            get
            {
                return "Settings";
            }
        }

        public string TimeInterval { get; set; }

        public string Distance { get; set; }

        public Command BackCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await NavigationService.Close(this);
                });
            }
        }

        public Command SaveCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var result = await UpdateLocationSettings();
                    if (result)
                    {
                        await NavigationService.Close(this);
                    }
                });
            }
        }

        public LocationViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }

        public override void ViewAppearing()
        {
            var distance = Utils.Utils.GetDistanceForDeviceUpdate();
            var interval = Utils.Utils.GetUpdateFrequency();

            Distance = distance.ToString();
            TimeInterval = interval.ToString();
        }

        private async Task<bool> UpdateLocationSettings()
        {
            Distance = string.IsNullOrEmpty(Distance) ? "0" : Distance;
            TimeInterval = string.IsNullOrEmpty(TimeInterval) ? "0" : TimeInterval;

            if (int.TryParse(Distance, out int convertedDistance))
            {
                Utils.Utils.SetDistanceForDeviceUpdate(convertedDistance);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Invalid value for Alert Distance (Miles)", "", "OK");
                return false;
            }

            if (int.TryParse(TimeInterval,out int convertedTimeInterval))
            {
                Utils.Utils.SetUpdateFrequency(convertedTimeInterval);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Invalid value for Geolocation Refresh Rate (Seconds)", "", "OK");
                return false;
            }

            await App.Current.MainPage.DisplayAlert("Successfully updated location settings", "", "OK");
            return true;
        }
    }
}
