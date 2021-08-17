
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<object> mAlerts = null;
        public ObservableCollection<object> Alerts
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
            Alerts = new ObservableCollection<object>()
            {
                new GroupHeaderItemViewModel()
                {
                    Title="Live",
                    Description="There aren’t any active alerts near you.",
                    IsEmptyList=false
                },
                new AlertItemViewModel(new Alert()
                {
                    Love = new Love()
                    {
                        FirstName= "John",
                        LastName= "Doe",
                        ImageUrl = "https://images.statusfacebook.com/profile_pictures/beautiful-children-photos/beautiful-children-dp-profile-pictures-for-whatsapp-facebook-01.jpg",
                        BirthDay= new DateTime(2016, 12, 6)
                    }
                })
                {
                    UpdatedTimeDescription = "Started 23 minutes ago",
                    OnUpdateItemAction = OnUpdateItemList,
                },  
                new AlertItemViewModel(new Alert()
                {
                    Love = new Love()
                    {
                        FirstName= "Hue",
                        LastName= "Sanron",
                        BirthDay= new DateTime(2018, 5, 13),
                        ImageUrl = "https://images.statusfacebook.com/profile_pictures/beautiful-children-photos/beautiful-children-dp-profile-pictures-for-whatsapp-facebook-05.jpg",
                    }
                })
                {
                    UpdatedTimeDescription = "Started 2 hours ago",
                    OnUpdateItemAction = OnUpdateItemList,
                },
                new GroupHeaderItemViewModel()
                {
                    Title="History",
                    Description="You don’t have any past alerts.",
                    IsEmptyList=false
                },
                new AlertItemViewModel(new Alert()
                {
                    Love = new Love()
                    {
                        FirstName= "Donian",
                        LastName= "Billean",
                        BirthDay= new DateTime(2015, 2, 8),
                        ImageUrl = "https://images.statusfacebook.com/profile_pictures/beautiful-children-photos/beautiful-children-dp-profile-pictures-for-whatsapp-facebook-06.jpg",
                    }
                })
                {
                    UpdatedTimeDescription = "Started 3 days ago",
                    OnUpdateItemAction = OnUpdateItemList,
                },
            };
        }

        private void OnUpdateItemList()
        {
            RaisePropertyChanged(nameof(Alerts));
        }

        public AlertsViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }
    }
}
