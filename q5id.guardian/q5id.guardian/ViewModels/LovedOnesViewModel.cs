using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using q5id.guardian.Services;
using q5id.guardian.Services.Bases;
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
    public class LovedOnesViewModel : BaseViewModel
    {
        private Love mSelectedLovedOnes = null;
        public Love SelectedLovedOnes
        {
            get => mSelectedLovedOnes;
            set
            {
                mSelectedLovedOnes = value;
                RaisePropertyChanged(nameof(SelectedLovedOnes));
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
                HairColor = HairColors.Find((ItemChoice choice) =>
                {
                    return choice.Name == selectedLovedOnes.HairColor;
                });
                EyeColor = EyeColors.Find((ItemChoice choice) =>
                {
                    return choice.Name == selectedLovedOnes.EyeColor;
                });
                HeightFeet = HeightFeets.Find((ItemChoice choice) =>
                {
                    return choice.Name == selectedLovedOnes.HeightFeet;
                });
                HeightInches = ListHeightInches.Find((ItemChoice choice) =>
                {
                    return choice.Name == selectedLovedOnes.HeightInches;
                });
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
                RaisePropertyChanged(nameof(IsUpdateSuccess));
            }
        }

        private User mUser = null;
        public User User
        {
            get => mUser;
            set
            {
                mUser = value;
                RaisePropertyChanged(nameof(User));
                RaisePropertyChanged(nameof(IsVolunteer));
            }
        }

        public Boolean IsVolunteer
        {
            get
            {
                return this.mUser != null && this.mUser.Role == UserRole.Volunteer;
            }
        }

        private List<Love> mLoves = null;
        public List<Love> Loves
        {
            get => mLoves;
            set
            {
                mLoves = value;
                RaisePropertyChanged(nameof(Loves));
                RaisePropertyChanged(nameof(IsHasNoLove));
            }
        }

        public Boolean IsHasNoLove
        {
            get
            {
                return mLoves == null || mLoves.Count == 0;
            }
        }

        private string mFirstName = null;
        public string FirstName
        {
            get => mFirstName;
            set
            {
                mFirstName = value;
                RaisePropertyChanged(nameof(FirstName));
                RaisePropertyChanged(nameof(FullName));
            }
        }

        private string mLastName = null;
        public string LastName
        {
            get => mLastName;
            set
            {
                mLastName = value;
                RaisePropertyChanged(nameof(LastName));
                RaisePropertyChanged(nameof(FullName));
            }
        }

        public string FullName
        {
            get
            {
                return mFirstName + " " + mLastName;
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
                RaisePropertyChanged(nameof(Image));
            }
        }

        private string mImage2 = null;
        public string Image2
        {
            get => mImage2;
            set
            {
                mImage2 = value;
                RaisePropertyChanged(nameof(Image2));
            }
        }

        private string mImage3 = null;
        public string Image3
        {
            get => mImage3;
            set
            {
                mImage3 = value;
                RaisePropertyChanged(nameof(Image3));
            }
        }

        private string mImage4 = null;
        public string Image4
        {
            get => mImage4;
            set
            {
                mImage4 = value;
                RaisePropertyChanged(nameof(Image4));
            }
        }

        private string mImage5 = null;
        public string Image5
        {
            get => mImage5;
            set
            {
                mImage5 = value;
                RaisePropertyChanged(nameof(Image5));
            }
        }

        private DateTime? mBirthDay = null;
        public DateTime? BirthDay
        {
            get => mBirthDay;
            set
            {
                mBirthDay = value;
                RaisePropertyChanged(nameof(BirthDay));
            }
        }

        private ItemChoice mHeightFeet = null;
        public ItemChoice HeightFeet
        {
            get => mHeightFeet;
            set
            {
                mHeightFeet = value;
                RaisePropertyChanged(nameof(HeightFeet));
                RaisePropertyChanged(nameof(Height));
            }
        }

        private ItemChoice mHeightInches = null;
        public ItemChoice HeightInches
        {
            get => mHeightInches;
            set
            {
                mHeightInches = value;
                RaisePropertyChanged(nameof(HeightInches));
                RaisePropertyChanged(nameof(Height));
            }
        }

        public string Height
        {
            get
            {
                if(mHeightFeet == null || mHeightInches == null)
                {
                    return "0";
                }
                return mHeightFeet.Name + "’ " + mHeightInches.Name + "”";
            }
        }

        private string mWeight = null;
        public string Weight
        {
            get => mWeight;
            set
            {
                mWeight = value;
                RaisePropertyChanged(nameof(Weight));
            }
        }

        private string mDetail = null;
        public string Detail
        {
            get => mDetail;
            set
            {
                mDetail = value;
                RaisePropertyChanged(nameof(Detail));
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
                RaisePropertyChanged(nameof(HairColor));
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
                RaisePropertyChanged(nameof(EyeColor));
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
                RaisePropertyChanged(nameof(PrimaryImage));
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
                RaisePropertyChanged(nameof(SecondaryImages));
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
                RaisePropertyChanged(nameof(HairColors));
            }
        }

        private List<ItemChoice> mEyeColors = null;
        public List<ItemChoice> EyeColors
        {
            get => mEyeColors;
            set
            {
                mEyeColors = value;
                RaisePropertyChanged(nameof(EyeColors));
            }
        }

        private List<ItemChoice> mHeightFeets = null;
        public List<ItemChoice> HeightFeets
        {
            get => mHeightFeets;
            set
            {
                mHeightFeets = value;
                RaisePropertyChanged(nameof(HeightFeets));
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

        private async void GetChoices()
        {
            var response = await AppService.Instances.GetChoices();
            if (response.IsSuccess)
            {
                List<Choice> choices = response.ResponseObject;
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
        }

        private StructureEntity LovedOnesEntity = null;
        private bool isInitData = false;

        public override async Task Initialize()
        {
            GetLovedOnesEntity();
            GetLoves();
            GetChoices();
            await Task.CompletedTask;
        }

        private void GetLovedOnesEntity()
        {
            var settings = Utils.Utils.GetSettings();
            if (settings != null)
            {
                LovedOnesEntity = Utils.Utils.GetSettings().Find((StructureEntity entity) =>
                {
                    return entity.EntityName == Utils.Constansts.LOVED_ONES_ENTITY_SETTING_KEY;
                });
            }
        }

        private async void GetLoves()
        {
            IsLoading = true;
            if (LovedOnesEntity != null)
            {
                var response = await AppService.Instances.GetListLovedOnes(LovedOnesEntity.Id);
                if (response.IsSuccess)
                {
                    Loves = response.ResponseObject.Select((Entity<Love> entityLove) =>
                    {
                        var love = entityLove.Data;
                        love.Id = entityLove.Id;
                        return love;
                    }).ToList();
                }
            }
            IsLoading = false;
        }

        private async void CreateUpdateLove()
        {
            if (LovedOnesEntity != null)
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
                    Id = lovedOnesToUpdate != null ? lovedOnesToUpdate.Id : "",
                    CreatedBy = lovedOnesToUpdate != null ? lovedOnesToUpdate.CreatedBy : User.Id,
                    FirstName = FirstName,
                    LastName = LastName,
                    DateofBirth = GetStrDateOfBirth(),
                    HairColor = HairColor == null ? "" : HairColor.Name,
                    EyeColor = EyeColor == null ? "" : EyeColor.Name,
                    HeightFeet = HeightFeet == null ? "" : HeightFeet.Name,
                    HeightInches = HeightInches == null ? "" : HeightInches.Name,
                    Weight = Weight,
                    OtherInformation = Detail,
                    ModifiedOn = lovedOnesToUpdate != null ? DateTime.UtcNow.ToString() : "",
                    Image = lovedOnesToUpdate != null ? lovedOnesToUpdate.Image : "",
                    Image2 = lovedOnesToUpdate != null ? lovedOnesToUpdate.Image2 : "",
                    Image3 = lovedOnesToUpdate != null ? lovedOnesToUpdate.Image3 : "",
                    Image4 = lovedOnesToUpdate != null ? lovedOnesToUpdate.Image4 : "",
                    Image5 = lovedOnesToUpdate != null ? lovedOnesToUpdate.Image5 : "",
                    ProfileId = lovedOnesToUpdate != null ? lovedOnesToUpdate.ProfileId : System.Guid.NewGuid().ToString(),
                    CreatedOn = lovedOnesToUpdate != null ? lovedOnesToUpdate.CreatedOn : DateTime.UtcNow.ToString(),
                };
                if(PrimaryImage != null)
                {
                    var responseUploadImageOne = await AppService.Instances.UploadImage(LovedOnesEntity.Id, PrimaryImage);
                    if (responseUploadImageOne.IsSuccess)
                    {
                        lovedOnesToPost.Image = responseUploadImageOne.ResponseObject.Path;
                    }
                }
                if(SecondaryImages != null)
                {
                    if (SecondaryImages[0] != null)
                    {
                        var responseUploadImageTwo = await AppService.Instances.UploadImage(LovedOnesEntity.Id, SecondaryImages[0]);
                        if (responseUploadImageTwo.IsSuccess)
                        {
                            lovedOnesToPost.Image2 = responseUploadImageTwo.ResponseObject.Path;
                        }
                    }
                    if (SecondaryImages[1] != null)
                    {
                        var responseUploadImageThree = await AppService.Instances.UploadImage(LovedOnesEntity.Id, SecondaryImages[1]);
                        if (responseUploadImageThree.IsSuccess)
                        {
                            lovedOnesToPost.Image3 = responseUploadImageThree.ResponseObject.Path;
                        }
                    }
                    if (SecondaryImages[2] != null)
                    {
                        var responseUploadImageFour = await AppService.Instances.UploadImage(LovedOnesEntity.Id, SecondaryImages[2]);
                        if (responseUploadImageFour.IsSuccess)
                        {
                            lovedOnesToPost.Image4 = responseUploadImageFour.ResponseObject.Path;
                        }
                    }
                    if (SecondaryImages[3] != null)
                    {
                        var responseUploadImageFive = await AppService.Instances.UploadImage(LovedOnesEntity.Id, SecondaryImages[3]);
                        if (responseUploadImageFive.IsSuccess)
                        {
                            lovedOnesToPost.Image5 = responseUploadImageFive.ResponseObject.Path;
                        }
                    }
                }
                ApiResponse<EntityResponse<Love>> response;
                if(lovedOnesToUpdate != null)
                {
                    //Update flow
                    response = await AppService.Instances.UpdateLovedOnes(LovedOnesEntity.Id, lovedOnesToPost);
                }
                else
                {
                    //Create flow
                    response = await AppService.Instances.CreateLovedOnes(LovedOnesEntity.Id, lovedOnesToPost);
                }
                
                IsLoading = false;
                if (response.IsSuccess && response.ResponseObject != null)
                {
                    if (response.ResponseObject.IsSuccessful)
                    {
                        IsUpdateSuccess = true;
                        SelectedLovedOnes = null;
                        GetLoves();
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
                var response = await AppService.Instances.DeleteLovedOnes(selectedLovedOnes.Id);
                IsLoading = false;
                if (response.IsSuccess)
                {
                    IsUpdateSuccess = true;
                    GetLoves();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", response.Message, "OK");
                }
            }
        }

    }
}