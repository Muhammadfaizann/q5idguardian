using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using q5id.guardian.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public Command SignUpCommand { get; }

        public LoginViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            LoginCommand = new Command(OnLoginAsVolClicked);
            SignUpCommand = new Command(OnLoginAsSubClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            await ClearStackAndNavigateToPage<HomeViewModel>();
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

        private async Task OnSignUpClicked()
        {
            await NavigationService.Navigate<AuthenFaceViewModel>();
        }
    }
}
