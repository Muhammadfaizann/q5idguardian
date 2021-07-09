using q5id.guardian.Views;
using Xamarin.Forms;

namespace q5id.guardian
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(LovedOnesPage), typeof(LovedOnesPage));
            Routing.RegisterRoute(nameof(AlertsPage), typeof(AlertsPage));
        }
    }
}
