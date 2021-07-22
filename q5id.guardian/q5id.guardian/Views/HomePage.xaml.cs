using q5id.guardian.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace q5id.guardian.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            SelectTab(0);
            Application.Current.MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Black);
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
        }

        public void SelectTab(int index)
        {
            homeView.IsVisible = index == 0;
            lovedView.IsVisible = index == 1;
            alertView.IsVisible = index == 2;

            imageHome.Source = index == 0 ? ImageSource.FromFile("home_icon_active") : ImageSource.FromFile("home_icon_normal");
            imageLove.Source = index == 1 ? ImageSource.FromFile("heart_icon_active") : ImageSource.FromFile("heart_icon_normal");
            imageAlert.Source = index == 2 ? ImageSource.FromFile("alert_icon_active") : ImageSource.FromFile("alert_icon_normal");

            labelHome.TextColor = index == 0 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            labelLove.TextColor = index == 1 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
            labelAlert.TextColor = index == 2 ? ThemeConstanst.DimPink : ThemeConstanst.DimGray;
        }
    }
}