using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using q5id.guardian.Services;
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
                RaisePropertyChanged(nameof(Alerts));
                RaisePropertyChanged(nameof(IsHaveAlerts));
            }
        }

        public List<Position> AlertPositions
        {
            get
            {
                return Alerts.Select((Alert alert) =>
                {
                    return alert.Position;
                }).ToList();
            }
        }

        public Boolean IsHaveAlerts
        {
            get
            {
                return this.Alerts != null && this.Alerts.Count > 0;
            }
        }

        public StructureEntity AlertEntity { get; private set; }

        public override async Task Initialize()
        {
            GetUserPages();
            GetAlerts();
        }

        private void GetAlertEntity()
        {
            var settings = Utils.Utils.GetSettings();
            if (settings != null)
            {
                AlertEntity = Utils.Utils.GetSettings().Find((StructureEntity entity) =>
                {
                    return entity.EntityName == Utils.Constansts.ALERT_ENTITY_SETTING_KEY;
                });
            }
        }

        private async void GetAlerts()
        {
            if (AlertEntity != null)
            {
                var response = await AppService.Instances.GetListAlert(AlertEntity.Id);
                if (response.IsSuccess)
                {
                    Alerts = response.ResponseObject.Select((Entity<Alert> entityAlert) =>
                    {
                        return entityAlert.Data;
                    }).ToList();
                }
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
