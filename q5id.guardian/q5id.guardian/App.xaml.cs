using q5id.guardian.Models;
using q5id.guardian.Services;
using q5id.guardian.ViewModels;
using q5id.guardian.Views;
using Splat;
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

            Locator.CurrentMutable.RegisterLazySingleton<INavigationService>(() => new NavigationService());

            RegisterPages();

            var navigationService = Locator.Current.GetService<INavigationService>();
            navigationService.SetRoot(typeof(LoginViewModel), null, NavigationType.ContentPageWithNavigation);
        }

        private void RegisterPages()
        {
            var navigationService = Locator.Current.GetService<INavigationService>();
            navigationService.Register<HomePage>();
            navigationService.Register<AlertsPage>();
            navigationService.Register<LoginPage>();
            navigationService.Register<LovedOnesPage>();
            navigationService.Register<SettingPage>();

            Locator.CurrentMutable.Register(() =>
            {
                var model = new HomeViewModel();
                return model;
            });

            Locator.CurrentMutable.Register(() =>
            {
                var model = new AlertsViewModel();
                return model;
            });

            Locator.CurrentMutable.Register(() =>
            {
                var model = new LoginViewModel();
                return model;
            });

            Locator.CurrentMutable.Register(() =>
            {
                var model = new LovedOnesViewModel();
                return model;
            });

            Locator.CurrentMutable.Register(() =>
            {
                var model = new SettingViewModel();
                return model;
            });
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
