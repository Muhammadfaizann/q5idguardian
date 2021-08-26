
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using q5id.guardian.Services;
using q5id.guardian.ViewModels.ItemViewModels;

namespace q5id.guardian.ViewModels
{
    public class AlertsViewModel : BaseViewModel
    {
        private bool mIsYourPersonalNetwork = false;
        public bool IsYourPersonalNetwork
        {
            get => mIsYourPersonalNetwork;
            set
            {
                mIsYourPersonalNetwork = value;
                RaisePropertyChanged(nameof(IsYourPersonalNetwork));
            }
        }

        private bool mIsGuardianNearby = false;
        public bool IsGuardianNearby
        {
            get => mIsGuardianNearby;
            set
            {
                mIsGuardianNearby = value;
                RaisePropertyChanged(nameof(IsGuardianNearby));
            }
        }

        private bool mIsLowEnforcement = false;
        public bool IsLowEnforcement
        {
            get => mIsLowEnforcement;
            set
            {
                mIsLowEnforcement = value;
                RaisePropertyChanged(nameof(IsLowEnforcement));
            }
        }

        private bool mIsOwner = false;
        public bool IsOwner
        {
            get => mIsOwner;
            set
            {
                mIsOwner = value;
                RaisePropertyChanged(nameof(IsOwner));
            }
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

        private ObservableCollection<object> mAlerts;
        public ObservableCollection<object> Alerts
        {
            get => mAlerts;
            set
            {
                mAlerts = value;
                RaisePropertyChanged(nameof(Alerts));
            }
        }

        private Alert mAlertDetail;
        public Alert AlertDetail
        {
            get
            {
                return mAlertDetail;
            }
            set
            {
                mAlertDetail = value;
                RaisePropertyChanged(nameof(AlertDetail));
            }
        }

        private Love mCreatingLove;
        public Love CreatingLove
        {
            get
            {
                return mCreatingLove;
            }
            set
            {
                mCreatingLove = value;
                RaisePropertyChanged(nameof(CreatingLove));
            }
        }

        private ObservableCollection<object> mFeeds;
        public ObservableCollection<object> Feeds
        {
            get => mFeeds;
            set
            {
                mFeeds = value;
                RaisePropertyChanged(nameof(Feeds));
            }
        }

        private ObservableCollection<Love> mLoves = null;
        public ObservableCollection<Love> Loves
        {
            get => mLoves;
            set
            {
                mLoves = value;
                RaisePropertyChanged(nameof(Loves));
            }
        }

        public override async Task Initialize()
        {
            GetAlerts();
            GetFeeds();
            GetLoves();
        }

        private void GetLoves()
        {
            Loves = new ObservableCollection<Love>()
            {
                new Love()
                {
                    FirstName = "Amber",
                    LastName = "Jones",
                    ImageUrl = "https://images.statusfacebook.com/profile_pictures/beautiful-children-photos/beautiful-children-dp-profile-pictures-for-whatsapp-facebook-01.jpg",
                    UpdatedTime = null,
                    AddedTime = new DateTime(2020, 1, 1),
                    BirthDay = new DateTime(2016, 1, 1),
                },
                new Love()
                {
                    FirstName = "Sarah",
                    LastName = "Jones",
                    ImageUrl = "https://images.statusfacebook.com/profile_pictures/beautiful-children-photos/beautiful-children-dp-profile-pictures-for-whatsapp-facebook-05.jpg",
                    UpdatedTime = new DateTime(2021, 4, 1),
                    AddedTime = new DateTime(2020, 1, 1),
                    BirthDay = new DateTime(2017, 1, 1),
                },
                new Love()
                {
                    FirstName = "Theo",
                    LastName = "Jones",
                    ImageUrl = "https://images.statusfacebook.com/profile_pictures/beautiful-children-photos/beautiful-children-dp-profile-pictures-for-whatsapp-facebook-06.jpg",
                    UpdatedTime = new DateTime(2021, 4, 1),
                    AddedTime = new DateTime(2020, 1, 1),
                    BirthDay = new DateTime(2018, 1, 1),
                    IsLongTime = true,
                }
            };
        }

        private void GetFeeds()
        {
            Feeds = new ObservableCollection<object>()
            {
                new FeedItemViewModel(new Feed())
                {
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQyX16xukQ-ZF5Obira-CMZqamIbFPfaMeB557mzjZjsbZ3h97l3LlXihU5VGiDtegDvo0",
                    Name = "Mathan A ",
                    Action = "is looking",
                    UpdatedTime = "9:50 am",
                    IsParent = false,
                },
                new FeedItemViewModel(new Feed())
                {
                    ImageUrl = "https://expertphotography.b-cdn.net/wp-content/uploads/2020/08/social-media-profile-photos-3.jpg",
                    Name = "Dyland M ",
                    Action = "is looking",
                    UpdatedTime = "9:50 am",
                    IsParent = false,
                },
                new FeedItemViewModel(new Feed())
                {
                    ImageUrl = "https://cdn.fastly.picmonkey.com/contentful/h6goo9gw1hh6/2sNZtFAWOdP1lmQ33VwRN3/24e953b920a9cd0ff2e1d587742a2472/1-intro-photo-final.jpg",
                    Name = "Sarah R ",
                    Action = "is looking",
                    UpdatedTime = "9:49 am",
                    IsParent = false,
                },
                new FeedItemViewModel(new Feed())
                {
                    ImageUrl = "https://i1.wp.com/www.alphr.com/wp-content/uploads/2020/12/Facebook-How-to-Change-Profile-Picture.jpg",
                    Name = "Parent ",
                    Action = "posted",
                    UpdatedTime = "9:48 am",
                    IsParent = true,
                    Detail="“We’re in the kids’ clothing department at Target, my daughter is wearing a blue dress and rainbow shoes.”"
                },
            };
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
                    OnUpdateExpanded = OnItemExpandedUpdate,
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
                    OnUpdateExpanded = OnItemExpandedUpdate,
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
                    OnUpdateExpanded = OnItemExpandedUpdate,
                },
            };
        }

        private void OnItemExpandedUpdate(AlertItemViewModel item)
        {
            var indexOf = Alerts.IndexOf(item);
            Alerts[indexOf] = item;
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
