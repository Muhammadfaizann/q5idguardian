using q5id.guardian.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace q5id.guardian.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertsPage : BasePage<AlertsViewModel>
    {
        public AlertsPage()
        {
            InitializeComponent();
        }
    }
}