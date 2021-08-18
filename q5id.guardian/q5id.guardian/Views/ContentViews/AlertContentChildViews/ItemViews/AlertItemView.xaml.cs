using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.AlertContentChildViews.ItemViews
{
    public partial class AlertItemView : ContentView
    {
        private AlertDefaultItemView defaultItemView;
        private AlertExpandedItemView expandedItemView;
        private ContentView currentView;

        public event EventHandler<object> OnItemClicked;

        public AlertItemView()
        {
            InitializeComponent();
            InitItemView();

        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(currentView != null)
            {
                currentView.BindingContext = this.BindingContext;
            }
        }

        private void InitItemView()
        {
            if(defaultItemView == null)
            {
                defaultItemView = new AlertDefaultItemView();
            }
            defaultItemView.BindingContext = this.BindingContext;
            defaultItemView.OnExpandedEvent += DefaultItemView_OnExpandedEvent;
            this.gridContent.Children.Clear();
            this.gridContent.Children.Add(defaultItemView);
            currentView = defaultItemView;
        }

        private void ExpandedItemView_OnCollapseEvent(object sender, EventArgs e)
        {
            if (expandedItemView != null)
            {
                expandedItemView.BindingContext = null;
                expandedItemView.OnCollapsedEvent -= ExpandedItemView_OnCollapseEvent;
            }
            InitItemView();
        }

        private void DefaultItemView_OnExpandedEvent(object sender, EventArgs e)
        {
            if(defaultItemView != null)
            {
                defaultItemView.BindingContext = null;
                defaultItemView.OnExpandedEvent -= DefaultItemView_OnExpandedEvent;
            }
            if (expandedItemView == null)
            {
                expandedItemView = new AlertExpandedItemView();
            }
            expandedItemView.BindingContext = this.BindingContext;
            expandedItemView.OnCollapsedEvent += ExpandedItemView_OnCollapseEvent;
            this.gridContent.Children.Clear();
            this.gridContent.Children.Add(expandedItemView);
            currentView = expandedItemView;
        }

        private void OnItemTapped(object sender, EventArgs e)
        {
            if(e is TappedEventArgs tappedEventArgs)
            {
                OnItemClicked?.Invoke(sender, tappedEventArgs.Parameter);
            }
        }
    }
}
