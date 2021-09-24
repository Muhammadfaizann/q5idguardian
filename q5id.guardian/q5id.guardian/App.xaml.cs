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
            InitializeComponent();
            GetSettings();
        }

        private async void GetSettings()
        {
            var response = await AppService.Instances.GetSettings();
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
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
