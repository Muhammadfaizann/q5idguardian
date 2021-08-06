using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using System;
namespace q5id.guardian.ViewModels
{
    public class AuthenFaceViewModel : BaseViewModel
    {
        public AuthenFaceViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }
    }
}
