using System;
using System.Collections.Generic;
using System.Diagnostics;
using q5id.guardian.ViewModels;
using q5id.guardian.Views.ContentViews;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace q5id.guardian.Views
{
    public partial class IAPPage : BasePage<IAPViewModel>
    {

        SuccessPurchaseView SuccessPurchaseView { get; set; }

        public static readonly BindableProperty IsSuccessPurchaseProperty =
            BindableProperty.Create(nameof(IsSuccessPurchase), typeof(Boolean), typeof(IAPPage), false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnIsUpdateSuccessChanged);

        private static void OnIsUpdateSuccessChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is IAPPage view && newValue is Boolean boolVal)
            {
                if (boolVal == true)
                {
                    view.OnSuccessPurchase();
                    view.IsSuccessPurchase = false;
                }
            }
        }

        private async void OnSuccessPurchase()
        {
            try
            {
              
                if(SuccessPurchaseView == null)
                {
                    SuccessPurchaseView = new SuccessPurchaseView();
                    SuccessPurchaseView.BindingContext = this.DataContext;
                    SuccessPurchaseView.SetBinding(SuccessPurchaseView.OkCommandProperty, "CloseCommand");
                }
                await Navigation.PushPopupAsync(SuccessPurchaseView);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Can not open popup: " + ex.Message);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(SuccessPurchaseView != null)
            {
                SuccessPurchaseView.BindingContext = this.DataContext;
            }
            
        }

        public Boolean IsSuccessPurchase
        {
            get
            {
                return (Boolean)GetValue(IsSuccessPurchaseProperty);
            }
            set
            {
                SetValue(IsSuccessPurchaseProperty, value);
            }
        }

        public IAPPage()
        {
            InitializeComponent();
            this.SetBinding(IAPPage.IsSuccessPurchaseProperty, "IsSuccessPurchase");
        }
    }
}