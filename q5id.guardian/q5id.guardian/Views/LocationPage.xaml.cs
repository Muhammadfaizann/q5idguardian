using System;
using System.Collections.Generic;
using q5id.guardian.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace q5id.guardian.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationPage : BasePage<LocationViewModel>
    {
        public LocationPage()
        {
            InitializeComponent();
        }
    }
}
