
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using q5id.guardian.ViewModels.ItemViewModels;

namespace q5id.guardian.ViewModels
{
    public class AlertsViewModel : BaseViewModel
    {
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

        private List<object> mAlerts = null;
        public List<object> Alerts
        {
            get => mAlerts;
            set
            {
                mAlerts = value;
                RaisePropertyChanged(nameof(Alerts));
            }
        }

        public override async Task Initialize()
        {
            GetAlerts();
        }

        private void GetAlerts()
        {
            Alerts = new List<object>()
            {
                new GroupHeaderItemViewModel()
                {
                    Title="Live",
                    Description="There aren’t any active alerts near you.",
                    IsEmptyList=true
                },
                new GroupHeaderItemViewModel()
                {
                    Title="History",
                    Description="You don’t have any past alerts.",
                    IsEmptyList=true
                },
            };
        }

        public AlertsViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }
    }
}
