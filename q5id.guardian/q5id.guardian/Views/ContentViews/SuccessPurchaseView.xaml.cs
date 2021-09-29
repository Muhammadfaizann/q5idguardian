using System;
using System.Collections.Generic;
using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews
{
    public partial class SuccessPurchaseView : PopupPage
    {
        public event EventHandler OkEventHandler;

        // BindableProperty implementation
        public static readonly BindableProperty OkCommandProperty =
            BindableProperty.Create(nameof(OkCommand), typeof(ICommand), typeof(SuccessPurchaseView), null);

        public ICommand OkCommand
        {
            get { return (ICommand)GetValue(OkCommandProperty); }
            set { SetValue(OkCommandProperty, value); }
        }

        public SuccessPurchaseView()
        {
            InitializeComponent();
        }

        async void OkClicked(System.Object sender, System.EventArgs e)
        {
            OkEventHandler?.Invoke(this, e);
            OkCommand?.Execute(null);
            await Navigation.PopPopupAsync();
        }
    }
}
