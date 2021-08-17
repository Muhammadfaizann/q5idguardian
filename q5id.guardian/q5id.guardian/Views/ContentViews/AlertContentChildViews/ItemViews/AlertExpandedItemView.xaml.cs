using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.AlertContentChildViews.ItemViews
{
    public partial class AlertExpandedItemView : ContentView
    {
        public event EventHandler OnCollapsedEvent;

        public AlertExpandedItemView()
        {
            InitializeComponent();
        }

        public void OnCollapsedTapped(object sender, System.EventArgs e)
        {
            OnCollapsedEvent?.Invoke(sender, e);
        }
    }
}
