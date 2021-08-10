using System;

using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public class BaseLovedContentChildView : ContentView
    {
        public LovedOnesContentView MainContentView { get; set; }

        public string ViewTitle = "Loved Ones";

        public BaseLovedContentChildView(LovedOnesContentView mainContentView)
        {
            MainContentView = mainContentView;
        }
    }
}

