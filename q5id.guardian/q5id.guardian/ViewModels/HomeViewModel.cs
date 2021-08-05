

using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using q5id.guardian.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        
        public HomeViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            OpenSettingCommand = new MvxAsyncCommand(OnSettingClicked);
            HomeVm = new HomeContentViewModel(navigationService, logProvider);
            LovedOnesVm = new LovedOnesViewModel(navigationService, logProvider);
            AlertsVm = new AlertsViewModel(navigationService, logProvider);
        }

        public HomeContentViewModel HomeVm { get; private set; }
        public LovedOnesViewModel LovedOnesVm { get; private set; }
        public AlertsViewModel AlertsVm { get; private set; }

        public Command OpenAlertCommand { get; }

        public MvxAsyncCommand OpenSettingCommand { get; }


        //private async void OnLoginClicked(object obj)
        //{
        //    await NavigationService.NavigateToAsync(typeof(AlertsViewModel));
        //}

        private async Task OnSettingClicked()
        {
            await NavigationService.Navigate<SettingViewModel>();
        }

        

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            _ = Task.Run(async () =>
            {
                IsBusy = false;
                await Task.Delay(2000);
                await HomeVm.Initialize();
                IsBusy = true;
            });
        }
        
    }
}