using System;
using System.Collections.Generic;
using q5id.guardian.ViewModels;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.AlertContentChildViews
{
    public partial class AlertListVew : BaseChildContentView
    {
        public AlertListVew(AlertContentView mainContentView) : base(mainContentView)
        {
            InitializeComponent();
            ViewTitle = "Alers";
        }

        void OnAlertItemClicked(object sender, EventArgs e)
        {
            if (e is TappedEventArgs tappedEventArgs && MainContentView is AlertContentView alertContentView)
            {
                alertContentView.ShowDetail();
                MainContentView.MainPage.UpdateRightControlVisibility(true);
                MainContentView.MainPage.UpdateRightControlImage(Utils.FontAwesomeIcons.ChevronLeft);
            }
        }

        void CreateAlertClicked(System.Object sender, System.EventArgs e)
        {
            if(MainContentView is AlertContentView alertContentView)
            {
                alertContentView.ShowCreateAlertChooseLove();
            }
            MainContentView.MainPage.UpdateRightControlVisibility(true);
            MainContentView.MainPage.UpdateRightControlImage(Utils.FontAwesomeIcons.Times);
        }
    }
}
