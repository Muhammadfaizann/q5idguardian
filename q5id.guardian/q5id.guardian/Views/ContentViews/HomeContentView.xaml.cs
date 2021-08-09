using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Geolocator;
using q5id.guardian.Controls;
using q5id.guardian.Utils;
using q5id.guardian.ViewModels;
using q5id.guardian.Views.ItemViews;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace q5id.guardian.Views.ContentViews
{
    public partial class HomeContentView : ContentView
    {
        private bool isInitMap = false;

        List<HomeCarouselItemView> mediaElements = new List<HomeCarouselItemView>();
        int mCurrentMediaPosition = 0;

        Map map;

        private Plugin.Geolocator.Abstractions.Position userPosition;

        public HomeContentView()
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                await GetLocalLocation();
            });
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
                map.SetBinding(AppMap.AlertItemsSourceProperty, "Alerts");
                frmContentMap.Content = map;
                this.UpdateLocalLocation();
            }
        }

        private async void UpdateLocalLocation()
        {
            if(userPosition == null)
            {
                await GetLocalLocation();
            }
            if (userPosition != null)
            {
                map?.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(userPosition.Latitude, userPosition.Longitude),
                                                 Distance.FromMiles(Constansts.MILES_DEFAULT_MAP_ZOOM_DISTANCT)));
            }
        }

        private async Task GetLocalLocation()
        {
            if (IsLocationAvailable())
            { 
                var locator = CrossGeolocator.Current;
                userPosition = await locator.GetPositionAsync(TimeSpan.FromSeconds(Constansts.SEC_TIMEOUT_LOAD_LOCATION));
                if(this.BindingContext is HomeContentViewModel homeContentViewModel)
                {
                    homeContentViewModel.GetUserAlerts(new Position(userPosition.Latitude, userPosition.Longitude));
                }
            }
        }

        private bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
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
    }
}
