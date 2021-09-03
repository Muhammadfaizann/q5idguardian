using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using q5id.guardian.Services;
using q5id.guardian.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public Command SignUpCommand { get; }

        private string mUserName = "Alvin Gold";
        public string UserName
        {
            get => mUserName;
            set
            {
                mUserName = value;
                RaisePropertyChanged(nameof(UserName));
            }
        }

        public StructureEntity AccountEntity { get; private set; }

        public LoginViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            LoginCommand = new Command(OnLoginAsVolClicked);
            SignUpCommand = new Command(OnLoginAsSubClicked);
            GetAccountEntity();
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
        }

        private async void OnLoginAsVolClicked(object obj)
        {
            var user = await GetUser();
            if (user != null)
            {
                user.Role = UserRole.Volunteer;
                this.OnLoginClicked(user);
            }
        }

        private void GetAccountEntity()
        {
            var settings = Utils.Utils.GetSettings();
            if (settings != null)
            {
                AccountEntity = Utils.Utils.GetSettings().Find((StructureEntity entity) =>
                {
                    return entity.EntityName == Utils.Constansts.ACCOUNT_ENTITY_SETTING_KEY;
                });
            }
        }

        private async Task<User> GetUser()
        {
            if(mUserName != "" && AccountEntity != null)
            {
                IsLoading = true;
                var currentUserResponse = await AppService.Instances.GetUsers(AccountEntity.Id, mUserName);
                if (currentUserResponse.IsSuccess && currentUserResponse.ResponseObject.Value.Count > 0)
                {
                    var validUser = currentUserResponse.ResponseObject.Value.Find((User user) =>
                    {
                        return user.AccountName == mUserName;
                    });
                    if(validUser != null)
                    {
                        IsLoading = false;
                        return validUser;
                    }
                }
                var newUser = new User()
                {
                    AccountName = mUserName,
                    AccountId = System.Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.UtcNow.ToString()
                };
                var createUserResponse = await AppService.Instances.CreateUser(AccountEntity.Id, newUser);
                if (createUserResponse.IsSuccess && createUserResponse.ResponseObject != null)
                {

                    var userEntity = createUserResponse.ResponseObject.Entity;
                    var user = userEntity.Data;
                    user.Id = userEntity.Id;
                    IsLoading = false;
                    return user;
                }
            }
            IsLoading = false;
            return null;
        }

        private async Task OnSignUpClicked()
        {
            await NavigationService.Navigate<AuthenFaceViewModel>();
        }
    }
}
