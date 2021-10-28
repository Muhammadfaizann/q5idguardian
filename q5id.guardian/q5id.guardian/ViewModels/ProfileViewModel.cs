using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using q5id.guardian.Services;
using q5id.guardian.Services.Bases;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class ProfileViewModel : BaseViewModel<User, User>
    {
        public string TitlePage
        {
            get
            {
                if(User != null)
                {
                    return "Profile";
                }
                return "Sign Up";
            }
        }

        private void UpdateProperties()
        {
            if (User != null)
            {
                Email = User.Email;
                FirstName = User.FirstName;
                LastName = User.LastName;;
                Phone = User.Phone;
                Image = User.ImageUrl;
            }
            else
            {
                OnResetData(null);
            }
        }

        private User mUser = null;
        public User User
        {
            get => mUser;
            set
            {
                mUser = value;
                UpdateProperties();
            }
        }

        public bool IsUpdate
        {
            get
            {
                return User != null;
            }
        }

        private string mEmail = null;
        public string Email
        {
            get => mEmail;
            set
            {
                mEmail = value;
            }
        }

        private string mPhone = null;
        public string Phone
        {
            get => mPhone;
            set
            {
                mPhone = value;
            }
        }

        private string mPassword = null;
        public string Password
        {
            get => mPassword;
            set
            {
                mPassword = value;
            }
        }

        private string mFirstName = null;
        public string FirstName
        {
            get => mFirstName;
            set
            {
                mFirstName = value;
            }
        }

        private string mLastName = null;
        public string LastName
        {
            get => mLastName;
            set
            {
                mLastName = value;
            }
        }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }

        }

        private ImageData mProfileImage = null;
        public ImageData ProfileImage
        {
            get
            {
                return mProfileImage;
            }
            set
            {
                mProfileImage = value;
            }
        }

        private string mImage = null;
        public string Image
        {
            get
            {
                return mImage;
            }
            set
            {
                mImage = value;
            }
        }

        public Command CreateUpdateCommand
        {
            get
            {
                return new Command(CreateUpdateUser);
            }
        }

        public Command ResetCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (User == null)
                    {
                        OnResetData(null);
                    }
                    else
                    {
                        User = null;
                    }
                });
            }
        }

        public Command BackCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await NavigationService.Close(this, User);
                });
            }
        }

        public ProfileViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }

        private void OnResetData(object obj)
        {
            this.Email = "";
            this.FirstName = "";
            this.LastName = "";
            this.ProfileImage = null;
            this.Image = "";
            this.Phone = "";
        }

        private bool isInitData = false;

        public override async Task Initialize()
        {
            await Task.CompletedTask;
        }

        private async void CreateUpdateUser()
        {
            if(await CheckFormAsync() == false)
            {
                return;
            }
            IsLoading = true;
            
            var userToPost = new User()
            {
                Id = User != null ? User.Id : null,
                UserId = User != null ? User.UserId : System.Guid.NewGuid().ToString(),
                RoleId = User != null ? User.RoleId : "",
                Email = Email,
                Phone = Phone,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName,
                FullName = FullName,
                CreatedOn = User != null ? User.CreatedOn : DateTime.UtcNow.ToString(),
                ModifiedOn = User != null ? DateTime.UtcNow.ToString() : "",
                ImageUrl = User != null ? User.ImageUrl : "",
                SubscriptionExpiredDate = User != null ? User.SubscriptionExpiredDate : null,
            };
            if (ProfileImage != null)
            {
                var responseUploadImageOne = await AppApiManager.Instances.UploadImage(Utils.Constansts.USER_ENTITY_SETTING_KEY, ProfileImage);
                if (responseUploadImageOne.IsSuccess)
                {
                    userToPost.ImageUrl = responseUploadImageOne.ResponseObject.Result;
                }
            }

            ApiResponse<AppServiceResponse<User>> response = new ApiResponse<AppServiceResponse<User>>();
            if (User != null)
            {
                //Update flow
                var responseUpdate = await AppApiManager.Instances.UpdateUser(userToPost);
                response.IsSuccess = responseUpdate.IsSuccess;
                response.Errors = responseUpdate.Errors;
                response.Message = responseUpdate.Message;
                response.ResponseStatusCode = responseUpdate.ResponseStatusCode;
                response.ResponseObject = responseUpdate.ResponseObject;
            }
            else
            {
                //Create flow
                var responseCreate = await AppApiManager.Instances.CreateAccount(userToPost);
                response.IsSuccess = responseCreate.IsSuccess;
                response.Errors = responseCreate.Errors;
                response.Message = responseCreate.Message;
                response.ResponseStatusCode = responseCreate.ResponseStatusCode;
                response.ResponseObject = new AppServiceResponse<User>()
                {
                    IsError = false,
                    StatusCode = responseCreate.ResponseStatusCode,
                    Result = responseCreate.ResponseObject?.Data
                };
            }
            IsLoading = false;
            
            if (response.IsSuccess && response.ResponseObject != null && response.ResponseObject.Result != null)
            {
                Image = userToPost.ImageUrl;
                ProfileImage = null;
                
                if(User == null)
                {
                    var user = await GetUser(userToPost.Email, userToPost.Password);
                    if(user != null)
                    {
                        await App.Current.MainPage.DisplayAlert("SignUp Successfully", "", "OK");
                        Utils.Utils.SaveToken(user);
                        await ClearStackAndNavigateToPage<HomeViewModel, User>(user);
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Updated Successfully", "", "OK");
                    User = userToPost;
                }
            }
            else if(response.ResponseObject != null)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.ResponseObject.Error, "OK");
            }

        }

        private async Task<User> GetUser(string username, string password)
        {
            IsLoading = true;
            var currentUserResponse = await AppApiManager.Instances.Login(username, password);
            IsLoading = false;
            if (currentUserResponse.IsSuccess && currentUserResponse.ResponseObject != null)
            {
                var user = currentUserResponse.ResponseObject;
                Utils.Utils.SaveToken(user);
                return user;
            }

            return null;
        }

        private async Task<bool> CheckFormAsync()
        {
            if(Email == null || Email == "")
            {
                await App.Current.MainPage.DisplayAlert("Email is required", "", "OK");
                return false;
            }
            if (IsUpdate == false && (Password == null || Password == ""))
            {
                await App.Current.MainPage.DisplayAlert("Password is required", "", "OK");
                return false;
            }
            if (FirstName == null || FirstName == "")
            {
                await App.Current.MainPage.DisplayAlert("Firstname is required", "", "OK");
                return false;
            }
            if (LastName == null || LastName == "")
            {
                await App.Current.MainPage.DisplayAlert("Lastname is required", "", "OK");
                return false;
            }
            return true;
        }

        public override void Prepare(User parameter)
        {
            User = parameter;
        }
    }
}
