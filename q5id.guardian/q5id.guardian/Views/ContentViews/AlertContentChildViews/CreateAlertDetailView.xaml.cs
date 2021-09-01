using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Geolocator;
using q5id.guardian.Models;
using q5id.guardian.ViewModels;
using q5id.guardian.Views.Base;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace q5id.guardian.Views.ContentViews.AlertContentChildViews
{
    public partial class CreateAlertDetailView : BaseChildContentView
    {
        public static readonly BindableProperty IsYourPersonalNetworkProperty = BindableProperty.Create(nameof(IsYourPersonalNetwork), typeof(bool), typeof(CreateAlertDetailView), false, BindingMode.TwoWay, null, propertyChanged: OnIsYourPersonalNetworkChanged);

        private static void OnIsYourPersonalNetworkChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CreateAlertDetailView createAlertDetailView)
            {
                createAlertDetailView.UpdateYourPersonalNetwork();
            }
        }

        public bool IsYourPersonalNetwork
        {
            get
            {
                return (bool)GetValue(IsYourPersonalNetworkProperty);
            }
            set
            {
                SetValue(IsYourPersonalNetworkProperty, value);
            }
        }

        public static readonly BindableProperty IsGuardianNearbyProperty = BindableProperty.Create(nameof(IsGuardianNearby), typeof(bool), typeof(CreateAlertDetailView), false, BindingMode.TwoWay, null, propertyChanged: OnIsGuardianNearbyChanged);

        private static void OnIsGuardianNearbyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CreateAlertDetailView createAlertDetailView)
            {
                createAlertDetailView.UpdateGuardianNearby();
            }
        }

        public bool IsGuardianNearby
        {
            get
            {
                return (bool)GetValue(IsGuardianNearbyProperty);
            }
            set
            {
                SetValue(IsGuardianNearbyProperty, value);
            }
        }

        public static readonly BindableProperty IsLowEnforcementProperty = BindableProperty.Create(nameof(IsLowEnforcement), typeof(bool), typeof(CreateAlertDetailView), false, BindingMode.TwoWay, null, propertyChanged: OnIsLowEnforcementChanged);

        private static void OnIsLowEnforcementChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CreateAlertDetailView createAlertDetailView)
            {
                createAlertDetailView.UpdateLowEnforcement();
            }
        }

        public bool IsLowEnforcement
        {
            get
            {
                return (bool)GetValue(IsLowEnforcementProperty);
            }
            set
            {
                SetValue(IsLowEnforcementProperty, value);
            }
        }

        public static readonly BindableProperty AlertPositionProperty = BindableProperty.Create(nameof(AlertPosition), typeof(Position?), typeof(CreateAlertDetailView), null, BindingMode.TwoWay, propertyChanged: OnAlertPositionChanged);

        private static void OnAlertPositionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CreateAlertDetailView)
            {

            }
        }

        public Position? AlertPosition
        {
            get
            {
                return (Position?)GetValue(AlertPositionProperty);
            }
            set
            {
                SetValue(AlertPositionProperty, value);
            }
        }


        public CreateAlertDetailView(BaseContainerView mainContentView) : base(mainContentView)
        {
            InitializeComponent();
            ViewTitle = "Create Alert";
            this.SetBinding(IsYourPersonalNetworkProperty, "IsYourPersonalNetwork");
            this.SetBinding(IsGuardianNearbyProperty, "IsGuardianNearby");
            this.SetBinding(IsLowEnforcementProperty, "IsLowEnforcement");
            this.SetBinding(AlertPositionProperty, "AlertPosition");
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

            EntrySearchMap.SelectedItemChanged += EntrySearchMap_SelectedItemChanged;
        }

        private void EntrySearchMap_SelectedItemChanged(object sender, EventArgs e)
        {
            if (EntrySearchMap.IsVisible)
            {
                if(EntrySearchMap.SelectedItem is GooglePlace googlePlace)
                {
                    AlertPosition = new Position(googlePlace.Latitude, googlePlace.Longitude);
                }
                else
                {
                    AlertPosition = null;
                }
            }
        }

        void OnCurrentMyLocationTapped(System.Object sender, System.EventArgs e)
        {
            FrameDifferenceLocation.BorderColor = Color.Transparent;
            ImageSearchLocation.IsVisible = false;
            EntrySearchMap.IsVisible = false;
            FrameCurrentLocation.BorderColor = Utils.Utils.GetColorFromResource("redNue", Color.Red);
            ImageCurrentLocation.IsVisible = true;
            CheckAndGetLocalLocation();
        }

        void OnDifferenceLocationTapped(System.Object sender, System.EventArgs e)
        {
            FrameDifferenceLocation.BorderColor = Utils.Utils.GetColorFromResource("redNue", Color.Red);
            ImageSearchLocation.IsVisible = true;
            EntrySearchMap.IsVisible = true;
            FrameCurrentLocation.BorderColor = Color.Transparent;
            ImageCurrentLocation.IsVisible = false;
            AlertPosition = null;
        }

        void YourPersonalNetworkTapped(System.Object sender, System.EventArgs e)
        {
            IsYourPersonalNetwork = !IsYourPersonalNetwork;
            UpdateYourPersonalNetwork();
        }

        private void UpdateYourPersonalNetwork()
        {
            String colorResource = IsYourPersonalNetwork ? "redNue" : "lightCharcoal40";
            ImageSourceYourPersonalNetwork.Color = Utils.Utils.GetColorFromResource(colorResource, Color.LightGray);
            ImageSourceYourPersonalNetwork.FontFamily = IsYourPersonalNetwork ? Utils.ThemeConstanst.FontAwesomeSolid : Utils.ThemeConstanst.FontAwesomeRegular;
        }

        void GuardianNearbyTapped(System.Object sender, System.EventArgs e)
        {
            IsGuardianNearby = !IsGuardianNearby;
            UpdateGuardianNearby();
        }

        private void UpdateGuardianNearby()
        {
            String colorResource = IsGuardianNearby ? "redNue" : "lightCharcoal40";
            ImageSourceGuardianNearby.Color = Utils.Utils.GetColorFromResource(colorResource, Color.LightGray);
            ImageSourceGuardianNearby.FontFamily = IsGuardianNearby ? Utils.ThemeConstanst.FontAwesomeSolid : Utils.ThemeConstanst.FontAwesomeRegular;
        }

        void LowEnforcementTapped(System.Object sender, System.EventArgs e)
        {
            IsLowEnforcement = !IsLowEnforcement;
            UpdateLowEnforcement();
        }

        private void UpdateLowEnforcement()
        {
            String colorResource = IsLowEnforcement ? "redNue" : "lightCharcoal40";
            ImageSourceLowEnforcement.Color = Utils.Utils.GetColorFromResource(colorResource, Color.LightGray);
            ImageSourceLowEnforcement.FontFamily = IsLowEnforcement ? Utils.ThemeConstanst.FontAwesomeSolid : Utils.ThemeConstanst.FontAwesomeRegular;
        }

        void CreateAlertClicked(System.Object sender, System.EventArgs e)
        {
            if(MainContentView is AlertContentView alertContentView)
            {
                alertContentView.ShowDetail();
            }
            MainContentView.MainPage.UpdateRightControlImage(Utils.FontAwesomeIcons.ChevronLeft);
        }

        private async void CheckAndGetLocalLocation()
        {
            var result = await Utils.Utils.CheckAndRequestLocationPermission();
            if (result == PermissionStatus.Granted)
            {
                GetLocalLocation();
            }
        }

        private async void GetLocalLocation()
        {
            if (IsLocationAvailable())
            {
                try
                {
                    var locator = CrossGeolocator.Current;
                    var lastKnowPos = await locator.GetLastKnownLocationAsync();
                    AlertPosition = new Position(lastKnowPos.Latitude, lastKnowPos.Longitude);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Cannot get local location: " + ex.Message);
                }
            }
        }

        private bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;
            return CrossGeolocator.Current.IsGeolocationAvailable;
        }
    }
}
