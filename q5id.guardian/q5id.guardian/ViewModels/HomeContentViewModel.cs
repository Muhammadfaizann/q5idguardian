using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using q5id.guardian.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace q5id.guardian.ViewModels
{
    public class HomeContentViewModel : BaseViewModel
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
                    var result = await NavigationService.Navigate<IAPViewModel, User, User>(mUser);
                    User = result;
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

        public Boolean IsVolunteer
        {
            get
            {
                return this.User != null && this.User.Role == UserRole.Volunteer;
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
            GetAlerts();
        }

        public async void GetAlerts()
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
