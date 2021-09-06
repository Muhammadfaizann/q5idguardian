using System;
using System.Collections.Generic;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.AlertContentChildViews
{
    public partial class CreateAlertChooseLoveView : BaseChildContentView
    {
        public CreateAlertChooseLoveView(BaseContainerView mainContentView) : base(mainContentView)
        {
            InitializeComponent();
            ViewTitle = "Create Alert";
        }

        void OnItemTapped(System.Object sender, System.EventArgs e)
        {
            if (MainContentView is AlertContentView alertContentView)
            {
                alertContentView.ShowCreateAlertDetail();
            }
        }
    }
}
