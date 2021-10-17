using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Geolocator;
using q5id.guardian.Controls;
using q5id.guardian.Utils;
using q5id.guardian.ViewModels;
using q5id.guardian.Views.Base;
using q5id.guardian.Views.ItemViews;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using static Xamarin.Essentials.Permissions;
using Map = Xamarin.Forms.Maps.Map;

namespace q5id.guardian.Views.ContentViews
{
    public partial class HomeContentView : BaseContainerView
    {
        private bool isInitMap = false;

        List<HomeCarouselItemView> mediaElements = new List<HomeCarouselItemView>();
        int mCurrentMediaPosition = 0;

        Map map;

        private Plugin.Geolocator.Abstractions.Position userPosition;

        public HomeContentView(HomePage homePage) : base(homePage)
        {
            InitializeComponent();
            CheckAndGetLocalLocation();
        }

        private async void CheckAndGetLocalLocation()
        {
            var result = await Utils.Utils.CheckAndRequestLocationPermission();
            if(result == PermissionStatus.Granted)
            {
                await GetLocalLocation();
            }
        }

        

        private void ShowMap()
        {
            if (map == null)
            {
                map = new AppMap();
                map.IsShowingUser = true;
                map.HorizontalOptions = LayoutOptions.Fill;
                map.VerticalOptions = LayoutOptions.Fill;
                map.BindingContext = this.BindingContext;
                map.SetBinding(AppMap.PositionItemsSourceProperty, "AlertPositions");
                frmContentMap.Content = map;
                this.UpdateLocalLocation();
            }
        }

        private async void UpdateLocalLocation()
        {
            if (userPosition == null)
            {
                await GetLocalLocation();
            }
            if (userPosition != null)
            {
                map?.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(userPosition.Latitude, userPosition.Longitude),
                                                 Distance.FromKilometers(Constansts.KM_DEFAULT_MAP_ZOOM_DISTANCT)));
            }

        }

        private async Task GetLocalLocation()
        {
            userPosition = await Utils.Utils.GetLocalLocation();
        }

        private void MediaElement_BindingContextChanged(object sender, EventArgs e)
        {
            var element = sender as HomeCarouselItemView;
            if (mediaElements.Count == 0)
            {
                mCurrentMediaPosition = 0;
                element.ShowMediaPlayer();
                element.ShowPlayerControl();
            }
            mediaElements.Add(element);
        }

        void ToggleView_Changed(System.Object sender, System.EventArgs e)
        {
            if(sender is ToggleView toggle)
            {
                if (toggle.IsActive)
                {
                    gridMap.IsVisible = true;
                    gridContent.IsVisible = false;
                    if (mCurrentMediaPosition > -1 && mediaElements != null && mediaElements.Count > mCurrentMediaPosition)
                    {
                        mediaElements[mCurrentMediaPosition].StopPlayer();
                    }
                    if (isInitMap == false)
                    {
                        ShowMap();
                        isInitMap = true;
                    }
                }
                else
                {
                    gridMap.IsVisible = false;
                    gridContent.IsVisible = true;
                }
            }
        }

        private void CarouselViewControl_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            if (mediaElements.Count == 0 || e.NewValue == mCurrentMediaPosition) return;
            foreach(HomeCarouselItemView itemView in mediaElements)
            {
                itemView.StopPlayer();
            }
            mCurrentMediaPosition = e.NewValue;
            mediaElements[mCurrentMediaPosition].ShowMediaPlayer();
            mediaElements[mCurrentMediaPosition].ShowPlayerControl();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if(Parent == null && mediaElements.Count > 0)
            {
                mediaElements[mCurrentMediaPosition].StopPlayer();
            }
        }

        void OnEmergencyTapped(System.Object sender, System.EventArgs e)
        {
            Call("911");
        }

        void OnNonEmergencyTapped(System.Object sender, System.EventArgs e)
        {
            Call("311");
        }

        public void Call(string number)
        {
            if(DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.DeviceType != DeviceType.Physical)
            {
                App.Current.MainPage.DisplayAlert("Error", "Phone Dialer is not supported on this device", "OK");
                return;
            }
            try
            {
                PhoneDialer.Open(number);
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Debug.WriteLine("Can not call number: " + ex.Message);
            }
        }

        void CreateAlertTapped(System.Object sender, System.EventArgs e)
        {
            MainPage.ShowCreateAlertView(AlertContentView.NAVIGATE_FROM_HOME);
        }

        public override Grid GetContentView()
        {
            return new Grid();
        }
    }
}
