using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.InAppBilling;
using q5id.guardian.DependencyServices;
using q5id.guardian.Models;
using q5id.guardian.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class HomeViewModel : BaseViewModel<User>
    {
        public HomeViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            OpenSettingCommand = new MvxAsyncCommand(OnSettingClicked);
            HomeVm = new HomeContentViewModel(navigationService, logProvider);
            HomeVm.OnUpdateModel += SubVmOnUpdateModel;
            LovedOnesVm = new LovedOnesViewModel(navigationService, logProvider);
            LovedOnesVm.OnUpdateModel += SubVmOnUpdateModel;
            AlertsVm = new AlertsViewModel(navigationService, logProvider);
            AlertsVm.OnUpdateModel += SubVmOnUpdateModel;
            Task.Run(async () =>
            {
                await HomeVm.Initialize();
               await GetSubscriptionStatus();
            });
        }

        private void SubVmOnUpdateModel(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await GetSubscriptionStatus();
            });
        }

        public HomeContentViewModel HomeVm { get; private set; }
        public LovedOnesViewModel LovedOnesVm { get; private set; }
        public AlertsViewModel AlertsVm { get; private set; }

        private User mUser;
        public User User
        {
            get => mUser;
            set
            {
                mUser = value;
                HomeVm.User = mUser;
                LovedOnesVm.User = mUser;
                AlertsVm.User = mUser;
            }
        }

        public Command OpenAlertCommand { get; }

        public MvxAsyncCommand OpenSettingCommand { get; }

        public Command ShowProfileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var result = await NavigationService.Navigate<ProfileViewModel, User, User>(User);
                    User = result;
                });
            }
        }

        public Command LogOutCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Utils.Utils.SaveToken(null);
                    var currentUserDevice = Utils.Utils.GetUserDevice();
                    if(currentUserDevice != null)
                    {
                        await AppApiManager.Instances.DeleteUserDevice(currentUserDevice);
                    }
                    await ClearStackAndNavigateToPage<LoginViewModel>();
                });
            }
        }

        public override void Prepare(User parameter)
        {
            User = parameter;
        }

        public override void ViewCreated()
        {
            base.ViewCreated();
            
        }

        public override void Start()
        {
            base.Start();
        }

        private async void OnServiceUnauthorized(object sender, EventArgs e)
        {
            Utils.Utils.SaveToken(null);
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await ClearStackAndNavigateToPage<IntroViewModel>();
            });
           
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            
            base.ViewDestroy(viewFinishing);
        }

        public override async Task Initialize()
        {           
            await LovedOnesVm.Initialize();
            await AlertsVm.Initialize();
        }      

        private async void GetProfile()
        {
            UserSession userSession = Utils.Utils.GetToken();
            if(userSession != null)
            {
                var currentUserResponse = await AppApiManager.Instances.GetUserProfile(userSession.UserId);
                if (currentUserResponse.IsSuccess && currentUserResponse.ResponseObject != null && currentUserResponse.ResponseObject.Count > 0)
                {
                    var result = currentUserResponse.ResponseObject[0];
                    User = result;
                }
            }
            else
            {
                await ClearStackAndNavigateToPage<LoginViewModel>();
            }
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
        }

        private async Task OnSettingClicked()
        {
            await NavigationService.Navigate<SettingViewModel>();
        }

        

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            GetProfile();
           
            AppApiManager.Instances.OnUnauthorized += OnServiceUnauthorized;
        }

        private async Task GetSubscriptionStatus()
        {
            HomeVm.IsLoading = true;
            Debug.WriteLine("≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥.");
            Debug.WriteLine("    Fetching Subscription Status   ");
            Debug.WriteLine("≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥||");
            Debug.WriteLine(".≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈|");
            Boolean isSubscriptionRole = false;

            try
            {
                var purchases = await InAppBillingService.Instances.GetProductPurchases();
                if (purchases != null)
                {
                    int numberOfSubscriptionDay = 30;
                    InAppBillingPurchase purchase = purchases.FirstOrDefault();
                    if (purchase != null)
                    {
                        if (purchase.State == PurchaseState.Purchased && purchase.TransactionDateUtc.AddDays(numberOfSubscriptionDay).Ticks > DateTime.UtcNow.Ticks)
                        {
                            var isExpired = await InAppBillingService.Instances.IsExpiredReceipt(purchase.PurchaseToken);
                            isSubscriptionRole = !isExpired;
                        }
                    }
                }
            }
            catch (Exception ex) {
                // todo log exception if not already logged
                Log.LogError(ex, ex.Message);
                Debug.WriteLine(ex.Message);
            }

#if DEBUG
            // optionally bypass subscription role checks for local debugging. REMEMBER to comment out any changes before checking in!
             //isSubscriptionRole = true;
#endif

            HomeVm.IsLoading = false;
            HomeVm.IsSubcriber = isSubscriptionRole;
            LovedOnesVm.IsSubcriber = isSubscriptionRole;
            AlertsVm.IsSubcriber = isSubscriptionRole;
        }

        public override void ViewDisappeared()
        {
            AppApiManager.Instances.OnUnauthorized -= OnServiceUnauthorized;
            base.ViewDisappeared();
        }

        public Command OpenHomeTapCommand
        {
            get
            {
                return new Command(async () =>
                {
                    HomeVm.GetAlerts();
                });
            }
        }

        public Command OpenLovedOnesTapCommand
        {
            get
            {
                return new Command(() =>
                {
                    LovedOnesVm.GetLoves();
                });
            }
        }

        public Command OpenAlertTapCommand
        {
            get
            {
                return new Command(async () =>
                {
                    AlertsVm.GetMyLoves();
                    AlertsVm.GetAlerts();
                });
            }
        }
    }
}