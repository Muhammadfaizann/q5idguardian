using System;
using Microsoft.Extensions.Logging;
using MvvmCross;
using MvvmCross.Navigation;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels.ItemViewModels
{
    public class BaseItemViewModel<T> : BaseViewModel where T : class
     {
        public Command ItemClickCommand { get; set; }

        public T Model { get; set; }

        private static IMvxNavigationService GetNavigationService()
        {
            return Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        }

        private static ILoggerFactory GetLoggerService()
        {
            return Mvx.IoCProvider.Resolve<ILoggerFactory>();
        }

        private BaseItemViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }

        public BaseItemViewModel(T mModel) : this(GetNavigationService(), GetLoggerService())
        {
            this.Model = mModel;
        }
    }
}
