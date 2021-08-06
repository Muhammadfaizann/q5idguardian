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
            frmPlayBtn.IsVisible = true;
            isPlaying = false;
        }

        public void ShowPlayerControl() {
            frmPlayBtn.IsVisible = true;
        }

        public void HidePlayerControl()
        {
            frmPlayBtn.IsVisible = false;
        }

        private void ShowMediaPlayer()
        {
            if (mediaElement == null)
            {
                mediaElement = new MediaElement();
                mediaElement.HorizontalOptions = LayoutOptions.Fill;
                mediaElement.VerticalOptions = LayoutOptions.Fill;
                mediaElement.ShowsPlaybackControls = false;
                mediaElement.AutoPlay = false;
                this.frmContentMedia.Content = mediaElement;
            }
            mediaElement.IsVisible = true;
            if (this.BindingContext is UserPage userPage)
            {
                mediaElement.Source = MediaSource.FromUri(userPage.VideoUrl);
            }
        }

        private void HideMediaPlayer()
        {
            if (mediaElement != null)
            {
                mediaElement.IsVisible = false;
                mediaElement.Source = null;
            }
        }

        public void StopPlayer()
        {
            this.mediaElement?.Stop();
            HideMediaPlayer();
            isPlaying = false;
            this.ShowPlayerControl();
        }

        void PlayerGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (isPlaying)
            {
                this.mediaElement?.Pause();
                this.ShowPlayerControl();
            }
            else
            {
                this.ShowMediaPlayer();
                this.mediaElement?.Play();
                this.HidePlayerControl();

            }
            isPlaying = !isPlaying;
        }
    }
}
