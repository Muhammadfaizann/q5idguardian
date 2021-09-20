using System;
using q5id.guardian.Views.Base;
using q5id.guardian.Views.ContentViews;
using Xamarin.Forms;

namespace q5id.guardian.Selectors
{
    public class LovedOnesPageSelector : DataTemplateSelector
    {
        public DataTemplate ListTemplate { get; set; }
        public DataTemplate IntroTemplate { get; set; }
        public DataTemplate ProfileInfoTemplate { get; set; }
        public DataTemplate PhysicalInfoTemplate { get; set; }
        public DataTemplate ImageInfoTemplate { get; set; }
        public DataTemplate DetailInfoTemplate { get; set; }
        public DataTemplate ReviewTemplate { get; set; }
        public DataTemplate EditTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is int intValue)
            {
                switch (intValue)
                {
                    //case LovedOnesContentView.VIEW_LIST_INDEX:
                    //    return ListTemplate;
                    //case LovedOnesContentView.VIEW_INTRO_INDEX:
                    //    return IntroTemplate;
                    //case LovedOnesContentView.VIEW_PROFILE_INFO_INDEX:
                    //    return ProfileInfoTemplate;
                    //case LovedOnesContentView.VIEW_PHYSIC_INFO_INDEX:
                    //    return PhysicalInfoTemplate;
                    //case LovedOnesContentView.VIEW_IMAGE_INFO_INDEX:
                    //    return ImageInfoTemplate;
                    //case LovedOnesContentView.VIEW_DETAIL_INFO_INDEX:
                    //    return DetailInfoTemplate;
                    //case LovedOnesContentView.VIEW_REVIEW_INDEX:
                    //    return ReviewTemplate;
                    //case LovedOnesContentView.VIEW_EDIT_INDEX:
                    //    return EditTemplate;
                    default:
                        return ListTemplate;
                }
            }
            return ListTemplate;
        }
    }
}
