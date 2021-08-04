using System;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace q5id.guardian.Views.ItemViews
{
    public partial class HomeCarouselItemView : ContentView
    {
        private bool isPlaying = false;
        private MediaElement mediaElement;

        public HomeCarouselItemView()
        {
            InitializeComponent();
        }

        public void ShowMediaPlayer()
        {
            if(mediaElement == null)
            {
                mediaElement = new MediaElement();
                mediaElement.HorizontalOptions = LayoutOptions.Fill;
                mediaElement.VerticalOptions = LayoutOptions.Fill;
                mediaElement.ShowsPlaybackControls = false;
                mediaElement.AutoPlay = false;
                mediaElement.BindingContext = this.BindingContext;
                mediaElement.SetBinding(MediaElement.SourceProperty, "VideoUrl");
                this.frmContentMedia.Content = mediaElement;
            }
        }

        public void StopPlayer()
        {
            this.mediaElement?.Stop();
            frmPlayBtn.IsVisible = true;
            isPlaying = false;
        }

        void PlayerGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (isPlaying)
            {
                this.mediaElement?.Pause();
            }
            else
            {
                this.mediaElement?.Play();

            }
            isPlaying = !isPlaying;
            frmPlayBtn.IsVisible = !frmPlayBtn.IsVisible;
        }
    }
}
