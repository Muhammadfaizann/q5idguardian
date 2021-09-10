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
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginVolunteerCommand { get; }

        public Command LoginSubscriberCommand { get; }

        public Command SignUpCommand { get; }

        public Command BillingCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await MakePurchase();
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
                RaisePropertyChanged(nameof(UserName));
            }
        }

        public StructureEntity ContactEntity { get; private set; }

        public LoginViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            LoginVolunteerCommand = new Command(OnLoginAsVolClicked);
            LoginSubscriberCommand = new Command(OnLoginAsSubClicked);
            SignUpCommand = new Command(OnSignUpClicked);
            GetAccountEntity();
        }

        private async void OnSignUpClicked(object obj)
        {
            await NavigationService.Navigate<ProfileViewModel>();
        }

        private async void OnLoginClicked(User user)
        {
            await ClearStackAndNavigateToPage<HomeViewModel, User>(user);
        }

        private async void OnLoginAsSubClicked(object obj)
        {
            var user = await GetUser();
            if(user != null)
            {
                user.Role = UserRole.Subscriber;
                this.OnLoginClicked(user);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Login Failed", "OK");
            }
        }

        private async void OnLoginAsVolClicked(object obj)
        {
            var user = await GetUser();
            if (user != null)
            {
                user.Role = UserRole.Volunteer;
                this.OnLoginClicked(user);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Login Failed", "OK");
            }
        }

        private void GetAccountEntity()
        {
            var settings = Utils.Utils.GetSettings();
            if (settings != null)
            {
                ContactEntity = Utils.Utils.GetSettings().Find((StructureEntity entity) =>
                {
                    return entity.EntityName == Utils.Constansts.CONTACT_ENTITY_SETTING_KEY;
                });
            }
        }

        private async Task<User> GetUser()
        {
            if(mUserName != "" && ContactEntity != null)
            {
                IsLoading = true;
                var currentUserResponse = await AppService.Instances.GetUsers(ContactEntity.Id, mUserName);
                if (currentUserResponse.IsSuccess && currentUserResponse.ResponseObject.Value.Count > 0)
                {
                    var validUser = currentUserResponse.ResponseObject.Value.Find((User user) =>
                    {
                        return user.NickName == mUserName;
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

        private async Task OnSignUpClicked()
        {
            await NavigationService.Navigate<AuthenFaceViewModel>();
        }

        public async Task MakePurchase()
        {
            await NavigationService.Navigate<IAPViewModel>();
        }
    }
}
