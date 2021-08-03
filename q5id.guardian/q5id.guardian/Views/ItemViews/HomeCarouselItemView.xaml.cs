using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace q5id.guardian.Views.ItemViews
{
    public partial class HomeCarouselItemView : ContentView
    {
        private bool isPlaying = false;

        public HomeCarouselItemView()
        {
            InitializeComponent();
        }

        public void StopPlayer()
        {
            this.mediaElement.Stop();
            frmPlayBtn.IsVisible = true;
            isPlaying = false;
        }

        public void PreparePlayer()
        {
            this.mediaElement.Play();
            this.mediaElement.Pause();
        }

        void PlayerGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (isPlaying)
            {
                this.mediaElement.Pause();
            }
            else
            {
                this.mediaElement.Play();

            }
            isPlaying = !isPlaying;
            frmPlayBtn.IsVisible = !frmPlayBtn.IsVisible;
        }
    }
}
