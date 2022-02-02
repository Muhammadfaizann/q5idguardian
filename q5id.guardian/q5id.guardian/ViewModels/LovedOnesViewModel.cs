using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using q5id.guardian.Services;
using q5id.guardian.Services.Bases;
using q5id.guardian.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class LovedOnesViewModel : BaseSubViewModel
    {
        private Love mSelectedLovedOnes = null;
        public Love SelectedLovedOnes
        {
            get => mSelectedLovedOnes;
            set
            {
                mSelectedLovedOnes = value;
                UpdateProperties();
            }
        }

        private void UpdateProperties()
        {
            if(SelectedLovedOnes != null)
            {
                var selectedLovedOnes = SelectedLovedOnes;
                FirstName = selectedLovedOnes.FirstName;
                LastName = selectedLovedOnes.LastName;
                BirthDay = DateTime.Parse(selectedLovedOnes.DateofBirth);
                if(HairColors != null)
                {
                    HairColor = HairColors.Find((ItemChoice choice) =>
                    {
                        return choice.Id == selectedLovedOnes.HairColorId;
                    });
                }
                if(EyeColors != null)
                {
                    EyeColor = EyeColors.Find((ItemChoice choice) =>
                    {
                        return choice.Id == selectedLovedOnes.EyeColorId;
                    });
                }
                if(HeightFeets != null)
                {
                    HeightFeet = HeightFeets.Find((ItemChoice choice) =>
                    {
                        return choice.Id == selectedLovedOnes.HeightFeetId;
                    });
                }
                if(ListHeightInches != null)
                {
                    HeightInches = ListHeightInches.Find((ItemChoice choice) =>
                    {
                        return choice.Id == selectedLovedOnes.HeightInchesId;
                    });
                }
                Weight = selectedLovedOnes.Weight;
                Detail = selectedLovedOnes.OtherInformation;
                PrimaryImage = null;
                SecondaryImages = new ObservableCollection<ImageData>()
                {
                    null, null, null, null,
                };
                Image = selectedLovedOnes.Image;
                Image2 = selectedLovedOnes.Image2;
                Image3 = selectedLovedOnes.Image3;
                Image4 = selectedLovedOnes.Image4;
                Image5 = selectedLovedOnes.Image5;
            }
            else
            {
                OnResetData(null);
            }
        }

        private Boolean mIsUpdateSuccess = false;
        public Boolean IsUpdateSuccess
        {
            get => mIsUpdateSuccess;
            set
            {
                mIsUpdateSuccess = value;
            }
        }

        private User mUser = null;
        public User User
        {
            get => mUser;
            set
            {
                mUser = value;
            }
        }

        private Boolean mIsSubcriber = false;
        public Boolean IsSubcriber
        {
            get => mIsSubcriber;
            set
            {
                mIsSubcriber = value;
            }
        }

        public Boolean IsVolunteer
        {
            get
            {
                return !IsSubcriber;
            }
        }

        private List<Love> mLoves = null;
        public List<Love> Loves
        {
            get => mLoves;
            set
            {
                mLoves = value;;
            }
        }

        public Boolean IsHasNoLove
        {
            get
            {
                return Loves == null || Loves.Count == 0;
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

        private string mImage2 = null;
        public string Image2
        {
            get => mImage2;
            set
            {
                mImage2 = value;
            }
        }

        private string mImage3 = null;
        public string Image3
        {
            get => mImage3;
            set
            {
                mImage3 = value;
            }
        }

        private string mImage4 = null;
        public string Image4
        {
            get => mImage4;
            set
            {
                mImage4 = value;
            }
        }

        private string mImage5 = null;
        public string Image5
        {
            get => mImage5;
            set
            {
                mImage5 = value;
            }
        }

        public DateTime CurrentDate
        {
            get => DateTime.Now;
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

        private ItemChoice mHeightFeet = null;
        public ItemChoice HeightFeet
        {
            get => mHeightFeet;
            set
            {
                mHeightFeet = value;
            }
        }

        private ItemChoice mHeightInches = null;
        public ItemChoice HeightInches
        {
            get => mHeightInches;
            set
            {
                mHeightInches = value;
            }
        }

        public string Height
        {
            get
            {
                if(HeightFeet == null || HeightInches == null)
                {
                    return "0";
                }
                return HeightFeet.Name + "’ " + HeightInches.Name + "”";
            }
        }

        private string mWeight = null;
        public string Weight
        {
            get => mWeight;
            set
            {
                mWeight = value;
            }
        }

        private string mDetail = null;
        public string Detail
        {
            get => mDetail;
            set
            {
                mDetail = value;
            }
        }

        private ItemChoice mHairColor = null;
        public ItemChoice HairColor
        {
            get
            {
                return mHairColor;
            }
            set
            {
                mHairColor = value;
            }
        }

        private ItemChoice mEyeColor = null;
        public ItemChoice EyeColor
        {
            get
            {
                return mEyeColor;
            }
            set
            {
                mEyeColor = value;
            }
        }

        private ImageData mPrimaryImage = null;
        public ImageData PrimaryImage
        {
            get
            {
                return mPrimaryImage;
            }
            set
            {
                mPrimaryImage = value;
            }
        }

        private ObservableCollection<ImageData> mSecondaryImages = null;
        public ObservableCollection<ImageData> SecondaryImages
        {
            get
            {
                return mSecondaryImages;
            }
            set
            {
                mSecondaryImages = value;
            }
        }

        public Command SubscriptionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsLoading = true;
                    var result = await InAppBillingService.Instances.MakePurchase();
                    IsLoading = false;
                    this.UpdateModel();
                });
            }
        }

        public Command CreateUpdateCommand
        {
            get
            {
                return new Command(CreateUpdateLove);
            }
        }

        public Command DeleteCommand
        {
            get
            {
                return new Command(DeleteLove);
            }
        }

        public Command ResetCommand
        {
            get
            {
                return new Command(() =>
                {
                    if(SelectedLovedOnes == null)
                    {
                        OnResetData(null);
                    }
                    else
                    {
                        SelectedLovedOnes = null;
                    }
                });
            }
        }

        public LovedOnesViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }        
        private void OnResetData(object obj)
        {
            this.FirstName = "";
            this.LastName = "";
            this.BirthDay = null;
            this.HairColor = null;
            this.EyeColor = null;
            this.HeightFeet = null;
            this.HeightInches = null;
            this.Weight = null;
            this.Detail = null;
            this.PrimaryImage = null;
            this.SecondaryImages = new ObservableCollection<ImageData>()
            {
                null, null, null, null
            };
            this.Image = "";
            this.Image2 = "";
            this.Image3 = "";
            this.Image4 = "";
            this.Image5 = "";
        }

        private List<ItemChoice> mHairColors = null;
        public List<ItemChoice> HairColors
        {
            get => mHairColors;
            set
            {
                mHairColors = value;
            }
        }

        private List<ItemChoice> mEyeColors = null;
        public List<ItemChoice> EyeColors
        {
            get => mEyeColors;
            set
            {
                mEyeColors = value;
            }
        }

        private List<ItemChoice> mHeightFeets = null;
        public List<ItemChoice> HeightFeets
        {
            get => mHeightFeets;
            set
            {
                mHeightFeets = value;
            }
        }

        private List<ItemChoice> mListHeightInches = null;
        public List<ItemChoice> ListHeightInches
        {
            get => mListHeightInches;
            set
            {
                mListHeightInches = value;
                RaisePropertyChanged(nameof(ListHeightInches));
            }
        }

        public AlertsViewModel AlertsVmFrom { get; set; }

        private async void GetChoices()
        {
            List<Choice> choices = Utils.Utils.GetChoices();
            if(choices.Count == 0)
            {
                var response = await AppApiManager.Instances.GetChoices();
                if (response.IsSuccess && response.ResponseObject != null && response.ResponseObject != null)
                {
                    choices = response.ResponseObject;
                    if (choices.Count > 0)
                    {
                        Utils.Utils.SaveChoices(choices);
                    }
                }
            }
            if(choices.Count == 0)
            {
                return;
            }
            Choice hairColorChoice = choices.Find((Choice obj) =>
            {
                return obj.Name == Utils.Constansts.HAIR_COLORS_SETTING_KEY;
            });
            if (hairColorChoice != null)
            {
                HairColors = hairColorChoice.Items;
            }

            Choice eyeColorChoice = choices.Find((Choice obj) =>
            {
                return obj.Name == Utils.Constansts.EYE_COLORS_SETTING_KEY;
            });
            if (eyeColorChoice != null)
            {
                EyeColors = eyeColorChoice.Items;
            }

            Choice heightFeetChoice = choices.Find((Choice obj) =>
            {
                return obj.Name == Utils.Constansts.HEIGHT_FEETS_SETTING_KEY;
            });
            if (heightFeetChoice != null)
            {
                HeightFeets = heightFeetChoice.Items;
            }

            Choice heightInchesChoice = choices.Find((Choice obj) =>
            {
                return obj.Name == Utils.Constansts.HEIGHT_LIST_INCHES_SETTING_KEY;
            });
            if (heightInchesChoice != null)
            {
                ListHeightInches = heightInchesChoice.Items;
            }
        }

        private bool isInitData = false;

        public override async Task Initialize()
        {
            GetChoices();
            await Task.CompletedTask;
        }

        public async void GetLoves()
        {
            var userSession = Utils.Utils.GetToken();
            if(IsVolunteer == false && userSession != null) 
            {
                IsLoading = true;
                var response = await AppApiManager.Instances.GetListLovedOnes(userSession.UserId);
                if (response.IsSuccess && response.ResponseObject != null)
                {
                    Loves = response.ResponseObject;
                }
                IsLoading = false;
            }
        }
        private async void CreateUpdateLove()
        {
            IsLoading = true;
            Love lovedOnesToUpdate = null;
            if (mSelectedLovedOnes != null)
            {
                //Update Flow
                lovedOnesToUpdate = mSelectedLovedOnes;
            }

            var lovedOnesToPost = new Love()
            {
                Id = lovedOnesToUpdate != null ? lovedOnesToUpdate.PrimaryId : null,
                UserId = Utils.Utils.GetUserId(),
                CreatedBy = lovedOnesToUpdate != null ? lovedOnesToUpdate.CreatedBy : User.Id,
                FirstName = FirstName,
                LastName = LastName,
                DateofBirth = GetStrDateOfBirth(),
                HairColorId = HairColor == null ? "" : HairColor.Id,
                EyeColorId = EyeColor == null ? "" : EyeColor.Id,
                HeightFeetId = HeightFeet == null ? "" : HeightFeet.Id,
                HeightInchesId = HeightInches == null ? "" : HeightInches.Id,
                Weight = Weight,
                OtherInformation = Detail,
                ModifiedOn = lovedOnesToUpdate != null ? DateTime.UtcNow.ToString() : "",
                Image = lovedOnesToUpdate != null ? lovedOnesToUpdate.Image : "",
                Image2 = lovedOnesToUpdate != null ? lovedOnesToUpdate.Image2 : "",
                Image3 = lovedOnesToUpdate != null ? lovedOnesToUpdate.Image3 : "",
                Image4 = lovedOnesToUpdate != null ? lovedOnesToUpdate.Image4 : "",
                Image5 = lovedOnesToUpdate != null ? lovedOnesToUpdate.Image5 : "",
                CreatedOn = lovedOnesToUpdate != null ? lovedOnesToUpdate.CreatedOn : DateTime.UtcNow.ToString(),
            };

            if (lovedOnesToUpdate != null)
            {
                lovedOnesToPost.ProfileId = lovedOnesToUpdate.ProfileId;
            }

            if (PrimaryImage != null)
            {
                var responseUploadImageOne = await AppApiManager.Instances.UploadImage(Utils.Constansts.LOVED_ONES_ENTITY_SETTING_KEY, PrimaryImage);
                if (responseUploadImageOne.IsSuccess)
                {
                    lovedOnesToPost.Image = responseUploadImageOne.ResponseObject.Result;
                }
            }
            if (SecondaryImages != null)
            {
                if (SecondaryImages[0] != null)
                {
                    var responseUploadImageTwo = await AppApiManager.Instances.UploadImage(Utils.Constansts.LOVED_ONES_ENTITY_SETTING_KEY, SecondaryImages[0]);
                    if (responseUploadImageTwo.IsSuccess)
                    {
                        lovedOnesToPost.Image2 = responseUploadImageTwo.ResponseObject.Result;
                    }
                }
                if (SecondaryImages[1] != null)
                {
                    var responseUploadImageThree = await AppApiManager.Instances.UploadImage(Utils.Constansts.LOVED_ONES_ENTITY_SETTING_KEY, SecondaryImages[1]);
                    if (responseUploadImageThree.IsSuccess)
                    {
                        lovedOnesToPost.Image3 = responseUploadImageThree.ResponseObject.Result;
                    }
                }
                if (SecondaryImages[2] != null)
                {
                    var responseUploadImageFour = await AppApiManager.Instances.UploadImage(Utils.Constansts.LOVED_ONES_ENTITY_SETTING_KEY, SecondaryImages[2]);
                    if (responseUploadImageFour.IsSuccess)
                    {
                        lovedOnesToPost.Image4 = responseUploadImageFour.ResponseObject.Result;
                    }
                }
                if (SecondaryImages[3] != null)
                {
                    var responseUploadImageFive = await AppApiManager.Instances.UploadImage(Utils.Constansts.LOVED_ONES_ENTITY_SETTING_KEY, SecondaryImages[3]);
                    if (responseUploadImageFive.IsSuccess)
                    {
                        lovedOnesToPost.Image5 = responseUploadImageFive.ResponseObject.Result;
                    }
                }
            }
            ApiResponse<AppServiceResponse<Entity<Love>>> response;
            if (lovedOnesToUpdate != null)
            {
                //Update flow
                response = await AppApiManager.Instances.UpdateLovedOnes(lovedOnesToPost);
            }
            else
            {
                //Create flow
                response = await AppApiManager.Instances.CreateLovedOnes(lovedOnesToPost);
            }

            IsLoading = false;
            if (response.IsSuccess && response.ResponseObject != null)
            {
                if (response.ResponseObject.IsError == false)
                {
                    IsUpdateSuccess = true;
                    if (AlertsVmFrom != null)
                    {   
                        AlertsVmFrom.CreatingLove = lovedOnesToPost;
                        IsLoading = false;
                        await App.Current.MainPage.DisplayAlert("Info", "Information updated successfully.", "OK");
                        AlertsVmFrom.CreateAlertCommand.Execute(null);                       
                    }
                    else
                    {
                        SelectedLovedOnes = null;
                        GetLoves();
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", response.ResponseObject.Message, "OK");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
            }
        }

        private string GetStrDateOfBirth()
        {
            if(BirthDay != null)
            {
                return BirthDay.Value.ToString(Utils.Constansts.BIRTHDAY_DATE_FORMAT);
            }
            return "";
        }

        private async void DeleteLove()
        {
            if (mSelectedLovedOnes != null)
            {
                IsLoading = true;
                var selectedLovedOnes = mSelectedLovedOnes;
                var response = await AppApiManager.Instances.DeleteLovedOnes(selectedLovedOnes);
                IsLoading = false;
                if (response.IsSuccess)
                {
                    IsUpdateSuccess = true;
                    if (AlertsVmFrom != null)
                    {
                        IsLoading = false;
                        
                        await App.Current.MainPage.DisplayAlert("Info", "Loved one record deleted successfully.", "OK");
                        AlertsVmFrom.CreateAlertCommand.Execute(null);
                    }
                    else
                    {
                        SelectedLovedOnes = null;
                        GetLoves();
                    }                    
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
                }
            }
        }

    }
}