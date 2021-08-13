using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using q5id.guardian.Views.ContentViews.LovedOnesChildContentViews;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews
{
    public partial class LovedOnesContentView : ContentView
    {
        private List<BaseLovedContentChildView> previousContentViews;
        private BaseLovedContentChildView currentContentView;
        private HomePage MainPage;

        public byte[] PrimaryImageSourceByteArray;
        public List<byte[]> SecondaryImageSourceByteArrays = new List<byte[]>();

        public static readonly BindableProperty ResetCommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LovedOnesContentView), null, defaultBindingMode: BindingMode.TwoWay);

        public ICommand ResetCommand
        {
            get { return (ICommand)GetValue(ResetCommandProperty); }
            set
            {
                SetValue(ResetCommandProperty, value);
            }
        }

        public LovedOnesContentView(HomePage homePage)
        {
            InitializeComponent();
            MainPage = homePage;
            this.SetBinding(ResetCommandProperty, "ResetCommand");
            ResetView();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(currentContentView != null)
            {
                currentContentView.BindingContext = this.BindingContext;
            }
        }

        public void ClearImages()
        {
            PrimaryImageSourceByteArray = null;
            SecondaryImageSourceByteArrays.Clear();
        }

        private void ResetView()
        {
            previousContentViews = new List<BaseLovedContentChildView>();
            currentContentView = null;
            ClearImages();
            ResetCommand?.Execute(null);
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
                currentContentView.BindingContext = null;
            }
            view.BindingContext = this.BindingContext;
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
                currentContentView.BindingContext = null;
                BaseLovedContentChildView previousView = previousContentViews[previousContentViews.Count - 1];
                gridContent.Children.Clear();
                gridContent.Children.Add(previousView);
                currentContentView = previousView;
                currentContentView.BindingContext = this.BindingContext;
                previousContentViews.Remove(previousView);
                MainPage.UpdateRightControlVisibility(previousContentViews.Count > 0);
                MainPage.UpdateHeaderTitle(currentContentView.ViewTitle);
            }
        }

        public void OnHomeRightControlClick(object sender, EventArgs e)
        {
            if(previousContentViews.Count > 0)
            {
                ResetView();
            }
        }

    }
}
