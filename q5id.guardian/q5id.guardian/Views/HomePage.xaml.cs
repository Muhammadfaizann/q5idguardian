using q5id.guardian.Utils;
using q5id.guardian.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace q5id.guardian.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BasePage<HomeViewModel>
    {
        private bool isInitPage = false;

        public HomePage()
        {
            InitializeComponent();
            SelectTab(0);
            gridHome.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    SelectTab(0);
                })
            });

            gridLove.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    SelectTab(1);
                })
            });

            gridAlert.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    SelectTab(2);
                })
            });
           // homeView.BindingContext = new HomeContentViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateSafeArea();
        }

        private void UpdateSafeArea()
        {
            if(isInitPage == false)
            {
                isInitPage = true;
                var safeAreaInset = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
                if (safeAreaInset.Top > 0)
                {
                    topBox.HeightRequest = safeAreaInset.Top;
                }
                if (safeAreaInset.Bottom > 0)
                {
                    bottomBox.HeightRequest = safeAreaInset.Bottom;
                }
            }
        }

        public void SelectTab(int index)
        {
            homeView.IsVisible = index == 0;
            lovedView.IsVisible = index == 1;
            alertView.IsVisible = index == 2;

            imageSourceHome.Color = index == 0 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            imageSourceLove.Color = index == 1 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            imageSourceAlert.Color = index == 2 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            
            labelHome.TextColor = index == 0 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            labelLove.TextColor = index == 1 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            labelAlert.TextColor = index == 2 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
        }
    }
}