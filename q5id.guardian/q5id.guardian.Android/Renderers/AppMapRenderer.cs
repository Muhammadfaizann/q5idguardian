using System;
using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using q5id.guardian.Controls;
using q5id.guardian.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(AppMap), typeof(AppMapRenderer))]
namespace q5id.guardian.Droid.Renderers
{
    public class AppMapRenderer : MapRenderer
    {
        public AppMapRenderer(Context context) : base(context)
        {

        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            map.UiSettings.ZoomControlsEnabled = false;
            map.UiSettings.MyLocationButtonEnabled = false;
        }
    }
}
