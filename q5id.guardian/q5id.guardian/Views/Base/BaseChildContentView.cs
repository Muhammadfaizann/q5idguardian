using System;
using Xamarin.Forms;

namespace q5id.guardian.Views.Base
{
    public class BaseChildContentView : ContentView
    {
        public string ViewTitle = "";

        public BaseContainerView MainContentView { get; set; }

        public BaseChildContentView(BaseContainerView MainContentView)
        {
            this.MainContentView = MainContentView;
        }
    }
}
