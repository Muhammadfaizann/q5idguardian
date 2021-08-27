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
            AppService.Init("12dc1a185cad4cf0a9e51fcd314c2a59");
            InitializeComponent();
            GetSettings();
        }

        private async void GetSettings()
        {
            var response = await AppService.Instances.GetSettings();
            if (response.IsSuccess)
            {
                var strutures = response.ResponseObject;
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
