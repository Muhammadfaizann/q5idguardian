using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using Xamarin.Forms.Maps;

namespace q5id.guardian.ViewModels
{
    public class HomeContentViewModel : BaseViewModel
    {

        public HomeContentViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }
        private User mUser = null;
        public User User
        {
            get => mUser;
            set
            {
                mUser = value;
                RaisePropertyChanged(nameof(User));
                RaisePropertyChanged(nameof(IsVolunteer));
            }
        }

        public Boolean IsVolunteer
        {
            get
            {
                return this.mUser != null && this.mUser.Role == UserRole.Volunteer;
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

        private ObservableCollection<Alert> mAlerts;
        public ObservableCollection<Alert> Alerts
        {
            get
            {
                return this.mAlerts;
            }

            set
            {
                this.mAlerts = value;
                RaisePropertyChanged(nameof(Alerts));
                RaisePropertyChanged(nameof(IsHaveAlerts));
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
        }
        private void GetUserPages()
        {
            Pages = new ObservableCollection<UserPage>()
            {
                new UserPage()
                {
                    Title = "Title 1",
                    Subtitle = "Subtitle 1",
                    VideoUrl = "https://sec.ch9.ms/ch9/5d93/a1eab4bf-3288-4faf-81c4-294402a85d93/XamarinShow_mid.mp4",
                },
                new UserPage()
                {
                    Title = "Title 2",
                    Subtitle = "Subtitle 2",
                    VideoUrl = "https://sec.ch9.ms/ch9/5d93/a1eab4bf-3288-4faf-81c4-294402a85d93/XamarinShow_mid.mp4",
                },
                new UserPage()
                {
                    Title = "Title 3",
                    Subtitle = "Subtitle 3",
                    VideoUrl = "https://sec.ch9.ms/ch9/5d93/a1eab4bf-3288-4faf-81c4-294402a85d93/XamarinShow_mid.mp4",
                },
                new UserPage()
                {
                    Title = "Title 4",
                    Subtitle = "Subtitle 4",
                    VideoUrl = "https://sec.ch9.ms/ch9/5d93/a1eab4bf-3288-4faf-81c4-294402a85d93/XamarinShow_mid.mp4",
                },
            };
        }

        public void GetUserAlerts(Position fromPos)
        {
            Random rand = new Random();
            var alerts = new ObservableCollection<Alert>();
            for(int i = 0; i < 2; i++)
            {
                Alert al = new Alert();
                al.Title = "Title " + (i + 1);
                al.Address = "Address " + (i + 1);
                float difLat = 500 + rand.Next(500);
                if(rand.Next(2) == 0)
                {
                    difLat = -difLat;
                }
                difLat = difLat / 10000;
                float difLong = 500 + rand.Next(500);
                if (rand.Next(2) == 0)
                {
                    difLong = -difLong;
                }
                difLong = difLong / 10000;
                al.Position = new Position(fromPos.Latitude + difLat, fromPos.Longitude + difLong);
                alerts.Add(al);
            }
            Alerts = alerts;
        }
    }
}
