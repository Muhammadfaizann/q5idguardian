using System;
using System.ComponentModel;
using q5id.guardian.Controls;
using q5id.guardian.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AppListView), typeof(AppListViewRenderer))]
namespace q5id.guardian.iOS.Renderers
{
    public class AppListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == VisualElement.IsVisibleProperty.PropertyName)
            {
                return;
            }
            base.OnElementPropertyChanged(sender, e);
        }
    }

}