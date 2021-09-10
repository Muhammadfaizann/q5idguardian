using System;
using Plugin.InAppBilling;

namespace q5id.guardian.ViewModels.ItemViewModels
{
    public class InAppBillingProductItemViewModel : BaseItemViewModel<InAppBillingProduct>
    {
        public bool IsPaid { get; set; }

        public InAppBillingProductItemViewModel(InAppBillingProduct model) : base(model)
        {

        }
    }
}

