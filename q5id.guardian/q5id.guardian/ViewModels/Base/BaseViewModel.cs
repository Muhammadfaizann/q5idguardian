using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Presenters.Hints;
using MvvmCross.ViewModels;
using q5id.guardian.Presenter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace q5id.guardian.ViewModels
{
    public class BaseViewModel : MvxNavigationViewModel
    {

        public BaseViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(logProvider, navigationService)
        {

        }

        protected void PopToPage<TViewModel>() where TViewModel : MvxViewModel
        {
            var hint = new MvxPopPresentationHint(typeof(TViewModel));
            this.NavigationService.ChangePresentation(hint);
        }

        protected async Task ClearStackAndNavigateToPage<TViewModel>() where TViewModel : MvxViewModel
        {
            var presentation = new MvxBundle(new Dictionary<string, string> { { PresentationConstantValue.CLEAR_STACK_AND_SHOW_PAGE, "" } });

            await this.NavigationService.Navigate<TViewModel>(presentationBundle: presentation);
        }

        public IMvxAsyncCommand CloseCommand => new MvxAsyncCommand(ClosePage);

        protected async Task ClosePage()
        {
            await this.NavigationService.Close(this);
        }

        private bool mIsBusy = false;

        public bool IsBusy
        {
            get => mIsBusy;
            set => SetProperty(ref mIsBusy, value);
        }

        private string mTitle = "";

        public string Title
        {
            get => mTitle;
            set => SetProperty(ref mTitle, value);
        }
    }
}
