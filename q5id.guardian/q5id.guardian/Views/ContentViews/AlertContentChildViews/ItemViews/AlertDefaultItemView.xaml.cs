using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.AlertContentChildViews.ItemViews
{
    public partial class AlertDefaultItemView : ContentView
    {
        public event EventHandler OnExpandedEvent;

        public AlertDefaultItemView()
        {
            InitializeComponent();
        }

        void OnExpandedClicked(System.Object sender, System.EventArgs e)
        {
            OnExpandedEvent?.Invoke(sender, e);
        }
    }
}
