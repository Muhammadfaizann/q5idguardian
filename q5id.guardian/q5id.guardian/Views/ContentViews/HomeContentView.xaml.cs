using System;
using System.Collections.Generic;
using q5id.guardian.Controls;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews
{
    public partial class HomeContentView : ContentView
    {
        public HomeContentView()
        {
            InitializeComponent();
        }

        void ToggleView_Changed(System.Object sender, System.EventArgs e)
        {
            if(sender is ToggleView toggle)
            {
                if (toggle.IsActive)
                {
                    gridMap.IsVisible = true;
                    gridContent.IsVisible = false;
                }
                else
                {
                    gridMap.IsVisible = false;
                    gridContent.IsVisible = true;
                }
            }
        }
    }
}
