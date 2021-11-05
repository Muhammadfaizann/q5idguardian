using System;
using System.Collections.Generic;
using System.Windows.Input;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.AlertContentChildViews
{
    public partial class CreateAlertChooseLoveView : BaseChildContentView
    {

        public static readonly BindableProperty GetMyLovedOnesCommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CreateAlertChooseLoveView), null, defaultBindingMode: BindingMode.TwoWay);

        public ICommand GetMyLovedOnesCommand
        {
            get { return (ICommand)GetValue(GetMyLovedOnesCommandProperty); }
            set
            {
                SetValue(GetMyLovedOnesCommandProperty, value);
            }
        }

        public CreateAlertChooseLoveView(BaseContainerView mainContentView) : base(mainContentView)
        {
            InitializeComponent();
            ViewTitle = "Create Alert";
            this.SetBinding(GetMyLovedOnesCommandProperty, "GetMyLovedOnesCommand");
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            GetMyLovedOnesCommand?.Execute(null);
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
