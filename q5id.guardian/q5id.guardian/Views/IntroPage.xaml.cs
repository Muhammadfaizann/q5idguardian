using q5id.guardian.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using static Xamarin.Essentials.Permissions;

namespace q5id.guardian.Views
{
    public partial class IntroPage : BasePage<IntroViewModel>
    {
        public IntroPage()
        {
            InitializeComponent();
        }
    }
}
