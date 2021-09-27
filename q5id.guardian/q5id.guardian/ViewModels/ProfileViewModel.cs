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
    public class ProfileViewModel : BaseViewModel
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
                NickName = User.NickName;
                FirstName = User.FirstName;
                LastName = User.LastName;
                BirthDay = DateTime.Parse(User.BirthDate);
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

        private string mNickName = null;
        public string NickName
        {
            get => mNickName;
            set
            {
                mNickName = value;
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

        private DateTime? mBirthDay = null;
        public DateTime? BirthDay
        {
            get => mBirthDay;
            set
            {
                mBirthDay = value;
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
                    await ClosePage();
                });
            }
        }

        public ProfileViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }

        private void OnResetData(object obj)
        {
            this.NickName = "";
            this.FirstName = "";
            this.LastName = "";
            this.BirthDay = null;
            this.ProfileImage = null;
            this.Image = "";
        }

        private StructureEntity ContactEntity = null;
        private bool isInitData = false;

        public override async Task Initialize()
        {
            GetContactEntity();
            await Task.CompletedTask;
        }

        private void GetContactEntity()
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

        private async void CreateUpdateUser()
        {
            if(ContactEntity == null)
            {
                return;
            }
            IsLoading = true;
            
            var userToPost = new User()
            {
                Id = User != null ? User.Id : null,
                ContactId = User != null ? User.ContactId : System.Guid.NewGuid().ToString(),
                NickName = NickName,
                FirstName = FirstName,
                LastName = LastName,
                FullName = FullName,
                BirthDate = GetStrDateOfBirth(),
                CreatedOn = User != null ? User.CreatedOn : DateTime.UtcNow.ToString(),
                ModifiedOn = User != null ? DateTime.UtcNow.ToString() : "",
                ImageUrl = User != null ? User.ImageUrl : "",
            };
            if (ProfileImage != null)
            {
                var responseUploadImageOne = await AppApiManager.Instances.UploadImage(ContactEntity.Id, ProfileImage);
                if (responseUploadImageOne.IsSuccess)
                {
                    userToPost.ImageUrl = responseUploadImageOne.ResponseObject.Path;
                }
            }

            ApiResponse<AppServiceResponse<EntityResponse<User>>> response;
            if (User != null)
            {
                //Update flow
                response = await AppApiManager.Instances.UpdateUser(ContactEntity.Id, userToPost);
            }
            else
            {
                //Create flow
                response = await AppApiManager.Instances.CreateUser(ContactEntity.Id, userToPost);
            }
            IsLoading = false;
            if (response.IsSuccess && response.ResponseObject != null)
            {
                Image = userToPost.ImageUrl;
                ProfileImage = null;
                await App.Current.MainPage.DisplayAlert("Updated Successfully", "", "OK");
                if(User == null)
                {
                    await NavigationService.Close(this);
                }
            }
            
        }

        private string GetStrDateOfBirth()
        {
            if (BirthDay != null)
            {
                return BirthDay.Value.ToString(Utils.Constansts.BIRTHDAY_DATE_FORMAT);
            }
            return "";
        }
    }
}
