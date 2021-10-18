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

        private string mUserName = "";
        public string UserName
        {
            get => mUserName;
            set
            {
                mUserName = value;
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
            if(mUserName != "")
            {
                IsLoading = true;
                var currentUserResponse = await AppApiManager.Instances.GetUsers(mUserName);
                if (currentUserResponse.IsSuccess && currentUserResponse.ResponseObject.Count > 0)
                {
                    var validUser = currentUserResponse.ResponseObject.Find((User user) =>
                    {
                        return user.Email == mUserName;
                    });
                    if(validUser != null)
                    {
                        IsLoading = false;
                        return validUser;
                    }
                }
            }
            IsLoading = false;
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
