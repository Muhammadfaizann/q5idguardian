using System;
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
        public const int HOME_INDEX = 0;
        public const int LOVED_ONES_INDEX = 1;
        public const int ALERT_INDEX = 2;

        private bool isInitPage = false;

        private ContentView HomeView = null;
        private LovedOnesContentView LovedOnesView = null;
        private AlertContentView AlertsView = null;
        private int mCurrentTap = -1;
        private bool mIsDrawerVisible = false;
        private bool mIsAnimation = false;

        public event EventHandler RightControlClicked;

        public HomePage()
        {
            InitializeComponent();
            GridDrawer.IsVisible = false;
            GridDrawer.TranslateTo(-320, 0, 0);
            HomeView = new HomeContentView(this);
            ShowView(HomeView, "HomeVm");
            SelectTab(HOME_INDEX);
            var gridHomeTapGes = new TapGestureRecognizer();
            gridHomeTapGes.Tapped += (object sender, EventArgs e) =>
            {
                ShowHomeView();
            };
            gridHomeTapGes.SetBinding(TapGestureRecognizer.CommandProperty, "OpenHomeTapCommand");
            gridHome.GestureRecognizers.Add(gridHomeTapGes);

            var gridLoveTapGes = new TapGestureRecognizer();
            gridLoveTapGes.Tapped += (object sender, EventArgs e) =>
            {
                ShowLovedOnesView();
            };
            gridLoveTapGes.SetBinding(TapGestureRecognizer.CommandProperty, "OpenLovedOnesTapCommand");
            gridLove.GestureRecognizers.Add(gridLoveTapGes);

            var gridAlertTapGes = new TapGestureRecognizer();
            gridAlertTapGes.Tapped += (object sender, EventArgs e) =>
            {
                ShowAlertView();
            };
            gridAlertTapGes.SetBinding(TapGestureRecognizer.CommandProperty, "OpenAlertTapCommand");
            gridAlert.GestureRecognizers.Add(gridAlertTapGes);

            frmRightControl.Clicked += FrmRightControl_Clicked;
            KeyboardView.OnKeyBoardUpdate += KeyboardView_OnKeyBoardUpdate;
            MainContainer = GridTop;
        }

        private void KeyboardView_OnKeyBoardUpdate(object sender, bool isShow)
        {
            GridBottom.IsVisible = !isShow;
        }

        public void ShowHomeView()
        {
            HomeView = new HomeContentView(this);

            ShowView(HomeView, "HomeVm");
            SelectTab(HOME_INDEX);
            UpdateRightControlVisibility(false);
        }

        public void ShowLovedOnesView()
        {
            LovedOnesView = new LovedOnesContentView(this);
            ShowView(LovedOnesView, "LovedOnesVm");
            SelectTab(LOVED_ONES_INDEX);
            UpdateRightControlVisibility(false);
        }

        public void ShowAlertView()
        {
            AlertsView = new AlertContentView(this);
            ShowView(AlertsView, "AlertsVm");
            SelectTab(ALERT_INDEX);
            UpdateRightControlVisibility(false);
        }

        public void ShowCreateAlertView(int fromView)
        {
            ShowAlertView();
            AlertsView.ShowCreateAlert(fromView);
            UpdateRightControlVisibility(true);
            UpdateRightControlImage(Utils.FontAwesomeIcons.Times);
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

            imageSourceHome.Color = index == HOME_INDEX ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            imageSourceLove.Color = index == LOVED_ONES_INDEX ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            imageSourceAlert.Color = index == ALERT_INDEX ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;

            labelHome.TextColor = index == HOME_INDEX ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            labelLove.TextColor = index == LOVED_ONES_INDEX ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            labelAlert.TextColor = index == ALERT_INDEX ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;

            string headerTitlePage = "Guardian";
            if(index == LOVED_ONES_INDEX)
            {
                headerTitlePage = "Loved Ones";
            }
            else if(index == ALERT_INDEX)
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

        async void OnDrawerTapped(System.Object sender, System.EventArgs e)
        {
            if(mIsAnimation == false)
            {
                mIsAnimation = true;
                mIsDrawerVisible = !mIsDrawerVisible;
                //GridDrawer.IsVisible = mIsDrawerVisible;
                if (mIsDrawerVisible)
                {
                    GridDrawer.IsVisible = true;
                    await GridDrawer.TranslateTo(0, 0, 250);
                    mIsAnimation = false;
                }
                else
                {
                    await GridDrawer.TranslateTo(-320, 0, 250);
                    GridDrawer.IsVisible = false;
                    mIsAnimation = false;
                }
            }
            
            
            

            //GridDrawer.IsVisible = !GridDrawer.IsVisible;
        }
    }
}