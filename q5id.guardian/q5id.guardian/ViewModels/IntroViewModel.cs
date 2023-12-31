﻿using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using q5id.guardian.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public interface IntroView
    {
        void ShowPidInstruction();
        Task ShowAlert(string message);
    }
    public class IntroViewModel : BaseViewModel
    {

        public IntroViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            IntroPages = new ObservableCollection<Intro>()
            {
                new Intro("Keeping kids and loved ones safe", "Guardian is a secure alert system where parents and caregivers get help from nearby Guardian volunteers to find lost loved ones."),
                new Intro("Protect your entire family", "With a monthly subscription, add multiple dependents to your account. If a loved one goes missing, nearby Guardian volunteers are ready to search."),
                new Intro("Get a free volunteer account", "If you don’t need to track loved ones, use Guardian for free. Should a caregiversend an alert and you’re nearby, you can step in and help."),
                new Intro("Privacy is our business", "We use our unique verified ID process to guarantee identities of caregivers and volunteers.")
            };
            LoginCommand = new Command(OnLoginClicked);
            OpenPIDCommand = new Command(OpenPIDClicked);
            JoinPidCommand = new Command(JoinPidClicked);
            CopyPushTokenCommand = new Command(CopyPushToken);
        }
        public IntroView View { get; set; }

        public Command LoginCommand { get; }

        public Command JoinPidCommand { get; }

        public Command OpenPIDCommand { get; }

        public Command CopyPushTokenCommand { get; }

        private ObservableCollection<Intro> mIntroPages;
        public ObservableCollection<Intro> IntroPages
        {
            get => mIntroPages;
            set => SetProperty(ref mIntroPages, value);
            
        }

        private async void OnLoginClicked(object obj)
        {
            
            await NavigationService.Navigate<LoginViewModel>();
        }

        private async void OpenPIDClicked(object obj)
        {

            try
            {
                // TODO: identify and implement best way to make these urls configuration driven, and whether or not to use webview or browser
                if (DeviceInfo.Platform == DevicePlatform.iOS) 
                    await Browser.OpenAsync("https://apps.apple.com/us/app/proven-identity/id1481905171", BrowserLaunchMode.External);
                else 
                    await Browser.OpenAsync("https://play.google.com/store/apps/details?id=com.q5id.provenidentity&hl=en_US&gl=US", BrowserLaunchMode.External);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }

        private async void JoinPidClicked(object obj)
        {

            View?.ShowPidInstruction();
        }
        
        private int _tapCount = 0;
        private async void CopyPushToken(object obj)
        {
            _tapCount++;
            if(_tapCount % 5 == 0)
            {
                await Clipboard.SetTextAsync(Utils.Utils.GetPushNotificationToken());
                View?.ShowAlert("Push token is coppied.");
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
