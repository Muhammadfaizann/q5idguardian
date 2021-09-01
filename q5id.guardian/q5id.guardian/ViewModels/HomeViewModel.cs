

using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using q5id.guardian.Models;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class HomeViewModel : BaseViewModel<User>
    {
        public HomeViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            OpenSettingCommand = new MvxAsyncCommand(OnSettingClicked);
            HomeVm = new HomeContentViewModel(navigationService, logProvider);
            LovedOnesVm = new LovedOnesViewModel(navigationService, logProvider);
            AlertsVm = new AlertsViewModel(navigationService, logProvider);
            Task.Run(async () =>
            {
               await HomeVm.Initialize();
            });
        }

        public HomeContentViewModel HomeVm { get; private set; }
        public LovedOnesViewModel LovedOnesVm { get; private set; }
        public AlertsViewModel AlertsVm { get; private set; }

        public Command OpenAlertCommand { get; }

        public MvxAsyncCommand OpenSettingCommand { get; }

        public override void Prepare(User parameter)
        {
            HomeVm.User = parameter;
            LovedOnesVm.User = parameter;
            AlertsVm.User = parameter;
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
        }

        private async Task OnSettingClicked()
        {
            await NavigationService.Navigate<SettingViewModel>();
        }

        

        public override void ViewAppeared()
        {
            base.ViewAppeared();
        }

        public Command OpenHomeTapCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await HomeVm.Initialize();
                });
            }
        }

        public Command OpenLovedOnesTapCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await LovedOnesVm.Initialize();
                });
            }
        }

        public Command OpenAlertTapCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await AlertsVm.Initialize();
                });
            }
        }
    }
}