using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace q5id.guardian.Views.Base
{
    public abstract class BaseContainerView : ContentView
    {
        public HomePage MainPage;
        private BaseChildContentView CurrentView { get; set; }

        public BaseContainerView(HomePage homePage)
        {
            MainPage = homePage;
        }

        public void PushView(BaseChildContentView view)
        {
            if (this.CurrentView != null)
            {
                view.PreviousView = this.CurrentView;
                this.CurrentView.IsVisible = false;
            }
            this.GetContentView().Children.Add(view);
            view.BindingContext = this.BindingContext;
            this.CurrentView = view;
        }

        public void BackView()
        {
            if (this.CurrentView != null && this.CurrentView.PreviousView != null)
            {
                this.CurrentView.PreviousView.IsVisible = true;
                this.GetContentView().Children.Remove(this.CurrentView);
                this.CurrentView = this.CurrentView.PreviousView;
                this.CurrentView.BindingContext = this.BindingContext;
            }
        }

        public void BackToTop()
        {
            while (this.CurrentView != null && this.CurrentView.PreviousView != null)
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
