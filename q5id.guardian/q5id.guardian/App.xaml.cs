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
            GoogleMapsApiService.Initialize("AIzaSyBfmbh-Rv6aVh4QP7DR41o-_RwPQKZgMDY");
            AppService.Init("7e83fc21dcb44d1db06b8284b0c0fb89");
            AppApiManager.Init("7e83fc21dcb44d1db06b8284b0c0fb89");
            InitializeComponent();
            GetSettings();
        }

        private async void GetSettings()
        {
            var response = await AppApiManager.Instances.GetSettings();
            if (response.IsSuccess && response.ResponseObject != null && response.ResponseObject.Result != null)
            {
                var strutures = response.ResponseObject.Result;
                if(strutures.Count > 0)
                {
                    Utils.Utils.SaveSetting(strutures[0].Entities);
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
