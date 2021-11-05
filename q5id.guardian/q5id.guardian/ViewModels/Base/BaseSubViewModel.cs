using System;
using MvvmCross.Navigation;
using Microsoft.Extensions.Logging;

namespace q5id.guardian.ViewModels.Base
{
    public class BaseSubViewModel : BaseViewModel
    {
        public event EventHandler OnUpdateModel;

        public BaseSubViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }

        public void UpdateModel()
        {
            OnUpdateModel?.Invoke(this, null);
        }
    }
}
