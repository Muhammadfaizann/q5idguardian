﻿using System;
using System.Collections.Generic;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.AlertContentChildViews
{
    public partial class AlertListVew : BaseChildContentView
    {
        public AlertListVew(AlertContentView mainContentView) : base(mainContentView)
        {
            InitializeComponent();
            ViewTitle = "Alers";
        }

        void OnAlertItemClicked(object sender, EventArgs e)
        {
            if (e is TappedEventArgs tappedEventArgs)
            {
                MainContentView.PushView(new AlertDetailView(MainContentView, tappedEventArgs.Parameter));
            }
        }
    }
}