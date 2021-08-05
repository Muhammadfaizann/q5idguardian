using q5id.guardian.Utils;
using q5id.guardian.ViewModels;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace q5id.guardian.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BasePage<HomeViewModel>
    {

        public HomePage()
        {
            InitializeComponent();
        }
    }
}