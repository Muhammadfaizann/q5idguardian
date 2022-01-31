using q5id.guardian.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace q5id.guardian.Views.Base
{
    public abstract class BaseContainerView : ContentView
    {
        public HomePage MainPage;
        protected List<BaseChildContentView> StackViews { get; set; }
        protected BaseChildContentView CurrentView;

        public BaseContainerView(HomePage homePage)
        {
            MainPage = homePage;
            StackViews = new List<BaseChildContentView>();
        }
        public void PushView(BaseChildContentView view, object bindingContext = null)
        {
            if (this.CurrentView != null)
            {
                this.CurrentView.IsVisible = false;
            }
            this.GetContentView().Children.Add(view);
            StackViews.Add(view);

            if(bindingContext == null)
            {
                view.BindingContext = this.BindingContext;
            }
            else
            {
                view.BindingContext = bindingContext;
            }
            
            this.CurrentView = view;
        }

        public void BackView()
        {
            if (StackViews.Count > 1)
            {
                this.GetContentView().Children.RemoveAt(StackViews.Count - 1);
                StackViews.RemoveAt(StackViews.Count - 1);
                this.CurrentView = StackViews[StackViews.Count - 1];
                this.CurrentView.IsVisible = true;
                this.CurrentView.BindingContext = this.BindingContext;
            }
        }

        public void BackToTop()
        {
            while (StackViews.Count > 1)
            {
                BackView();
            }
        }

        public abstract Grid GetContentView();

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (this.CurrentView != null)
            {
                this.CurrentView.BindingContext = this.BindingContext;
            }
        }
    }
}
