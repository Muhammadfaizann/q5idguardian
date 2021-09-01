﻿using System;
using System.Threading.Tasks;
using CarouselView.FormsPlugin.Abstractions;
using q5id.guardian.Utils;
using q5id.guardian.ViewModels;
using q5id.guardian.Views.ContentViews;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace q5id.guardian.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BasePage<HomeViewModel>
    {
        private bool isInitPage = false;

        private ContentView HomeView = null;
        private ContentView LovedOnesView = null;
        private ContentView AlertsView = null;
        private int mCurrentTap = -1;

        public event EventHandler RightControlClicked;

        public HomePage()
        {
            InitializeComponent();
            HomeView = new HomeContentView();
            ShowView(HomeView, "HomeVm");
            SelectTab(0);
            var gridHomeTapGes = new TapGestureRecognizer();
            gridHomeTapGes.Tapped += (object sender, EventArgs e) =>
            {
                HomeView = new HomeContentView();

                ShowView(HomeView, "HomeVm");
                SelectTab(0);
                UpdateRightControlVisibility(false);
            };
            gridHomeTapGes.SetBinding(TapGestureRecognizer.CommandProperty, "OpenHomeTapCommand");
            gridHome.GestureRecognizers.Add(gridHomeTapGes);

            var gridLoveTapGes = new TapGestureRecognizer();
            gridLoveTapGes.Tapped += (object sender, EventArgs e) =>
            {
                LovedOnesView = new LovedOnesContentView(this);
                ShowView(LovedOnesView, "LovedOnesVm");
                SelectTab(1);
                UpdateRightControlVisibility(false);
            };
            gridLoveTapGes.SetBinding(TapGestureRecognizer.CommandProperty, "OpenLovedOnesTapCommand");
            gridLove.GestureRecognizers.Add(gridLoveTapGes);

            var gridAlertTapGes = new TapGestureRecognizer();
            gridAlertTapGes.Tapped += (object sender, EventArgs e) =>
            {
                AlertsView = new AlertContentView(this);
                ShowView(AlertsView, "AlertsVm");
                SelectTab(2);
                UpdateRightControlVisibility(false);
            };
            gridAlertTapGes.SetBinding(TapGestureRecognizer.CommandProperty, "OpenAlertTapCommand");
            gridAlert.GestureRecognizers.Add(gridAlertTapGes);

            frmRightControl.Clicked += FrmRightControl_Clicked;
        }

        private void FrmRightControl_Clicked(object sender, EventArgs e)
        {
            this.RightControlClicked?.Invoke(sender, e);
        }

        private void ShowView(ContentView view, string bindingName = null)
        {
            this.gridContentView.Children.Clear();
            if(bindingName != null)
            {
                view.SetBinding(BindingContextProperty, bindingName);
            }
            this.gridContentView.Children.Add(view);
        }

        public void SelectTab(int index)
        {
            mCurrentTap = index;

            imageSourceHome.Color = index == 0 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            imageSourceLove.Color = index == 1 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            imageSourceAlert.Color = index == 2 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;

            labelHome.TextColor = index == 0 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            labelLove.TextColor = index == 1 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            labelAlert.TextColor = index == 2 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;

            string headerTitlePage = "Guardian";
            if(index == 1)
            {
                headerTitlePage = "Loved Ones";
            }
            else if(index == 2)
            {
                headerTitlePage = "Alerts";
            }
            lbNavigation.Text = headerTitlePage;
        }

        public void UpdateHeaderTitle(string title)
        {
            lbNavigation.Text = title;
        }

        public async void UpdateRightControlVisibility(bool isVisible)
        {
            if (isVisible)
            {
                await Task.Delay(300);
                frmRightControl.IsVisible = isVisible;
            }
            else
            {
                frmRightControl.IsVisible = isVisible;
            }
        }

        public void UpdateRightControlImage(string imageVector)
        {
            imgSourceRightControl.Glyph = imageVector;
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}