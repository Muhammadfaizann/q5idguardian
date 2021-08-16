﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace q5id.guardian.Views.Base
{
    public abstract class BaseContainerView : ContentView
    {
        protected List<BaseChildContentView> previousContentViews;
        protected BaseChildContentView currentContentView;
        protected HomePage MainPage;

        public BaseContainerView(HomePage homePage)
        {
            MainPage = homePage;
        }

        protected abstract Layout<View> GetContentView();

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (currentContentView != null)
            {
                currentContentView.BindingContext = this.BindingContext;
            }
        }

        protected void ResetView()
        {
            previousContentViews = new List<BaseChildContentView>();
            currentContentView = null;
        }

        public virtual void PushView(BaseChildContentView view)
        {
            if (currentContentView != null)
            {
                previousContentViews.Add(currentContentView);
                currentContentView.BindingContext = null;
            }
            view.BindingContext = this.BindingContext;
            GetContentView().Children.Clear();
            GetContentView().Children.Add(view);
            currentContentView = view;
            MainPage.UpdateHeaderTitle(view.ViewTitle);
        }


        public bool CanBackView()
        {
            return previousContentViews.Count > 0;
        }

        public virtual void BackView()
        {
            if (CanBackView())
            {
                currentContentView.BindingContext = null;
                BaseChildContentView previousView = previousContentViews[previousContentViews.Count - 1];
                GetContentView().Children.Clear();
                GetContentView().Children.Add(previousView);
                currentContentView = previousView;
                currentContentView.BindingContext = this.BindingContext;
                previousContentViews.Remove(previousView);
                MainPage.UpdateHeaderTitle(currentContentView.ViewTitle);
            }
        }
    }
}
