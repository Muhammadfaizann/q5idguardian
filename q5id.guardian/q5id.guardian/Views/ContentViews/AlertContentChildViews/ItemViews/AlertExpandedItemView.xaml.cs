using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.AlertContentChildViews.ItemViews
{
    public partial class AlertExpandedItemView : ContentView
    {
        public event EventHandler OnCollapsedEvent;

        public AlertExpandedItemView()
        {
            InitializeComponent();
            UpdateGridContentImage();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
        }

        protected override void InvalidateLayout()
        {
            base.InvalidateLayout();
           
        }

        private void UpdateGridContentImage()
        {
            if(this.GridContentImage != null)
            {
                var deviceWidth = Application.Current.MainPage.Width;
                var paddingHorizonal = 80;
                var paddingImages = 10;
                var heightImageSize = deviceWidth - paddingHorizonal - paddingImages;
                this.GridContentImage.HeightRequest = heightImageSize / 3;
            }
        }

        public void OnCollapsedTapped(object sender, System.EventArgs e)
        {
            OnCollapsedEvent?.Invoke(sender, e);
        }
    }
}
