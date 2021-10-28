using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using q5id.guardian.Models;
using q5id.guardian.Services;
using q5id.guardian.ViewModels;
using q5id.guardian.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace q5id.guardian
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            GetInitData();
        }

        private void GetInitData()
        {
            GetChoices();
        }

        private async void GetChoices()
        {
            var response = await AppApiManager.Instances.GetChoices();
            if (response.IsSuccess && response.ResponseObject != null && response.ResponseObject != null)
            {
                var choices = response.ResponseObject;
                if (choices.Count > 0)
                {
                    Utils.Utils.SaveChoices(choices);
                }
            }
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=62a69d61-da6a-4a7b-b197-2763d5e3d299;" +
                              "ios=8650a247-cc19-4619-97a9-366aef90a07d",
                              typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
