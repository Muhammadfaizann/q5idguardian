using System;
using System.Collections.Generic;
using q5id.guardian.Models;
using Xamarin.CommunityToolkit.Core;
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

        private void ShowMediaPlayer()
        {
            if(mediaElement == null)
            {
                mediaElement = new MediaElement();
                mediaElement.HorizontalOptions = LayoutOptions.Fill;
                mediaElement.VerticalOptions = LayoutOptions.Fill;
                mediaElement.ShowsPlaybackControls = false;
                mediaElement.AutoPlay = false;
                if(this.BindingContext is UserPage userPage)
                {
                    mediaElement.Source = MediaSource.FromUri(userPage.VideoUrl);
                }
                this.frmContentMedia.Content = mediaElement;
            }
        }

        private void HideMediaPlayer()
        {
            if (mediaElement != null)
            {
                mediaElement.Source = null;
                mediaElement = null;
                this.frmContentMedia.Content = new BoxView();
            }
        }

        public void StopPlayer()
        {
            this.mediaElement?.Stop();
            HideMediaPlayer();
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
                this.ShowMediaPlayer();
                this.mediaElement?.Play();

            }
            isPlaying = !isPlaying;
            frmPlayBtn.IsVisible = !frmPlayBtn.IsVisible;
        }
    }
}
