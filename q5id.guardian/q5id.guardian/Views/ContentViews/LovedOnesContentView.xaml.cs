using System;
using System.Collections.Generic;
using q5id.guardian.Views.ContentViews.LovedOnesChildContentViews;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews
{
    public partial class LovedOnesContentView : ContentView
    {
        private List<BaseLovedContentChildView> previousContentViews;
        private BaseLovedContentChildView currentContentView;
        private HomePage MainPage;

        public LovedOnesContentView(HomePage homePage)
        {
            InitializeComponent();
            MainPage = homePage;
            ResetView();
        }

        private void ResetView()
        {
            previousContentViews = new List<BaseLovedContentChildView>();
            currentContentView = null;
            this.PushView(new LovedOnesListView(this));
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if(MainPage != null)
            {
                if(Parent != null)
                {
                    MainPage.UpdateRightControlVisibility(previousContentViews.Count > 0);
                    MainPage.RightControlClicked += OnHomeRightControlClick;
                }
                else
                {
                    MainPage.UpdateRightControlVisibility(false);
                    MainPage.RightControlClicked -= OnHomeRightControlClick;
                }
            }
        }

        public void PushView(BaseLovedContentChildView view)
        {
            if(currentContentView != null)
            {
                previousContentViews.Add(currentContentView);
            }
            gridContent.Children.Clear();
            gridContent.Children.Add(view);
            currentContentView = view;
            MainPage.UpdateRightControlVisibility(previousContentViews.Count > 0);
            MainPage.UpdateHeaderTitle(view.ViewTitle);
        }

        
        public bool CanBackView()
        {
            return previousContentViews.Count > 0;
        }

        public void BackView()
        {
            if(CanBackView())
            {
                BaseLovedContentChildView previousView = previousContentViews[previousContentViews.Count - 1];
                gridContent.Children.Clear();
                gridContent.Children.Add(previousView);
                currentContentView = previousView;
                previousContentViews.Remove(previousView);
                MainPage.UpdateRightControlVisibility(previousContentViews.Count > 0);
                MainPage.UpdateHeaderTitle(currentContentView.ViewTitle);
            }
        }

        public void OnHomeRightControlClick(object sender, EventArgs e)
        {
            if(previousContentViews.Count > 0)
            {
                var firstView = previousContentViews[0];
                previousContentViews = new List<BaseLovedContentChildView>();
                currentContentView = null;
                PushView(firstView);
            }
        }

    }
}
