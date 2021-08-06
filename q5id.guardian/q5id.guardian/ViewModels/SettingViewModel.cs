using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using System;
namespace q5id.guardian.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        public SettingViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }
    }
}
