using System;
using System.Collections.Generic;
using q5id.guardian.Controls;
using q5id.guardian.Views.Base;
using q5id.guardian.Views.ContentViews.AlertContentChildViews.ItemViews;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

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

        private AlertCardInfoView cardInfoView;
        private Map map;

        public AlertDetailView(BaseContainerView mainContentView, object param) : base(mainContentView)
        {
            InitializeComponent();
            ViewTitle = "Live Alert";
            this.SetBinding(AlertDetailProperty, "AlertDetail");
            AlertDetail = param;

            cardInfoView = new AlertCardInfoView()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start
            };
            cardInfoView.BindingContext = this.BindingContext;
            ShowList();
        }

        private void ShowList()
        {
            StackHeaderMap.Children.Clear();
            StackHeaderList.Children.Add(cardInfoView);
            cardInfoView.BindingContext = this.BindingContext;
            ViewMap.IsVisible = false;
            ViewList.IsVisible = true;
        }

        private void ShowMap()
        {
            InitMap();
            StackHeaderList.Children.Clear();
            StackHeaderMap.Children.Add(cardInfoView);
            cardInfoView.BindingContext = this.BindingContext;
            ViewMap.IsVisible = true;
            ViewList.IsVisible = false;
        }

        private void InitMap()
        {
            if (map == null)
            {
                map = new AppMap();
                map.IsShowingUser = true;
                map.HorizontalOptions = LayoutOptions.Fill;
                map.VerticalOptions = LayoutOptions.Fill;
                map.BindingContext = this.BindingContext;
                FrameContentMap.Content = map;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            cardInfoView.BindingContext = this.BindingContext;
        }

        void OnShowMapTapped(System.Object sender, System.EventArgs e)
        {
            ShowMap();
        }

        void OnShowListTapped(System.Object sender, System.EventArgs e)
        {
            ShowList();
        }

    }
}
