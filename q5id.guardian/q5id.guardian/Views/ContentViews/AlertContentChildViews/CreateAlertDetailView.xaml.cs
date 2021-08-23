using System;
using System.Collections.Generic;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.AlertContentChildViews
{
    public partial class CreateAlertDetailView : BaseChildContentView
    {
        public static readonly BindableProperty LoveProperty
        = BindableProperty.Create(nameof(Love),
            typeof(object),
            typeof(CreateAlertDetailView),
            null,
            BindingMode.OneWayToSource);

        public object Love
        {
            get
            {
                return GetValue(LoveProperty);
            }
            set
            {
                SetValue(LoveProperty, value);
            }
        }

        public bool IsYourPersonalNetwork { get; set; }
        public bool IsGuardianNearby { get; set; }
        public bool IsLowEnforcement { get; set; }

        public CreateAlertDetailView(BaseContainerView mainContentView, Object itemModel) : base(mainContentView)
        {
            InitializeComponent();
            ViewTitle = "Create Alert";
            this.SetBinding(LoveProperty, "CreatingLove");
            Love = itemModel;
            InitView();
        }

        private void InitView()
        {
            EntrySearchMap.IsVisible = false;
            ImageCurrentLocation.IsVisible = false;
            ImageSearchLocation.IsVisible = false;

            ImageSourceGuardianNearby.Color = Utils.Utils.GetColorFromResource("lightCharcoal40", Color.LightGray);
            ImageSourceGuardianNearby.FontFamily = Utils.ThemeConstanst.FontAwesomeRegular;
            ImageSourceYourPersonalNetwork.Color = Utils.Utils.GetColorFromResource("lightCharcoal40", Color.LightGray);
            ImageSourceYourPersonalNetwork.FontFamily = Utils.ThemeConstanst.FontAwesomeRegular;
            ImageSourceLowEnforcement.Color = Utils.Utils.GetColorFromResource("lightCharcoal40", Color.LightGray);
            ImageSourceLowEnforcement.FontFamily = Utils.ThemeConstanst.FontAwesomeRegular;

            IsYourPersonalNetwork = false;
            IsGuardianNearby = false;
            IsLowEnforcement = false;
        }

        void OnCurrentMyLocationTapped(System.Object sender, System.EventArgs e)
        {
            FrameDifferenceLocation.BorderColor = Color.Transparent;
            ImageSearchLocation.IsVisible = false;
            EntrySearchMap.IsVisible = false;
            FrameCurrentLocation.BorderColor = Utils.Utils.GetColorFromResource("redNue", Color.Red);
            ImageCurrentLocation.IsVisible = true;
        }

        void OnDifferenceLocationTapped(System.Object sender, System.EventArgs e)
        {
            FrameDifferenceLocation.BorderColor = Utils.Utils.GetColorFromResource("redNue", Color.Red);
            ImageSearchLocation.IsVisible = true;
            EntrySearchMap.IsVisible = true;
            FrameCurrentLocation.BorderColor = Color.Transparent;
            ImageCurrentLocation.IsVisible = false;
        }

        void YourPersonalNetworkTapped(System.Object sender, System.EventArgs e)
        {
            IsYourPersonalNetwork = !IsYourPersonalNetwork;
            String colorResource = IsYourPersonalNetwork ? "redNue" : "lightCharcoal40";
            ImageSourceYourPersonalNetwork.Color = Utils.Utils.GetColorFromResource(colorResource, Color.LightGray);
            ImageSourceYourPersonalNetwork.FontFamily = IsYourPersonalNetwork ? Utils.ThemeConstanst.FontAwesomeSolid : Utils.ThemeConstanst.FontAwesomeRegular;
        }

        void GuardianNearbyTapped(System.Object sender, System.EventArgs e)
        {
            IsGuardianNearby = !IsGuardianNearby;
            String colorResource = IsGuardianNearby ? "redNue" : "lightCharcoal40";
            ImageSourceGuardianNearby.Color = Utils.Utils.GetColorFromResource(colorResource, Color.LightGray);
            ImageSourceGuardianNearby.FontFamily = IsGuardianNearby ? Utils.ThemeConstanst.FontAwesomeSolid : Utils.ThemeConstanst.FontAwesomeRegular;
        }

        void LowEnforcementTapped(System.Object sender, System.EventArgs e)
        {
            IsLowEnforcement = !IsLowEnforcement;
            String colorResource = IsLowEnforcement ? "redNue" : "lightCharcoal40";
            ImageSourceLowEnforcement.Color = Utils.Utils.GetColorFromResource(colorResource, Color.LightGray);
            ImageSourceLowEnforcement.FontFamily = IsLowEnforcement ? Utils.ThemeConstanst.FontAwesomeSolid : Utils.ThemeConstanst.FontAwesomeRegular;
        }
    }
}
