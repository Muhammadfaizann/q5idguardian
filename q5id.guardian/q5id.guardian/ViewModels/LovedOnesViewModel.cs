using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class LovedOnesViewModel : BaseViewModel
    {
        public LovedOnesViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }
    }
}