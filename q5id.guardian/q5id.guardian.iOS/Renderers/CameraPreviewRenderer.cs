using System;
using System.ComponentModel;
using q5id.guardian.Controls;
using q5id.guardian.iOS.Controls;
using q5id.guardian.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CameraPreview), typeof(CameraPreviewRenderer))]
namespace q5id.guardian.iOS.Renderers
{
	public class CameraPreviewRenderer : ViewRenderer<CameraPreview, UICameraPreview>
	{
		UICameraPreview uiCameraPreview;

		protected override void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				// Unsubscribe
				uiCameraPreview.Tapped -= OnCameraPreviewTapped;
			}
			if (e.NewElement != null)
			{
				if (Control == null)
				{
					uiCameraPreview = new UICameraPreview(e.NewElement.Camera);
					SetNativeControl(uiCameraPreview);
				}
				// Subscribe
				uiCameraPreview.Tapped += OnCameraPreviewTapped;
			}
		}


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
			if(e.PropertyName == CameraPreview.CameraProperty.PropertyName)
            {
				UpdateCamera();
			}
        }

		private void UpdateCamera()
        {
			if(uiCameraPreview != null)
            {
				uiCameraPreview.Tapped -= OnCameraPreviewTapped;
			}
			uiCameraPreview = new UICameraPreview(Element.Camera);
			SetNativeControl(uiCameraPreview);
			uiCameraPreview.Tapped += OnCameraPreviewTapped;
		}

        void OnCameraPreviewTapped(object sender, EventArgs e)
		{
			if (uiCameraPreview.IsPreviewing)
			{
				uiCameraPreview.CaptureSession.StopRunning();
				uiCameraPreview.IsPreviewing = false;
			}
			else
			{
				uiCameraPreview.CaptureSession.StartRunning();
				uiCameraPreview.IsPreviewing = true;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Control.CaptureSession.Dispose();
				Control.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
