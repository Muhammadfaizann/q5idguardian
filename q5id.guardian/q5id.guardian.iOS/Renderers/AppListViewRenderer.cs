﻿using System;
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
    }

}