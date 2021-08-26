using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using q5id.guardian.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class LovedOnesViewModel : BaseViewModel
    {
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

        private string mHeightFeet = null;
        public string HeightFeet
        {
            get => mHeightFeet;
            set
            {
                mHeightFeet = value;
                RaisePropertyChanged(nameof(HeightFeet));
                RaisePropertyChanged(nameof(Height));
            }
        }

        private string mHeightInches = null;
        public string HeightInches
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
                return mHeightFeet + "’ " + mHeightInches + "”";
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

        private byte[] mPrimaryImage = null;
        public byte[] PrimaryImage
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

        private ObservableCollection<byte[]> mSecondaryImages = null;
        public ObservableCollection<byte[]> SecondaryImages
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

        private Command mResetCommand;
        public Command ResetCommand
        {
            get => mResetCommand;
            set
            {
                mResetCommand = value;
                RaisePropertyChanged(nameof(ResetCommand));
            }
        }

        public LovedOnesViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            ResetCommand = new Command(OnResetData);
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

        private async Task GetChoices()
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
            }
        }

        public override async Task Initialize()
        {
            await GetLoves();
            await GetChoices();
        }

        private async Task GetLoves()
        {
            Loves = new List<Love>()
            {
                new Love()
                {
                    FirstName = "Amber",
                    LastName = "Jones",
                    ImageUrl = "https://images.statusfacebook.com/profile_pictures/beautiful-children-photos/beautiful-children-dp-profile-pictures-for-whatsapp-facebook-01.jpg",
                    UpdatedTime = null,
                    AddedTime = new DateTime(2020, 1, 1)
                },
                new Love()
                {
                    FirstName = "Sarah",
                    LastName = "Jones",
                    ImageUrl = "https://images.statusfacebook.com/profile_pictures/beautiful-children-photos/beautiful-children-dp-profile-pictures-for-whatsapp-facebook-05.jpg",
                    UpdatedTime = new DateTime(2021, 4, 1),
                    AddedTime = new DateTime(2020, 1, 1)
                },
                new Love()
                {
                    FirstName = "Theo",
                    LastName = "Jones",
                    ImageUrl = "https://images.statusfacebook.com/profile_pictures/beautiful-children-photos/beautiful-children-dp-profile-pictures-for-whatsapp-facebook-06.jpg",
                    UpdatedTime = new DateTime(2021, 4, 1),
                    AddedTime = new DateTime(2020, 1, 1),
                    IsLongTime = true,
                }
            };
            await Task.CompletedTask;
        }
    }
}