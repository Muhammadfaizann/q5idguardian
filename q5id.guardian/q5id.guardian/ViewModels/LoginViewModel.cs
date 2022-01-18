using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.InAppBilling;
using q5id.guardian.DependencyServices;
using q5id.guardian.Models;
using q5id.guardian.Services;
using q5id.guardian.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public Command SignUpCommand { get; }

        public Command ForgotPasswordCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await NavigationService.Navigate<ForgotPasswordViewModel>();
                });
            }
        }


        private AuthResponse _authResp = null;

        private string mUserName = "";
        public string UserName
        {
            get => mUserName;
            set
            {
                mUserName = value;
            }
        }

        private string mPassword = "";
        public string Password
        {
            get => mPassword;
            set
            {
                mPassword = value;
            }
        }

        public LoginViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            LoginCommand = new Command(OnLoginClicked);
            SignUpCommand = new Command(OnSignUpClicked);
        }

        private async void OnSignUpClicked(object obj)
        {
            // Ignore for now
            // await NavigationService.Navigate<ProfileViewModel>();
        }

        private async void OnLoginClicked(Object obj)
        {
            var result = await Login();
            if(result)
            {
                // Todo start polling
                Device.StartTimer(TimeSpan.FromSeconds(10), () =>
                {
                    IsLoading = true;
                    // do something every 10 seconds
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        
                        var resp = await AppApiManager.Instances.PollStatus(mUserName, _authResp);

                        if (resp.IsSuccess && resp.ResponseObject != null && resp.ResponseObject.Status != "Processing")
                        {
                            var user = resp.ResponseObject;
                            Utils.Utils.SaveToken(user);
                            IsLoading = false;
                            await ClearStackAndNavigateToPage<HomeViewModel, User>(user);
                            UpdateUserDevice(user);
                        }
                        // interact with UI elements
                    });
                    return IsLoading;
                });
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Login Failed", "OK");
            }
        }

        private async void UpdateUserDevice(User user)
        {
            var currentUserDevice = Utils.Utils.GetUserDevice();
            if(currentUserDevice != null)
            {
                await AppApiManager.Instances.DeleteUserDevice(currentUserDevice);
                Utils.Utils.SaveUserDevice(null);
            };
            var currentPushToken = Utils.Utils.GetPushNotificationToken();
            Debug.WriteLine("currentPushToken: ", currentPushToken);
            IAppDeviceService service = DependencyService.Get<IAppDeviceService>();
            var userDevice = new UserDevice()
            {
                UserId = user.UserId,
                DevicePushId = currentPushToken,
                Platform = Device.RuntimePlatform.ToLower(),
                IsAppPurchaseToken = "False",
                DeviceId = service.GetDeviceId(),
            };
            var responseCreateUserDevice = await AppApiManager.Instances.CreateUserDevice(userDevice);
            if (responseCreateUserDevice.IsSuccess && responseCreateUserDevice.ResponseObject != null && responseCreateUserDevice.ResponseObject.Result != null)
            {
                var newUserDevice = responseCreateUserDevice.ResponseObject.Result;
                Utils.Utils.SaveUserDevice(newUserDevice);
            }
        }

        private async Task<bool> Login()
        {
            // mUserName = "+1 7167081550";
            if (mUserName != "")
            {
                IsLoading = true;
                var currentUserResponse = await AppApiManager.Instances.Login(mUserName);
                if (currentUserResponse.IsSuccess && currentUserResponse.ResponseObject != null)
                {
                    _authResp = currentUserResponse.ResponseObject;
                    Utils.Utils.SavePIDToken(_authResp);
                    return true;
                }
            }
            
            return false;
        }

        //private async Task OnSignUpClicked()
        //{
        //    await NavigationService.Navigate<AuthenFaceViewModel>();
        //}

        //public async Task MakePurchase()
        //{
        //    await NavigationService.Navigate<IAPViewModel>();
        //}
    }
}
