using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Newtonsoft.Json;
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
        bool _completedPid = false;
    
        private async void OnLoginClicked(Object obj)
        {
            var result = await Login();
            if(result)
            {
                // Todo start polling
                Device.StartTimer(TimeSpan.FromSeconds(10), () =>
                {
                    // do something every 10 seconds
                    if (!_completedPid)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {

                            var resp = await AppApiManager.Instances.PollStatus(mUserName, _authResp);

                            if (resp.IsSuccess && resp.ResponseObject != null && resp.ResponseObject.Status != "Processing")
                            {
                                var user = resp.ResponseObject;
                                Debug.WriteLine(JsonConvert.SerializeObject(user));
                                Utils.Utils.SaveToken(user);
                                IsLoading = false;
                                _completedPid = true;
                                await ClearStackAndNavigateToPage<HomeViewModel, User>(user);
                            }
                        });
                        return true;
                    }else
                    {
                        return false;
                    }
                   
                });
            }
            else
            {
               IsLoading = false;
                await App.Current.MainPage.DisplayAlert("Error", "Login Failed", "OK");
            }
        }

        private async Task<bool> Login()
        {
            Utils.Utils.SavePIDToken(null);
#if DEBUG
            // 7167081550 5039159930  4086475274
            mUserName = "4086475274";
#endif

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
