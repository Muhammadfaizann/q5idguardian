using System;
using q5id.guardian.ViewModels.ItemViewModels;
using Xamarin.Forms;

namespace q5id.guardian.Selectors
{
    public class AlertTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HeaderGroupTemplate { get; set; }
        public DataTemplate ItemTemplate { get; set; }
        public DataTemplate ItemExpandedTemplate { get; set; }
        public DataTemplate ItemAmberAlertTemplate { get; set; }
        public DataTemplate ItemAmberAlertExpandedTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is GroupHeaderItemViewModel)
            {
                return HeaderGroupTemplate;
            }
            if(item is AlertItemViewModel alertItemViewModel)
            {
                if (alertItemViewModel.IsAmberAlert && alertItemViewModel.IsExpanded)
                {
                    return ItemAmberAlertExpandedTemplate;
                }
                else if (alertItemViewModel.IsExpanded)
                {
                    return ItemExpandedTemplate;
                }
                else if (alertItemViewModel.IsAmberAlert)
                {
                    return ItemAmberAlertTemplate;
                }
            }
            return ItemTemplate;
        }
    }
}
