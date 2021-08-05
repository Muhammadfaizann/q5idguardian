using q5id.guardian.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace q5id.guardian.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LovedOnesPage : BasePage<LovedOnesViewModel>
    {
        public LovedOnesPage()
        {
            InitializeComponent();
        }
    }
}