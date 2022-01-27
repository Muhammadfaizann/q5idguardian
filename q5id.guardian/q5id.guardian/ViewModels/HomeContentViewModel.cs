using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.DependencyServices;
using q5id.guardian.Models;
using q5id.guardian.Services;
using q5id.guardian.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace q5id.guardian.ViewModels
{
    public class HomeContentViewModel : BaseSubViewModel
    {

        
        public HomeContentViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }

        public Command SubscriptionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsLoading = true;
                    var result = await InAppBillingService.Instances.MakePurchase();
                    IsLoading = false;
                    this.UpdateModel();
                });
            }
        }

        private User mUser = null;
        public User User
        {
            get => mUser;
            set
            {
                mUser = value;
            }
        }

        private Boolean mIsSubcriber = false;
        public Boolean IsSubcriber
        {
            get => mIsSubcriber;
            set
            {
                mIsSubcriber = value;
            }
        }

        public Boolean IsVolunteer
        {
            get
            {
                return !IsSubcriber;
            }
        }

        private ObservableCollection<UserPage> mPage;
        public ObservableCollection<UserPage> Pages
        {
            get
            {
                return this.mPage;
            }
            set => SetProperty(ref mPage, value);
        }

        private List<Alert> mAlerts;
        public List<Alert> Alerts
        {
            get
            {
                return this.mAlerts;
            }

            set
            {
                this.mAlerts = value;
            }
        }

        public List<Position> AlertPositions
        {
            get
            {
                if (Alerts != null)
                {
                    return Alerts.Select((Alert alert) =>
                    {
                        return alert.Position;
                    }).ToList();
                }
                return null;
            }
        }

        public Boolean IsHaveAlerts
        {
            get
            {
                return this.Alerts != null && this.Alerts.Count > 0;
            }
        }

        
        public override async Task Initialize()
        {
            GetUserPages();
<<<<<<< HEAD
            await GetAlerts();
            await UpdateUserDevice();
            
        }

        private async Task UpdateUserDevice()
        {
            UserSession userSession = Utils.Utils.GetToken();
            var currentUserDevice = Utils.Utils.GetUserDevice();
            var location = await Utils.Utils.GetLocalLocation();
            if (currentUserDevice != null)
            {
                await AppApiManager.Instances.DeleteUserDevice(currentUserDevice);
                Utils.Utils.SaveUserDevice(null);
            };
            var currentPushToken = Utils.Utils.GetPushNotificationToken();
            Debug.WriteLine("currentPushToken: ", currentPushToken);
            IAppDeviceService service = DependencyService.Get<IAppDeviceService>();
            var userDevice = new UserDevice()
            {
                UserId = userSession.UserId,
                DevicePushId = Utils.Constansts.DevicePushIDAlwaysEmpty,
                Platform = Device.RuntimePlatform.ToUpper(),
                SubscriptionId = Utils.Constansts.FakeGuidAsIndicated,
                IsAppPurchaseToken = Utils.Constansts.AppPurchaseIsFalse,
                DeviceId = currentPushToken,
                DeviceUUID = service.GetDeviceId(),
                Tags = Utils.Constansts.ServerPopulatesThisTagShouldBeEmpty,
                Latitude = location != null ? location.Latitude : 0,
                Longitude = location != null ? location.Longitude : 0
            };
            
            var responseCreateUserDevice = await AppApiManager.Instances.CreateUserDevice(userDevice);
            if (responseCreateUserDevice.IsSuccess && responseCreateUserDevice.ResponseObject != null && responseCreateUserDevice.ResponseObject.Result != null)
            {
                var newUserDevice = responseCreateUserDevice.ResponseObject.Result;
                Utils.Utils.SaveUserDevice(newUserDevice);
            }
            else
            {
                var logInformation = new Dictionary<string, string>()
                {
                    { "UserId",userSession?.UserId },
                    { "PushToken",currentPushToken }
                };

                Analytics.TrackEvent("Update UserDevice - Failed",logInformation);
            }                    
        }
=======
            GetAlerts();
        }

        
>>>>>>> origin/develop

        public async Task GetAlerts()
        {
            var currentLocation = await Utils.Utils.GetLocalLocation();
            if (currentLocation != null)
            {
                var response = await AppApiManager.Instances.GetNearbyListAlert(currentLocation.Latitude, currentLocation.Longitude, Utils.Constansts.KM_DEFAULT_MAP_ZOOM_DISTANCT);
                if (response.IsSuccess && response.ResponseObject != null)
                {
                    Alerts = response.ResponseObject;
                }
            }
            else
            {
                Alerts = new List<Alert>();

                UserSession userSession = Utils.Utils.GetToken();
                var logInformation = new Dictionary<string, string>()
                {
                    { "UserId",userSession?.UserId }
                };

                Analytics.TrackEvent("GetAlerts - Failed", logInformation);
            }
        }


        private void GetUserPages()
        {
            Pages = new ObservableCollection<UserPage>()
            {
                new UserPage()
                {
                    Title = "Title 1",
                    Subtitle = "Subtitle 1",
                    VideoUrl = "https://flutter.github.io/assets-for-api-docs/assets/videos/butterfly.mp4",
                },
                new UserPage()
                {
                    Title = "Title 2",
                    Subtitle = "Subtitle 2",
                    VideoUrl = "https://flutter.github.io/assets-for-api-docs/assets/videos/butterfly.mp4",
                },
                new UserPage()
                {
                    Title = "Title 3",
                    Subtitle = "Subtitle 3",
                    VideoUrl = "https://flutter.github.io/assets-for-api-docs/assets/videos/butterfly.mp4",
                },
                new UserPage()
                {
                    Title = "Title 4",
                    Subtitle = "Subtitle 4",
                    VideoUrl = "https://flutter.github.io/assets-for-api-docs/assets/videos/butterfly.mp4",
                },
            };
        }
    }
}
