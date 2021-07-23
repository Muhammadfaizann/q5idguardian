using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace q5id.guardian.Views
{
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateSafeArea();
        }

        private void UpdateSafeArea()
        {
            var safeAreaInset = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
            if (safeAreaInset.Top > 0)
            {
                topBox.HeightRequest = safeAreaInset.Top;
            }
            if (safeAreaInset.Bottom > 0)
            {
                bottomBox.HeightRequest = safeAreaInset.Bottom;
            }
        }

    }
}
