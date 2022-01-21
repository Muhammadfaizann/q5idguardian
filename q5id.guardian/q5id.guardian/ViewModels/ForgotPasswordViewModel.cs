using System;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using q5id.guardian.Services;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        private string mEmail = "";
        public string Email
        {
            get => mEmail;
            set
            {
                mEmail = value;
            }
        }

        private bool mIsRequestSuccess = false;
        public bool IsRequestSuccess
        {
            get
            {
                return mIsRequestSuccess;
            }
            set
            {
                mIsRequestSuccess = value;
            }
        }

        public bool IsValidEmail
        {
            get
            {
                if (Email == "" || Email == null)
                {
                    return false;
                }

                return Utils.Utils.IsValidEmail(Email);
            }
        }

        public ForgotPasswordViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }

        public Command ForgotPasswordCommand
        {
            get
            {
                return new Command(OnForgotPassword);
            }
        }

        private async void OnForgotPassword()
        {
            IsLoading = true;
            var currentUserResponse = await AppApiManager.Instances.ForgotPassword(mEmail);
            IsLoading = false;
            //if (currentUserResponse.IsSuccess)
            //{
            //    IsRequestSuccess = true;
            //}
            //else
            //{
            //    await App.Current.MainPage.DisplayAlert("Error", "Request Failed", "OK");
            //}
            IsRequestSuccess = true;
        }
    }
}
