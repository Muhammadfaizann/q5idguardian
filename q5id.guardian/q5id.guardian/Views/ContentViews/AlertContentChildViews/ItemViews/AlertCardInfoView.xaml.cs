using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.AlertContentChildViews.ItemViews
{
    public partial class AlertCardInfoView : ContentView
    {
        public AlertCardInfoView()
        {
            InitializeComponent();
        }

        void OnExpandedInfoToggle(System.Object sender, System.EventArgs e)
        {
            StackDetail.IsVisible = !StackDetail.IsVisible;
            ImageSourceInfoExpand.Glyph = StackDetail.IsVisible ? Utils.FontAwesomeIcons.ChevronUp : Utils.FontAwesomeIcons.ChevronDown;
        }
    }
}
