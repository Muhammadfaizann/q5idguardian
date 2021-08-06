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
            LoginCommand = new Command(OnLoginAsVolClicked);
            SignUpCommand = new Command(OnLoginAsSubClicked);
        }

        private void OnLoginClicked(object obj)
        {
            NavigationService.SetRoot(typeof(HomeViewModel), obj, NavigationType.ContentPageWithNavigation);
        }

        private void OnLoginAsSubClicked(object obj)
        {
            this.OnLoginClicked(new User()
            {
                FirstName = "Minh",
                LastName = "Luu",
                Role = UserRole.Subscriber
            });
        }

        private void OnLoginAsVolClicked(object obj)
        {
            this.OnLoginClicked(new User()
            {
                FirstName = "Minh",
                LastName = "Luu",
                Role = UserRole.Volunteer
            });
        }

        private void OnSignUpClicked(object obj)
        {
            NavigationService.NavigateToAsync(typeof(AuthenFaceViewModel), obj);
        }
    }
}
