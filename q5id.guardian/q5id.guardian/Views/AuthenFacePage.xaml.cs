using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace q5id.guardian.Views
{
    public partial class AuthenFacePage : ContentPage
    {
        public AuthenFacePage()
        {
            InitializeComponent();
        }

        void OnSwitchCamera_Tapped(System.Object sender, System.EventArgs e)
        {
            if(cameraPreview.Camera == Controls.CameraOptions.Front)
            {
                cameraPreview.Camera = Controls.CameraOptions.Rear;
            }
            else
            {
                cameraPreview.Camera = Controls.CameraOptions.Front;
            }
        }
    }
}
