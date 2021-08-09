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

        public LovedOnesViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
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