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

        public LoginViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            LoginCommand = new MvxAsyncCommand(OnLoginClickedAsync);
            SignUpCommand = new MvxAsyncCommand(OnSignUpClicked);
        }
        public IMvxAsyncCommand LoginCommand { get; }

        public IMvxAsyncCommand SignUpCommand { get; }

       

        private async Task OnLoginClickedAsync()
        {
            await ClearStackAndNavigateToPage<HomeViewModel>();
        }

        private async Task OnSignUpClicked()
        {
            await NavigationService.Navigate<AuthenFaceViewModel>();
        }
    }
}
