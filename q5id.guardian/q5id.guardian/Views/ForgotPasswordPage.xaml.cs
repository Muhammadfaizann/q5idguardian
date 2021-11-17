using System;
using System.Collections.Generic;
using System.Diagnostics;
using q5id.guardian.ViewModels;
using q5id.guardian.Views.ContentViews;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace q5id.guardian.Views
{
    public partial class ForgotPasswordPage : BasePage<ForgotPasswordViewModel>
    {
        public static readonly BindableProperty IsRequestSuccessProperty =
            BindableProperty.Create(nameof(IsRequestSuccess), typeof(Boolean), typeof(ForgotPasswordPage), false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnIsRequestSuccessChanged);

        private static void OnIsRequestSuccessChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ForgotPasswordPage view && newValue is Boolean boolVal)
            {
                if (boolVal == true)
                {
                    view.OnRequestSuccess();
                    view.IsRequestSuccess = false;
                }
            }
        }

        private SuccessForgotPasswordView SuccessForgotPasswordView;

        private async void OnRequestSuccess()
        {
            try
            {

                if (SuccessForgotPasswordView == null)
                {
                    SuccessForgotPasswordView = new SuccessForgotPasswordView();
                    SuccessForgotPasswordView.BindingContext = this.DataContext;
                    SuccessForgotPasswordView.SetBinding(SuccessForgotPasswordView.OkCommandProperty, "CloseCommand");
                }
                await Navigation.PushPopupAsync(SuccessForgotPasswordView);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Can not open popup: " + ex.Message);
            }
        }

        public Boolean IsRequestSuccess
        {
            get
            {
                return (Boolean)GetValue(IsRequestSuccessProperty);
            }
            set
            {
                SetValue(IsRequestSuccessProperty, value);
            }
        }

        public ForgotPasswordPage()
        {
            InitializeComponent();
            this.SetBinding(IsRequestSuccessProperty, "IsRequestSuccess");
        }
    }
}
