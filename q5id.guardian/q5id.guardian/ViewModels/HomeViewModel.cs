

using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public HomeViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }
        public Command OpenAlertCommand { get; }

        public Command OpenSettingCommand { get; }


        //private async void OnLoginClicked(object obj)
        //{
        //    await NavigationService.NavigateToAsync(typeof(AlertsViewModel));
        //}

        //private async void OnSettingClicked(object obj)
        //{
        //    await NavigationService.NavigateToAsync(typeof(SettingViewModel));
        //}
    }
}