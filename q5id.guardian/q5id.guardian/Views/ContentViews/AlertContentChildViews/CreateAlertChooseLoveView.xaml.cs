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

        public override void OnAttach()
        {
            base.OnAttach();
            MainContentView.MainPage.UpdateRightControlImage(Utils.FontAwesomeIcons.Times);
            if(MainContentView is AlertContentView alertContentView)
            {
                alertContentView.AddNewEventHandlerRightControlClicked(OnCustomRightControlClicked);
            }
        }

        public override void OnDettach()
        {
            base.OnDettach();
            if (MainContentView is AlertContentView alertContentView)
            {
                alertContentView.RemoveEventHandlerRightControlClicked(OnCustomRightControlClicked);
            }
        }

        void OnCustomRightControlClicked(System.Object sender, System.EventArgs e)
        {
            if (MainContentView is AlertContentView alertContentView)
            {
                alertContentView.RemoveEventHandlerRightControlClicked(OnCustomRightControlClicked);
                alertContentView.BackToTop();
            }
        }

        void OnItemTapped(System.Object sender, System.EventArgs e)
        {
            if(e is TappedEventArgs tappedEventArgs)
            {
                MainContentView.PushView(new CreateAlertDetailView(MainContentView, tappedEventArgs.Parameter), false);
            }
        }
    }
}
