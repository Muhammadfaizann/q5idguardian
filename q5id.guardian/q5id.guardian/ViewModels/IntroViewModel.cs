using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using q5id.guardian.Services;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class IntroViewModel : BaseViewModel
    {

        public IntroViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            IntroPages = new ObservableCollection<Intro>()
            {
                new Intro("Keeping kids and loved ones safe", "Guardian is a secure alert system where parents and caregivers get help from nearby Guardian volunteers to find lost loved ones."),
                new Intro("Protect your entire family", "With a monthly subscription, add multiple dependents to your account. If a loved one goes missing, nearby Guardian volunteers are ready to search."),
                new Intro("Get a free volunteer account", "If you don’t need to track loved ones, use Guardian for free. Should a caregiversend an alert and you’re nearby, you can step in and help."),
                new Intro("Privacy is our business", "We use our unique verified ID process to guarantee identities of caregivers and volunteers. You can trust your Guardian search team and know that your data is safe.")
            };
            LoginCommand = new Command(OnLoginClicked);
        }
        public Command LoginCommand { get; }

        private ObservableCollection<Intro> mIntroPages;
        public ObservableCollection<Intro> IntroPages
        {
            get => mIntroPages;
            set => SetProperty(ref mIntroPages, value);
            
        }

        private async void OnLoginClicked(object obj)
        {
            var user = await GetProfile();
            if (user != null)
            {
                await ClearStackAndNavigateToPage<HomeViewModel, User>(user);
            }
            else
            {
                await NavigationService.Navigate<LoginViewModel>();
            }
            
        }

        private async Task<User> GetProfile()
        {
            UserSession userSession = Utils.Utils.GetToken();
            if (userSession != null)
            {
                var currentUserResponse = await AppApiManager.Instances.GetUserProfile(userSession.UserId);
                if (currentUserResponse.IsSuccess && currentUserResponse.ResponseObject != null && currentUserResponse.ResponseObject.Count > 0)
                {
                    var result = currentUserResponse.ResponseObject[0];
                    return result;
                }
            }
            return null;
        }
    }
}
