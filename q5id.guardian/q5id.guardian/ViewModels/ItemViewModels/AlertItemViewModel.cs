using System;
using System.Diagnostics;
using q5id.guardian.Models;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels.ItemViewModels
{
    public class AlertItemViewModel : BaseItemViewModel<Alert>
    {
        
        private string mUpdatedTimeDescription = "";
        public string UpdatedTimeDescription
        {
            get => mUpdatedTimeDescription;
            set
            {
                mUpdatedTimeDescription = value;
                RaisePropertyChanged(nameof(UpdatedTimeDescription));
            }
        }

        public Action OnUpdateItemAction { get; set; }

        public AlertItemViewModel(Alert alert) : base(alert)
        {
           
        }
    }
}
