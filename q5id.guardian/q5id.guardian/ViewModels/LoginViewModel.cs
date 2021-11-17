using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.InAppBilling;
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
            await NavigationService.Navigate<ProfileViewModel>();
        }

        private async void OnLoginClicked(Object obj)
        {
            var user = await GetUser();
            if(user != null)
            {
                await ClearStackAndNavigateToPage<HomeViewModel, User>(user);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Login Failed", "OK");
            }
        }

        private async Task<User> GetUser()
        {
            if(mUserName != "" && mPassword != "")
            {
                IsLoading = true;
                var currentUserResponse = await AppApiManager.Instances.Login(mUserName, mPassword);
                IsLoading = false;
                if (currentUserResponse.IsSuccess && currentUserResponse.ResponseObject != null)
                {
                    var user = currentUserResponse.ResponseObject;
                    Utils.Utils.SaveToken(user);
                    return user;
                }
            }
            
            return null;
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
