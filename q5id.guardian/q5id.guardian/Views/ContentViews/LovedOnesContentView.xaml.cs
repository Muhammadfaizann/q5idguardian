using System;
using System.Collections.Generic;
using q5id.guardian.Views.ContentViews.LovedOnesChildContentViews;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews
{
    public partial class LovedOnesContentView : ContentView
    {
        private List<ContentView> previousContentViews;
        private ContentView currentContentView;

        public LovedOnesContentView()
        {
            InitializeComponent();
            previousContentViews = new List<ContentView>();
            currentContentView = null;
            this.pushView(new LovedOnesListView());
        }

        public void pushView(ContentView view)
        {
            if(currentContentView != null)
            {
                previousContentViews.Add(currentContentView);
            }
            gridContent.Children.Clear();
            gridContent.Children.Add(view);
            currentContentView = view;
        }

        public void backView()
        {
            if(previousContentViews.Count > 0)
            {
                ContentView previousView = previousContentViews[previousContentViews.Count - 1];
                gridContent.Children.Clear();
                gridContent.Children.Add(previousView);
                currentContentView = previousView;
                previousContentViews.Remove(previousView);
            }
        }

    }
}
