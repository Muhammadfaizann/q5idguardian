using System;
using Android.Content;
using q5id.guardian.Controls;
using q5id.guardian.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AppListView), typeof(AppListViewRenderer))]
namespace q5id.guardian.Droid.Renderers
{
    public class AppListViewRenderer : ListViewRenderer
    {
        public AppListViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if(e.NewElement is AppListView)
            {
                Control.SetSelector(Android.Resource.Color.Transparent);
            }
        }
    }
}
