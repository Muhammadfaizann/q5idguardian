using System;
using System.Threading.Tasks;
using CarouselView.FormsPlugin.Abstractions;
using q5id.guardian.DependencyServices;
using q5id.guardian.Utils;
using q5id.guardian.ViewModels;
using q5id.guardian.Views.ContentViews;
using Xamarin.Essentials;
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

        public TapGestureRecognizer GridHomeTapGes { get; }
        public TapGestureRecognizer GridLoveTapGes { get; }
        public TapGestureRecognizer GridAlertTapGes { get; }

        public event EventHandler RightControlClicked;

        public HomePage()
        {
            InitializeComponent();
            GridDrawer.IsVisible = false;
            GridDrawer.TranslateTo(-320, 0, 0);
            HomeView = new HomeContentView(this);
            ShowView(HomeView, "HomeVm");
            SelectTab(HOME_INDEX);
            GridHomeTapGes = new TapGestureRecognizer();
            GridHomeTapGes.Tapped += (object sender, EventArgs e) =>
            {
                ShowHomeView();
            };
            GridHomeTapGes.SetBinding(TapGestureRecognizer.CommandProperty, "OpenHomeTapCommand");
            gridHome.GestureRecognizers.Add(GridHomeTapGes);

            GridLoveTapGes = new TapGestureRecognizer();
            GridLoveTapGes.Tapped += (object sender, EventArgs e) =>
            {
                ShowLovedOnesView();
            };
            GridLoveTapGes.SetBinding(TapGestureRecognizer.CommandProperty, "OpenLovedOnesTapCommand");
            gridLove.GestureRecognizers.Add(GridLoveTapGes);

            GridAlertTapGes = new TapGestureRecognizer();
            GridAlertTapGes.Tapped += (object sender, EventArgs e) =>
            {
                ShowAlertView();
            };
            GridAlertTapGes.SetBinding(TapGestureRecognizer.CommandProperty, "OpenAlertTapCommand");
            gridAlert.GestureRecognizers.Add(GridAlertTapGes);

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

        public void ShowAlertViewLoadData()
        {
            GridAlertTapGes?.Command?.Execute(null);
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

        void OnSubscriptionTapped(System.Object sender, System.EventArgs e)
        {
            IAppDeviceService service = DependencyService.Get<IAppDeviceService>();
            service.OpenSubscriptionManager();
        }

        async void OnPrivacyTapped(System.Object sender, System.EventArgs e)
        {
            Uri uri = new Uri(Utils.Constansts.PRIVACY_POLICY_URL);
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        async void OnHelpCenterTapped(System.Object sender, System.EventArgs e)
        {
            await Launcher.OpenAsync(Utils.Constansts.HELP_URL);
        }

        async void OnFaqTapped(System.Object sender, System.EventArgs e)
        {
            Uri uri = new Uri(Utils.Constansts.FAQ_URL);
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        async void OnEULATapped(System.Object sender, System.EventArgs e)
        {
            Uri uri = new Uri(Utils.Constansts.LICENSE_AGREEMENT);
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        async void OnAboutSelfTapped(System.Object sender, System.EventArgs e)
        {
            Uri uri = new Uri(Utils.Constansts.ABOUT_URL);
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}