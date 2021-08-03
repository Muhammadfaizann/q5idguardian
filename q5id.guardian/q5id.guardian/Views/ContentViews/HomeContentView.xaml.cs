using System;
using System.Collections.Generic;
using q5id.guardian.Controls;
using q5id.guardian.Views.ItemViews;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews
{
    public partial class HomeContentView : ContentView
    {
        List<HomeCarouselItemView> mediaElements = new List<HomeCarouselItemView>();
        int mCurrentMediaPosition = -1;

        public HomeContentView()
        {
            InitializeComponent();
        }

        private void MediaElement_BindingContextChanged(object sender, EventArgs e)
        {
            var element = sender as HomeCarouselItemView;
            if(mediaElements.Count == 0)
            {
                element.PreparePlayer();
                mCurrentMediaPosition = 0;
            }
            mediaElements.Add(element);
        }

        private void CarouselView_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            mediaElements[e.PreviousPosition].StopPlayer();
            mediaElements[e.CurrentPosition].PreparePlayer();
            mCurrentMediaPosition = e.CurrentPosition;
        }

        void ToggleView_Changed(System.Object sender, System.EventArgs e)
        {
            if(sender is ToggleView toggle)
            {
                if (toggle.IsActive)
                {
                    gridMap.IsVisible = true;
                    gridContent.IsVisible = false;
                    if (mCurrentMediaPosition > -1)
                    {
                        mediaElements[mCurrentMediaPosition].StopPlayer();
                    }
                }
                else
                {
                    gridMap.IsVisible = false;
                    gridContent.IsVisible = true;
                }
            }
        }
    }
}
