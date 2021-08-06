using System;
using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Graphics.Drawables;
using AndroidX.Core.Content;
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
        private Context mContext;

        public AppMapRenderer(Context context) : base(context)
        {
            mContext = context;
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            map.UiSettings.ZoomControlsEnabled = false;
            map.UiSettings.MyLocationButtonEnabled = false;
        }

        //protected override MarkerOptions CreateMarker(Pin pin)
        //{
        //    var marker = new MarkerOptions();
        //    marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
        //    marker.SetTitle(pin.Label);
        //    marker.SetSnippet(pin.Address);
        //    int height = 50;
        //    int width = 25;
            
        //    Drawable bitmapdraw = ContextCompat.GetDrawable(this.mContext, Resource.Drawable.custom_pin);
        //    if(bitmapdraw is BitmapDrawable bitmapDrawable)
        //    {
        //        Bitmap smallMarker = Bitmap.CreateScaledBitmap(bitmapDrawable.Bitmap, width, height, false);
        //        marker.SetIcon(BitmapDescriptorFactory.FromBitmap(smallMarker));
        //    }
        //    return marker;
        //}
    }
}
