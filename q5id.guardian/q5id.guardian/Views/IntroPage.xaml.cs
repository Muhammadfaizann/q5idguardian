using q5id.guardian.ViewModels;
using q5id.guardian.Views.ContentViews;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using static Xamarin.Essentials.Permissions;

namespace q5id.guardian.Views
{
    public partial class IntroPage : BasePage<IntroViewModel>, IntroView
    {
  

        PidInstrcutionView PidInstrcutionView { get; set; }

        public IntroPage()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (this.BindingContext != null)
            {

                (this.DataContext as IntroViewModel).View = this;
            }

        }

        public async void ShowPidInstruction()
        {
            try
            {

                if (PidInstrcutionView == null)
                {
                    PidInstrcutionView = new PidInstrcutionView();
                    PidInstrcutionView.BindingContext = this.DataContext;
                    PidInstrcutionView.SetBinding(PidInstrcutionView.OkCommandProperty, "OpenPIDCommand");
                }
                await Navigation.PushPopupAsync(PidInstrcutionView);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Can not open popup: " + ex.Message);
            }
        }
    }
}
