using System;
using q5id.guardian.Views.ContentViews;
using Xamarin.Forms;

namespace q5id.guardian.Selectors
{
    public class AlertPageSelector : DataTemplateSelector
    {
        public DataTemplate ListTemplate { get; set; }
        public DataTemplate DetailTemplate { get; set; }
        public DataTemplate CreateAlertChooseLoveTemplate { get; set; }
        public DataTemplate CreateAlertDetailTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is int intValue)
            {
                switch (intValue)
                {
                    //case AlertContentView.VIEW_LIST_INDEX:
                    //    return ListTemplate;
                    //case AlertContentView.VIEW_DETAIL_INDEX:
                    //    return DetailTemplate;
                    //case AlertContentView.VIEW_CREATE_ALERT_CHOOSE_LOVE_INDEX:
                    //    return CreateAlertChooseLoveTemplate;
                    //case AlertContentView.VIEW_CREATE_ALERT_DETAIL_INDEX:
                    //    return CreateAlertDetailTemplate;
                    default:
                        return ListTemplate;
                }
            }
            return ListTemplate;
        }
    }
}

