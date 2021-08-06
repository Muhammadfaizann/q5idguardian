

using System.Windows.Input;
using q5id.guardian.Models;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeContentViewModel HomeContentViewModel { get; set; }

        public Command OpenAlertCommand { get; }

        public Command OpenSettingCommand { get; }

        public HomeViewModel()
        {
            Title = "Home";
            OpenAlertCommand = new Command(OnLoginClicked);
            OpenSettingCommand = new Command(OnSettingClicked);
        }

        public override void Initialize(object parameter)
        {
            if(parameter is User user)
            {
                HomeContentViewModel = new HomeContentViewModel()
                {
                    User = user,
                };
            }
        }

        private async void OnLoginClicked(object obj)
        {
            await NavigationService.NavigateToAsync(typeof(AlertsViewModel));
        }

        private async void OnSettingClicked(object obj)
        {
            await NavigationService.NavigateToAsync(typeof(SettingViewModel));
        }
    }
}