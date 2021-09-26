using System;
using System.Collections.Generic;
using q5id.guardian.Models;
using Xamarin.CommunityToolkit.Core;
using Xamarin.Forms;

namespace q5id.guardian.Views.ItemViews
{
    public partial class HomeCarouselItemView : ContentView
    {
        private bool isPlaying = false;
        private bool isStop = true;
        private string videoUrl = "";

        public HomeCarouselItemView()
        {
            InitializeComponent();
            frmPlayBtn.IsVisible = false;
            isPlaying = false;
            isStop = true;
        }

        public void ShowPlayerControl() {
            frmPlayBtn.IsVisible = true;
        }

        public void ShowMediaPlayer()
        {
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (this.BindingContext is UserPage userPage)
            {
                videoUrl = userPage.VideoUrl;
                frmPlayBtn.IsVisible = true;
            }
        }

        public void HidePlayerControl()
        {
            frmPlayBtn.IsVisible = false;
        }

        
        public void StopPlayer()
        {
            mediaElement.Stop();
            isStop = true;
            isPlaying = false;
            this.ShowPlayerControl();
        }

        void PlayerGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (isPlaying)
            {
                mediaElement.Pause();
                this.ShowPlayerControl();
            }
            else
            {
                //VideoPlayer.Play();
                if (isStop)
                {
                    isStop = false;
                    mediaElement.Source = MediaSource.FromUri(videoUrl);
                    mediaElement.Play();
                }
                else
                {
                    mediaElement.Play();
                }
                this.HidePlayerControl();

            }
            isPlaying = !isPlaying;
        }
    }
}
