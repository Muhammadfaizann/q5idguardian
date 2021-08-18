using System;
using System.Collections.Generic;
using q5id.guardian.Utils;
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
            MainPage.UpdateRightControlImage(FontAwesomeIcons.ChevronLeft);
            ResetView();
            this.PushView(new AlertListVew(this));
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if(Parent == null)
            {
                MainPage.RightControlClicked -= MainPage_RightControlClicked;
            }
            else
            {
                MainPage.RightControlClicked += MainPage_RightControlClicked;
            }
        }

        private void MainPage_RightControlClicked(object sender, EventArgs e)
        {
            if (CanBackView())
            {
                BackView();
            }
        }

        public override void PushView(BaseChildContentView view)
        {
            base.PushView(view);
            MainPage.UpdateRightControlVisibility(this.previousContentViews.Count > 0);
        }

        public override void BackView()
        {
            base.BackView();
            MainPage.UpdateRightControlVisibility(this.previousContentViews.Count > 0);
        }

        protected override Layout<View> GetContentView()
        {
            return gridContent;
        }
    }
}
