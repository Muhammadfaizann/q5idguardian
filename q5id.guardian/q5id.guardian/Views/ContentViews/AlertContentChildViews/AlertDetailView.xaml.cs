using System;
using System.Collections.Generic;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.AlertContentChildViews
{
    public partial class AlertDetailView : BaseChildContentView
    {
        public static readonly BindableProperty AlertDetailProperty
        = BindableProperty.Create(nameof(AlertDetail),
            typeof(object),
            typeof(AlertDetailView),
            null,
            BindingMode.OneWayToSource);

        private static void OnAlertDetailChange(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is AlertDetailView alertDetailView)
            {
                alertDetailView.AlertDetail = newValue;
            }
        }

        public object AlertDetail
        {
            get
            {
                return GetValue(AlertDetailProperty);
            }
            set
            {
                SetValue(AlertDetailProperty, value);
            }
        }

        public AlertDetailView(BaseContainerView mainContentView, object param) : base(mainContentView)
        {
            InitializeComponent();
            ViewTitle = "Live Alert";
            this.SetBinding(AlertDetailProperty, "AlertDetail");
            AlertDetail = param;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }
    }
}
