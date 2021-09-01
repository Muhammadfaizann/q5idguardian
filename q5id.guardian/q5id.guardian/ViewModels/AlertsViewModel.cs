
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using q5id.guardian.Services;
using q5id.guardian.Services.Bases;
using q5id.guardian.ViewModels.ItemViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace q5id.guardian.ViewModels
{
    public class AlertsViewModel : BaseViewModel
    {
        private Boolean mIsUpdateSuccess = false;
        public Boolean IsUpdateSuccess
        {
            get => mIsUpdateSuccess;
            set
            {
                mIsUpdateSuccess = value;
                RaisePropertyChanged(nameof(IsUpdateSuccess));
            }
        }

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

        private Position? mAlertPosition = null;
        public Position? AlertPosition
        {
            get => mAlertPosition;
            set
            {
                mAlertPosition = value;
                RaisePropertyChanged(nameof(AlertPosition));
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

        private string mDetail;
        public string Detail
        {
            get => mDetail;
            set
            {
                mDetail = value;
                RaisePropertyChanged(nameof(Detail));
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

        private List<LoveItemViewModel> mLoves = null;
        public List<LoveItemViewModel> Loves
        {
            get => mLoves;
            set
            {
                mLoves = value;
                RaisePropertyChanged(nameof(Loves));
            }
        }

        private StructureEntity LovedOnesEntity = null;
        private StructureEntity AlertEntity = null;
        private bool isInitData = false;

        public override async Task Initialize()
        {

            GetLovedOnesEntity();
            GetAlertEntity();

            GetFeeds();
            await GetLoves();
            GetAlerts();
            await Task.CompletedTask;
        }

        private void GetLovedOnesEntity()
        {
            var settings = Utils.Utils.GetSettings();
            if (settings != null)
            {
                LovedOnesEntity = Utils.Utils.GetSettings().Find((StructureEntity entity) =>
                {
                    return entity.EntityName == Utils.Constansts.LOVED_ONES_ENTITY_SETTING_KEY;
                });
            }
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

        private async Task GetLoves()
        {
            IsLoading = true;
            if (LovedOnesEntity != null)
            {
                var response = await AppService.Instances.GetListLovedOnes(LovedOnesEntity.Id);
                if (response.IsSuccess)
                {
                    Loves = response.ResponseObject.Select((Entity<Love> entityLove) =>
                    {
                        var love = entityLove.Data;
                        love.Id = entityLove.Id;
                        return new LoveItemViewModel(love)
                        {
                            ItemClickCommand = new Command(() =>
                            {
                                CreatingLove = love;
                            })
                        };
                    }).ToList();
                }
            }
            IsLoading = false;
        }

        public Command ResetCommand
        {
            get
            {
                return new Command(ResetData);
            }
        }

        public Command CreateAlertCommand
        {
            get
            {
                return new Command(CreateAlert);
            }
        }

        private void ResetData()
        {
            CreatingLove = null;
            AlertDetail = null;
            IsYourPersonalNetwork = false;
            IsLowEnforcement = false;
            IsGuardianNearby = false;
            Detail = "";
            AlertPosition = null;
        }

        private async void CreateAlert()
        {
            if (AlertEntity != null)
            {
                IsLoading = true;
                var alertToPost = new Alert()
                {
                    Id = "",
                    FirstName = mCreatingLove.FullName,
                    AlertId = System.Guid.NewGuid().ToString(),
                    ProfileId = mCreatingLove.ProfileId,
                    CreatedOn = DateTime.UtcNow.ToString(),
                    Description = Detail,
                    Latitude = AlertPosition != null ? AlertPosition.Value.Latitude+"" : "",
                    Lognitude = AlertPosition != null ? AlertPosition.Value.Longitude + "" : "",
                };


                ApiResponse<EntityResponse<Alert>> response = await AppService.Instances.CreateAlert(AlertEntity.Id, alertToPost);

                IsLoading = false;
                if (response.IsSuccess && response.ResponseObject != null)
                {
                    if (response.ResponseObject.IsSuccessful)
                    {
                        IsUpdateSuccess = true;
                        ResetData();
                        GetAlerts();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", response.ResponseObject.Message, "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
                }
            }
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

        private async void GetAlerts()
        {
            var alerts = new ObservableCollection<object>();
            var liveHeaderItem = new GroupHeaderItemViewModel()
            {
                Title = "Live",
                Description = "There aren’t any active alerts near you.",
                IsEmptyList = true
            };
            List<AlertItemViewModel> listLiveItem = new List<AlertItemViewModel>();
            IsLoading = true;
            if (AlertEntity != null)
            {
                var response = await AppService.Instances.GetListAlert(AlertEntity.Id);
                if (response.IsSuccess)
                {
                    listLiveItem = response.ResponseObject.Select((Entity<Alert> entityAlert) =>
                    {
                        AlertItemViewModel item = new AlertItemViewModel(entityAlert.Data)
                        {
                            OnUpdateItemAction = OnUpdateItemList,
                            OnUpdateExpanded = OnItemExpandedUpdate,
                            
                        };
                        item.ItemClickCommand = new Command(() =>
                        {
                            AlertDetail = item.Model;
                        });
                        item.Model.Love = this.Loves.Find((LoveItemViewModel loveItemViewModel) =>
                        {
                            return loveItemViewModel.Model.ProfileId == item.Model.ProfileId;
                        }).Model;
                        return item;
                    }).ToList();
                }
            }
            var historyHeaderItem = new GroupHeaderItemViewModel()
            {
                Title = "History",
                Description = "You don’t have any past alerts.",
                IsEmptyList = true
            };
            liveHeaderItem.IsEmptyList = listLiveItem.Count == 0;
            alerts.Add(liveHeaderItem);
            foreach(AlertItemViewModel item in listLiveItem)
            {
                alerts.Add(item);
            }
            alerts.Add(historyHeaderItem);
            Alerts = alerts;
            IsLoading = false;
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
