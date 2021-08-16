using System;
using System.Collections.Generic;
using q5id.guardian.Views.Base;
using q5id.guardian.Views.ContentViews.AlertContentChildViews;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews
{
    public partial class AlertContentView : BaseContainerView
    {
        public AlertContentView(HomePage homePage) : base(homePage)
        {
            InitializeComponent();
            MainPage.UpdateRightControlVisibility(false);
            ResetView();
            this.PushView(new AlertListVew(this));
        }

        protected override Layout<View> GetContentView()
        {
            return gridContent;
        }
    }
}
