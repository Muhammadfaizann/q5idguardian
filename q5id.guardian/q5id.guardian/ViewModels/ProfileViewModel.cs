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

        private string mEmail = null;
        public string Email
        {
            get => mEmail;
            set
            {
                mEmail = value;
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
        }

        private StructureEntity UserEntity = null;
        private bool isInitData = false;

        public override async Task Initialize()
        {
            GetUserEntity();
            await Task.CompletedTask;
        }

        private void GetUserEntity()
        {
            var settings = Utils.Utils.GetSettings();
            if (settings != null)
            {
                UserEntity = Utils.Utils.GetSettings().Find((StructureEntity entity) =>
                {
                    return entity.EntityName == Utils.Constansts.USER_ENTITY_SETTING_KEY;
                });
            }
        }

        private async void CreateUpdateUser()
        {
            if(UserEntity == null)
            {
                return;
            }
            IsLoading = true;
            
            var userToPost = new User()
            {
                Id = User != null ? User.Id : null,
                UserId = User != null ? User.UserId : System.Guid.NewGuid().ToString(),
                Email = Email,
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
                var responseUploadImageOne = await AppApiManager.Instances.UploadImage(UserEntity.EntityName, ProfileImage);
                if (responseUploadImageOne.IsSuccess)
                {
                    userToPost.ImageUrl = responseUploadImageOne.ResponseObject.Result;
                }
            }

            ApiResponse<AppServiceResponse<Entity<User>>> response;
            if (User != null)
            {
                //Update flow
                response = await AppApiManager.Instances.UpdateUser(UserEntity.Id, userToPost);
            }
            else
            {
                //Create flow
                response = await AppApiManager.Instances.CreateUser(UserEntity.Id, userToPost);
            }
            IsLoading = false;
            if (response.IsSuccess && response.ResponseObject != null)
            {
                Image = userToPost.ImageUrl;
                ProfileImage = null;
                
                await App.Current.MainPage.DisplayAlert("Updated Successfully", "", "OK");
                if(User == null)
                {
                    await NavigationService.Close(this, User);
                }
                else
                {
                    User = userToPost;
                }
            }
        }

        public override void Prepare(User parameter)
        {
            User = parameter;
        }
    }
}
