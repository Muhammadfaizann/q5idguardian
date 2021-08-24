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
            InitializeComponent();
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
