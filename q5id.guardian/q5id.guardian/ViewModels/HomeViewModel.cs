

using System.Windows.Input;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Command OpenAlertCommand { get; }

        public HomeViewModel()
        {
            Title = "Home";
            OpenAlertCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            await NavigationService.NavigateToAsync(typeof(AlertsViewModel));
        }
    }
}