using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using System;
using System.Collections.Generic;
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

        private ColorData mHairColor = null;
        public ColorData HairColor
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

        private ColorData mEyeColor = null;
        public ColorData EyeColor
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

        public override async Task Initialize()
        {
            await GetLoves();
        }

        private async Task GetLoves()
        {
            await Task.Delay(3000);
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
        }
    }
}