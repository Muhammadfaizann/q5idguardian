using q5id.guardian.Models;
using q5id.guardian.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public Command SignUpCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            SignUpCommand = new Command(OnSignUpClicked);
        }

        private void OnLoginClicked(object obj)
        {
            NavigationService.SetRoot(typeof(HomeViewModel), null, NavigationType.ContentPageWithNavigation);
        }

        private void OnSignUpClicked(object obj)
        {
            NavigationService.NavigateToAsync(typeof(AuthenFaceViewModel), obj);
        }
    }
}
