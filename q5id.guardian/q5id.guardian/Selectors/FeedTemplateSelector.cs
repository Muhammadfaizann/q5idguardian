using System;
using q5id.guardian.ViewModels.ItemViewModels;
using Xamarin.Forms;

namespace q5id.guardian.Selectors
{
    public class FeedTemplateSelector : DataTemplateSelector
    {
        public DataTemplate VolunteerItemTemplate { get; set; }
        public DataTemplate ParentItemTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if(item is FeedItemViewModel feedItemViewModel)
            {
                if (feedItemViewModel.IsParent)
                {
                    return ParentItemTemplate;
                }
            }
            return VolunteerItemTemplate;
        }
    }
}

