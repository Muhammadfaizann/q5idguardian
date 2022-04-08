using System;
using System.Windows.Input;
using q5id.guardian.Models;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels.ItemViewModels
{
    public class AmberAlertItemViewModel: BaseItemViewModel<AmberAlert>
    {
        public Action<AmberAlertItemViewModel> OnUpdateExpanded;

        public Action OnOpenAlert;

        private Boolean mIsExpanded = false;
        public Boolean IsExpanded
        {
            get => mIsExpanded;
            set
            {
                mIsExpanded = value;
            }
        }

        private Boolean mIsAmberAlert = false;
        public Boolean IsAmberAlert
        {
            get => mIsAmberAlert;
            set
            {
                mIsAmberAlert = value;
            }
        }

        public ICommand ToggleExpandedCommand
        {
            get
            {
                return new Command(() => {
                    IsExpanded = !IsExpanded;
                    if (OnUpdateExpanded != null)
                    {
                        OnUpdateExpanded(this);
                    }
                });
            }
        }

        public ICommand OpenAlertCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (OnOpenAlert != null)
                    {
                        OnOpenAlert.Invoke();
                    }
                });
            }
        }

        public Action OnUpdateItemAction { get; set; }

        public AmberAlertItemViewModel(Alert alert) : base(alert)
        {

        }
    }
}
