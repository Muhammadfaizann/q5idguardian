using System;
using System.Collections.Generic;
using q5id.guardian.Controls;
using q5id.guardian.Utils;
using q5id.guardian.Views.Base;
using q5id.guardian.Views.ContentViews.AlertContentChildViews.ItemViews;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace q5id.guardian.Views.ContentViews.AlertContentChildViews
{
    public partial class AlertDetailView : BaseChildContentView
    {
        private AlertCardInfoView cardInfoView;
        private Map map;

        public AlertDetailView(BaseContainerView mainContentView) : base(mainContentView)
        {
            InitializeComponent();
            ViewTitle = "Live Alert";

            cardInfoView = new AlertCardInfoView()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start
            };
            cardInfoView.BindingContext = this.BindingContext;
            ShowList();
        }

        public void InitView()
        {
            ShowList();
            cardInfoView.InitView();
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
                map.SetBinding(AppMap.PositionItemsSourceProperty, "AlertPositions");
                FrameContentMap.Content = map;
                MoveToCurrentPosition();
            }
        }

        private async void MoveToCurrentPosition()
        {
            var userPosition = await Utils.Utils.GetLocalLocation();
            if(userPosition != null)
            {
                map?.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(userPosition.Latitude, userPosition.Longitude),
                                                 Distance.FromMiles(Constansts.MILES_DEFAULT_MAP_ZOOM_DISTANCT)));
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
