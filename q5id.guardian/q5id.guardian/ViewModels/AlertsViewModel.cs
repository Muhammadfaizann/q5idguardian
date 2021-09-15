
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
using Xamarin.Essentials;
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

        public bool IsOwner
        {
            get
            {
                if(mUser != null && mAlertDetail != null)
                {
                    return mUser.Id == mAlertDetail.CreatedBy;
                }
                return false;
            }
        }

        public bool IsShowAlertComment
        {
            get
            {
                if (mAlertDetail != null && mAlertDetail.IsEnded == false)
                {
                    if (IsOwner)
                    {
                        return true;
                    }
                    if (this.Feeds == null)
                    {
                        return true;
                    }
                    var existJoin = this.Feeds.Find((object obj) =>
                    {
                        if (obj is FeedItemViewModel feedItemViewModel)
                        {
                            return feedItemViewModel.Model.CreatedBy == User.Id;
                        }
                        return false;
                    });
                    return existJoin != null;
                }
                return false;
            }
        }

        private bool mIsCanEndAlert = false;
        public bool IsCanEndAlert
        {
            get => mIsCanEndAlert;
            set
            {
                mIsCanEndAlert = value;
                RaisePropertyChanged(nameof(IsCanEndAlert));
            }
        }

        public bool IsCanJoinLooking
        {
            get
            {
                if(mAlertDetail != null && mAlertDetail.IsEnded == false && IsOwner == false)
                {
                    if(this.Feeds == null)
                    {
                        return true;
                    }
                    var existJoin = this.Feeds.Find((object obj) =>
                    {
                        if(obj is FeedItemViewModel feedItemViewModel)
                        {
                            return feedItemViewModel.Model.CreatedBy == User.Id;
                        }
                        return false;
                    });
                    return existJoin == null;
                }
                return false;
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
                RaisePropertyChanged(nameof(IsOwner));
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
                RaisePropertyChanged(nameof(AlertPositions));
                RaisePropertyChanged(nameof(IsOwner));
                RaisePropertyChanged(nameof(IsCanJoinLooking));
                UpdateIsEndAlert();
                GetFeeds();
            }
        }

        private void UpdateIsEndAlert()
        {
            if(mAlertDetail != null && IsOwner)
            {
                IsCanEndAlert = mAlertDetail.IsClosed != Utils.Constansts.YES_KEY;
            }
            else
            {
                IsCanEndAlert = false;
            }
        }

        public List<Position> AlertPositions
        {
            get
            {
                if(mAlertDetail != null)
                {
                    var result = new List<Position>();
                    result.Add(mAlertDetail.Position);
                    return result;
                }
                return null;
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

        private List<object> mFeeds;
        public List<object> Feeds
        {
            get => mFeeds;
            set
            {
                mFeeds = value;
                RaisePropertyChanged(nameof(Feeds));
                RaisePropertyChanged(nameof(IsCanJoinLooking));
                RaisePropertyChanged(nameof(IsShowAlertComment));
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
                RaisePropertyChanged(nameof(MyLoves));
            }
        }

        public List<LoveItemViewModel> MyLoves
        {
            get
            {
                if (mLoves != null)
                {
                    return mLoves.Where((LoveItemViewModel item) =>
                    {
                        return item.Model.CreatedBy == User.Id;
                    }).ToList();
                }
                return new List<LoveItemViewModel>();
            }
        }

        private StructureEntity LovedOnesEntity = null;
        private StructureEntity AlertEntity = null;
        private StructureEntity FeedEntity = null;
        private bool isInitData = false;

        public override async Task Initialize()
        {

            GetLovedOnesEntity();
            GetAlertEntity();
            GetFeedEntity();
            await GetLoves();
            GetAlerts();
            await Task.CompletedTask;
        }

        private void GetFeedEntity()
        {
            var settings = Utils.Utils.GetSettings();
            if (settings != null)
            {
                FeedEntity = Utils.Utils.GetSettings().Find((StructureEntity entity) =>
                {
                    return entity.EntityName == Utils.Constansts.ALERT_FEED_ENTITY_SETTING_KEY;
                });
            }
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
                var response = await AppService.Instances.GetListLovedOnes(LovedOnesEntity.Id, null);
                if (response.IsSuccess && response.ResponseObject != null && response.ResponseObject.Value != null)
                {
                    Loves = response.ResponseObject.Value.Select((Love love) =>
                    {
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
            FeedMessage = "";
            AlertPosition = null;
            Feeds = new List<object>();
        }

        private async void CreateAlert()
        {
            if (AlertEntity != null && User != null)
            {
                IsLoading = true;
                var alertToPost = new Alert()
                {
                    CreatedBy = User.Id,
                    FirstName = mCreatingLove.FullName,
                    AlertId = System.Guid.NewGuid().ToString(),
                    ProfileId = mCreatingLove.ProfileId,
                    CreatedOn = DateTime.UtcNow.ToString(),
                    Description = Detail,
                    Latitude = AlertPosition != null ? AlertPosition.Value.Latitude+"" : "",
                    Lognitude = AlertPosition != null ? AlertPosition.Value.Longitude + "" : "",
                    IsClosed = Utils.Constansts.NO_KEY
                };

                if (AlertPosition != null)
                {
                    GooglePlace placeJson = await GoogleMapsApiService.Instances.FindPlaceByPosition(AlertPosition.Value);
                    if(placeJson != null)
                    {
                        alertToPost.Address = placeJson.Name;
                    }
                }
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

        public Command SubscriptionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await NavigationService.Navigate<IAPViewModel, User>(mUser);
                });
            }
        }

        public Command EndAlertCommand
        {
            get
            {
                return new Command(EndAlert);
            }
        }

        private async void EndAlert()
        {
            if (AlertEntity != null && AlertDetail != null)
            {
                IsLoading = true;
                var alertToPost = AlertDetail;
                alertToPost.IsClosed = Utils.Constansts.YES_KEY;
                ApiResponse<EntityResponse<Alert>> response = await AppService.Instances.UpdateAlert(AlertEntity.Id, alertToPost);
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

        public Command JoinAlertCommand
        {
            get
            {
                return new Command(JoinAlert);
            }
        }

        private async void JoinAlert()
        {
            if (FeedEntity != null && AlertDetail != null)
            {
                IsLoading = true;
                var userPosition = await Utils.Utils.GetLocalLocation();
                var feedToPost = new Feed()
                {
                    AlertFeedId = System.Guid.NewGuid().ToString(),
                    VolunteerName = User.FullName,
                    CreatedBy = User.Id,
                    AlertId = AlertDetail.AlertId,
                    Timestamp = DateTime.UtcNow.ToString(),
                    CreatedOn = DateTime.UtcNow.ToString(),
                    Latitude = userPosition != null ? userPosition.Latitude + "" : "",
                    Lognitude = userPosition != null ? userPosition.Longitude + "" : "",
                    Action = "is looking",
                };
                ApiResponse<EntityResponse<Feed>> response = await AppService.Instances.CreateFeed(FeedEntity.Id, feedToPost);
                IsLoading = false;
                if (response.IsSuccess && response.ResponseObject != null)
                {
                    if (response.ResponseObject.IsSuccessful)
                    {
                        GetFeeds();
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

        private string mFeedMessage = "";
        public string FeedMessage
        {
            get => mFeedMessage;
            set
            {
                mFeedMessage = value;
                RaisePropertyChanged(nameof(FeedMessage));
            }
        }

        public Command PostFeedCommand
        {
            get
            {
                return new Command(PostFeed);
            }
        }

        private async void PostFeed()
        {
            if (FeedEntity != null && AlertDetail != null && FeedMessage != "" && FeedMessage != null)
            {
                IsLoading = true;
                var userPosition = await Utils.Utils.GetLocalLocation();
                var feedToPost = new Feed()
                {
                    AlertFeedId = System.Guid.NewGuid().ToString(),
                    VolunteerName = User.FullName,
                    CreatedBy = User.Id,
                    AlertId = AlertDetail.AlertId,
                    Timestamp = DateTime.UtcNow.ToString(),
                    CreatedOn = DateTime.UtcNow.ToString(),
                    Latitude = userPosition != null ? userPosition.Latitude + "" : "",
                    Lognitude = userPosition != null ? userPosition.Longitude + "" : "",
                    Action = "posted ",
                    Comment = FeedMessage
                };
                ApiResponse<EntityResponse<Feed>> response = await AppService.Instances.CreateFeed(FeedEntity.Id, feedToPost);
                IsLoading = false;
                if (response.IsSuccess && response.ResponseObject != null)
                {
                    if (response.ResponseObject.IsSuccessful)
                    {
                        FeedMessage = "";
                        GetFeeds();
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

        private async void GetFeeds()
        {
            var result = new List<object>();
            IsLoading = true;
            if (FeedEntity != null && mAlertDetail != null)
            {
                
                var response = await AppService.Instances.GetFeeds(FeedEntity.Id, mAlertDetail.AlertId);
                if (response.IsSuccess && response.ResponseObject != null && response.ResponseObject.Value != null)
                {
                    
                    response.ResponseObject.Value.ForEach((Feed feed) =>
                    {
                        var item = new FeedItemViewModel(feed)
                        {
                            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQyX16xukQ-ZF5Obira-CMZqamIbFPfaMeB557mzjZjsbZ3h97l3LlXihU5VGiDtegDvo0",
                            Name = feed.CreatedBy == User.Id ? "You" : feed.VolunteerName,
                            Action = " "+feed.Action,
                            UpdatedTime = feed.AddedTime != null ? feed.AddedTime.Value.ToString("MM/dd/yyyy HH:mm") : "",
                            IsParent = false,
                            Detail = feed.Comment != "" && feed.Comment != null ? $"“{feed.Comment}”" : null,
                        };
                        result.Add(item);
                    });
                }
                result.Reverse();
                result.Add(new FeedItemViewModel(new Feed())
                {
                    ImageUrl = "https://i1.wp.com/www.alphr.com/wp-content/uploads/2020/12/Facebook-How-to-Change-Profile-Picture.jpg",
                    Name = IsOwner ? "You " : "Parent ",
                    Action = "posted",
                    UpdatedTime = mAlertDetail.AddedTime != null ? mAlertDetail.AddedTime.Value.ToString("MM/dd/yyyy HH:mm") : "",
                    IsParent = true,
                    Detail = $"“{mAlertDetail.Description}”"
                });
            }
            IsLoading = false;
            Feeds = result;
        }

        private async void GetAlerts()
        {
            var alerts = new ObservableCollection<object>();
            
            List<AlertItemViewModel> listAlertItem = new List<AlertItemViewModel>();
            IsLoading = true;
            var userPosition = await Utils.Utils.GetLocalLocation();
            var userLocation = userPosition != null ? new Location(userPosition.Latitude, userPosition.Longitude) : null;
            if (AlertEntity != null)
            {
                var response = await AppService.Instances.GetListAlert(AlertEntity.Id);
                if (response.IsSuccess && response.ResponseObject.Value != null)
                {
                    listAlertItem = response.ResponseObject.Value.Select((Alert alert) =>
                    {
                        AlertItemViewModel item = new AlertItemViewModel(alert)
                        {
                            OnUpdateItemAction = OnUpdateItemList,
                            OnUpdateExpanded = OnItemExpandedUpdate,
                            
                        };
                        item.Model.DistanceFromUser = Alert.GetDistanceFrom(item.Model, userLocation);
                        item.ItemClickCommand = new Command(() =>
                        {
                            AlertDetail = item.Model;
                        });
                        if(this.Loves != null)
                        {
                            var selectedLoveItemViewModel = this.Loves.Find((LoveItemViewModel loveItemViewModel) =>
                            {
                                return loveItemViewModel.Model.ProfileId == item.Model.ProfileId;
                            });
                            if (selectedLoveItemViewModel != null)
                            {
                                item.Model.Love = selectedLoveItemViewModel.Model;
                            }
                        }
                        return item;
                    }).ToList();
                }
            }
            List<AlertItemViewModel> listLiveItem = listAlertItem.Where((AlertItemViewModel item) =>
            {
                return item.Model.IsClosed != Utils.Constansts.YES_KEY;
            }).ToList();
            var liveHeaderItem = new GroupHeaderItemViewModel()
            {
                Title = "Live",
                Description = "There aren’t any active alerts near you.",
                IsEmptyList = true
            };
            liveHeaderItem.IsEmptyList = listLiveItem.Count == 0;
            List<AlertItemViewModel> listHistoryItem = listAlertItem.Where((AlertItemViewModel item) =>
            {
                return item.Model.IsClosed == Utils.Constansts.YES_KEY;
            }).ToList();
            var historyHeaderItem = new GroupHeaderItemViewModel()
            {
                Title = "History",
                Description = "You don’t have any past alerts.",
                IsEmptyList = true
            };
            historyHeaderItem.IsEmptyList = listHistoryItem.Count == 0;
            alerts.Add(liveHeaderItem);
            foreach(AlertItemViewModel item in listLiveItem)
            {
                alerts.Add(item);
            }
            alerts.Add(historyHeaderItem);
            foreach (AlertItemViewModel item in listHistoryItem)
            {
                alerts.Add(item);
            }
            Alerts = alerts;
            IsLoading = false;
        }

        private void OnItemExpandedUpdate(AlertItemViewModel item)
        {
            var indexOf = Alerts.IndexOf(item);
            if(indexOf > -1 && indexOf < Alerts.Count)
            {
                Alerts[indexOf] = item;
            }
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
