
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
using q5id.guardian.ViewModels.Base;
using q5id.guardian.ViewModels.ItemViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace q5id.guardian.ViewModels
{
    public class AlertsViewModel : BaseSubViewModel
    {
        private Boolean mIsUpdateSuccess = false;
        public Boolean IsUpdateSuccess
        {
            get => mIsUpdateSuccess;
            set
            {
                mIsUpdateSuccess = value;
            }
        }

        private bool mIsYourPersonalNetwork = false;
        public bool IsYourPersonalNetwork
        {
            get => mIsYourPersonalNetwork;
            set
            {
                mIsYourPersonalNetwork = value;
            }
        }

        private bool mIsGuardianNearby = false;
        public bool IsGuardianNearby
        {
            get => mIsGuardianNearby;
            set
            {
                mIsGuardianNearby = value;
            }
        }

        private bool mIsLowEnforcement = false;
        public bool IsLowEnforcement
        {
            get => mIsLowEnforcement;
            set
            {
                mIsLowEnforcement = value;
            }
        }

        public bool IsOwner
        {
            get
            {
                if (User != null && AlertDetail != null)
                {
                    return User.Id == AlertDetail.CreatedBy;
                }
                return false;
            }
        }

        public bool IsShowAlertComment
        {
            get
            {
                if (AlertDetail != null && AlertDetail.IsEnded == false)
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
            }
        }

        public bool IsCanJoinLooking
        {
            get
            {
                if (AlertDetail != null && AlertDetail.IsEnded == false && IsOwner == false)
                {
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

        private ObservableCollection<object> mAlerts;
        public ObservableCollection<object> Alerts
        {
            get => mAlerts;
            set
            {
                mAlerts = value;
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
                UpdateIsEndAlert();
                GetFeeds();
            }
        }

        private void UpdateIsEndAlert()
        {
            if (mAlertDetail != null && IsOwner)
            {
                IsCanEndAlert = !mAlertDetail.IsClosed;
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
                if (mAlertDetail != null)
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
            }
        }

        private string mDetail;
        public string Detail
        {
            get => mDetail;
            set
            {
                mDetail = value;
            }
        }

        private List<object> mFeeds;
        public List<object> Feeds
        {
            get => mFeeds;
            set
            {
                mFeeds = value;
            }
        }

        private List<LoveItemViewModel> mMyLoves = null;
        public List<LoveItemViewModel> MyLoves
        {
            get => mMyLoves;
            set
            {
                mMyLoves = value;
            }
        }

        private bool isInitData = false;

        public override async Task Initialize()
        {

            GetMyLoves();
            await Task.CompletedTask;
        }

        public async void GetMyLoves()
        {
            if (User == null)
            {
                return;
            }
            IsLoading = true;
            var response = await AppApiManager.Instances.GetListLovedOnes(User.Id);
            IsLoading = false;
            if (response.IsSuccess && response.ResponseObject != null)
            {
                MyLoves = response.ResponseObject.Select((Love love) =>
                {
                    return new LoveItemViewModel(love)
                    {
                        OnItemClicked = () =>
                        {
                            CreatingLove = love;
                        }
                    };
                }).ToList();
            }
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
            if (User != null)
            {
                IsLoading = true;
                string alertId = System.Guid.NewGuid().ToString();
                var alertToPost = new Alert()
                {
                    CreatedBy = User.Id,
                    FirstName = mCreatingLove.FullName,
                    ProfileId = mCreatingLove.ProfileId,
                    UserId = Utils.Utils.GetUserId(),
                    CreatedOn = DateTime.UtcNow.ToString(),
                    Description = Detail,
                    Latitude = AlertPosition != null ? AlertPosition.Value.Latitude : 0,
                    Longitude = AlertPosition != null ? AlertPosition.Value.Longitude : 0,
                    IsClosed = false
                };

                if (AlertPosition != null)
                {
                    string address = await Utils.Utils.FindPlaceByPosition(AlertPosition.Value);
                    alertToPost.Address = address;
                }
                ApiResponse<AppServiceResponse<Alert>> response = await AppApiManager.Instances.CreateAlert(alertToPost);

                IsLoading = false;
                if (response.IsSuccess && response.ResponseObject != null)
                {
                    if (response.ResponseObject.IsError == false)
                    {
                        IsUpdateSuccess = true;
                        
                        // Trigger SOS
                        if(IsLowEnforcement)
                        {
                            var resp = await AppApiManager.Instances.TriggerRapidSOS(new RapidSOSRequest()
                            {
                                AlertId = response?.ResponseObject?.Result?.data?.alertId
                            });

                            if(!string.IsNullOrWhiteSpace(resp.ResponseObject.Message))
                            {
                                await App.Current.MainPage.DisplayAlert("Info", resp.ResponseObject.Message, "OK");
                            }
                        }

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

        public Command GetMyLovedOnesCommand
        {
            get
            {
                return new Command(() =>
                {
                    GetMyLoves();
                });
            }
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

        public Command EndAlertCommand
        {
            get
            {
                return new Command(EndAlert);
            }
        }

        private async void EndAlert()
        {
            if (AlertDetail != null)
            {
                IsLoading = true;
                var alertToPost = AlertDetail;
                alertToPost.IsClosed = true;
                ApiResponse<AppServiceResponse<Alert>> response = await AppApiManager.Instances.UpdateAlert(alertToPost);
                IsLoading = false;
                if (response.IsSuccess && response.ResponseObject != null)
                {
                    if (response.ResponseObject.IsError == false)
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
            if (AlertDetail != null)
            {
                IsLoading = true;
                var userPosition = await Utils.Utils.GetLocalLocation();
                var feedToPost = new Feed()
                {
                    VolunteerName = User.FirstName + " " + User.LastName,
                    CreatedBy = User.Id,
                    UserId = Utils.Utils.GetUserId(),
                    AlertId = AlertDetail.AlertId,
                    Timestamp = DateTime.UtcNow.ToString(),
                    CreatedOn = DateTime.UtcNow.ToString(),
                    Latitude = userPosition.Latitude, // userPosition != null ? userPosition.Latitude + "" : "",
                    Longitude = userPosition.Longitude,//userPosition != null ? userPosition.Longitude + "" : "",
                    Action = "is looking",
                };
                ApiResponse<AppServiceResponse<Feed>> response = await AppApiManager.Instances.CreateFeed(feedToPost);
                IsLoading = false;
                if (response.IsSuccess && response.ResponseObject != null)
                {
                    if (response.ResponseObject.IsError == false)
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
            if (AlertDetail != null && FeedMessage != "" && FeedMessage != null)
            {
                IsLoading = true;
                var userPosition = await Utils.Utils.GetLocalLocation();
                var feedToPost = new Feed()
                {
                    VolunteerName = User.FirstName + " " + User.LastName,
                    CreatedBy = User.Id,
                    UserId = Utils.Utils.GetUserId(),
                    AlertId = AlertDetail.AlertId,
                    Timestamp = DateTime.UtcNow.ToString(),
                    CreatedOn = DateTime.UtcNow.ToString(),
                    Latitude =  userPosition.Latitude, //userPosition != null ? userPosition.Latitude + "" : "",
                    Longitude = userPosition.Longitude, //userPosition != null ? userPosition.Longitude + "" : "",
                    Action = "posted ",
                    Comment = FeedMessage
                };
                ApiResponse<AppServiceResponse<Feed>> response = await AppApiManager.Instances.CreateFeed(feedToPost);
                IsLoading = false;
                if (response.IsSuccess && response.ResponseObject != null)
                {
                    if (response.ResponseObject.IsError == false)
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
            if (mAlertDetail != null)
            {
                var response = await AppApiManager.Instances.GetAlertDetail(mAlertDetail.AlertId);
                if(response.IsSuccess && response.ResponseObject != null && response.ResponseObject.Count > 0)
                {
                    var alert = response.ResponseObject.First();
                    if(alert.AlertFeeds != null)
                    {
                        alert.AlertFeeds.ForEach((Feed feed) =>
                        {
                            var item = new FeedItemViewModel(feed)
                            {
                                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQyX16xukQ-ZF5Obira-CMZqamIbFPfaMeB557mzjZjsbZ3h97l3LlXihU5VGiDtegDvo0",
                                Name = feed.CreatedBy == User.Id ? "You" : feed.VolunteerName,
                                Action = " " + feed.Action,
                                UpdatedTime = feed.AddedTime != null ? feed.AddedTime.Value.ToString("MM/dd/yyyy HH:mm") : "",
                                IsParent = false,
                                Detail = feed.Comment != "" && feed.Comment != null ? $"“{feed.Comment}”" : null,
                            };
                            result.Add(item);
                        });
                    }
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

        public async void GetAlerts()
        {
            var alerts = new ObservableCollection<object>();
            
            List<AlertItemViewModel> listAlertItem = new List<AlertItemViewModel>();
            IsLoading = true;
            var userPosition = await Utils.Utils.GetLocalLocation();
            var userLocation = userPosition != null ? new Location(userPosition.Latitude, userPosition.Longitude) : null;
            var alertsResponse = await Task.WhenAll<List<Alert>>(GetNearbyAlerts(), GetHistoryFeedAlerts());
            List<Alert> listAllAlert = new List<Alert>();
            for(int i = 0; i < alertsResponse.Length; i++)
            {
                listAllAlert = listAllAlert.Union(alertsResponse[i], new AlertComparer()).Where((Alert alert) => {
                    return alert.Love != null;
                }).ToList();
            }
            listAlertItem = listAllAlert.Select((Alert alert) =>
            {
                AlertItemViewModel item = new AlertItemViewModel(alert)
                {
                    OnUpdateItemAction = OnUpdateItemList,
                    OnUpdateExpanded = OnItemExpandedUpdate,

                };
                item.Model.DistanceFromUser = Alert.GetDistanceFrom(item.Model, userLocation);
                item.OnOpenAlert = () =>
                {
                    AlertDetail = item.Model;
                };
                return item;
            }).ToList();
            List<AlertItemViewModel> listLiveItem = listAlertItem.Where((AlertItemViewModel item) =>
            {
                return item.Model.IsClosed != true;
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
                return item.Model.IsClosed == true;
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

        private async Task<List<Alert>> GetNearbyAlerts()
        {
            var currentUserPosition = await Utils.Utils.GetLocalLocation();
            if(currentUserPosition != null)
            {
                var response = await AppApiManager.Instances.GetNearbyListAlert(currentUserPosition.Latitude, currentUserPosition.Longitude, Utils.Constansts.KM_DEFAULT_MAP_ZOOM_DISTANCT);
                if(response.IsSuccess && response.ResponseObject != null)
                {
                    return response.ResponseObject;
                }
            }
            return new List<Alert>();
        }

        private async Task<List<Alert>> GetMyAlerts()
        {
            var response = await AppApiManager.Instances.GetListAlert();
            if (response.IsSuccess && response.ResponseObject != null)
            {
                return response.ResponseObject;
            }
            return new List<Alert>();
        }

        private async Task<List<Alert>> GetHistoryFeedAlerts()
        {
            var response = await AppApiManager.Instances.GetListFeedHistoryAlert();
            if (response.IsSuccess && response.ResponseObject != null)
            {
                return response.ResponseObject;
            }
            return new List<Alert>();
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

        public IMvxNavigationService AlertNavigationService { get; set; }
        public ILoggerFactory LogProvider { get; set; }
        public AlertsViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            AlertNavigationService = navigationService;
            LogProvider = logProvider;
        }
    }
}
