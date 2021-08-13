using System;
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
            gridHome.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    HomeView = new HomeContentView();

                    ShowView(HomeView, "HomeVm");
                    SelectTab(0);
                })
            });

            gridLove.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    LovedOnesView = new LovedOnesContentView(this);
                    ShowView(LovedOnesView, "LovedOnesVm");
                    SelectTab(1);
                })
            });

            gridAlert.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    AlertsView = new AlertContentView();
                    ShowView(AlertsView, "AlertsVm");
                    SelectTab(2);
                })
            });
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

        public void UpdateRightControlVisibility(bool isVisible)
        {
            frmRightControl.IsVisible = isVisible;
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}