
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;

namespace q5id.guardian.ViewModels
{
    public class AlertsViewModel : BaseViewModel
    {
        public AlertsViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }
    }
}
