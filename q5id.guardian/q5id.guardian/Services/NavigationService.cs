using q5id.guardian.Models;
using q5id.guardian.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace q5id.guardian.Services
{
    public class NavigationService : INavigationService
    {
        public NavigationService()
        {

        }

        private readonly object _sync = new object();
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();

        public void Register<T>()
        {
            var pageType = typeof(T);
            var pageKey = pageType.Name;

            Register(pageKey, pageType);
        }

        private void Register(string pageKey, Type pageType)
        {
            lock (_sync)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
        }

        public void SetRoot(Type viewModelType, object parameter = null, NavigationType navigationType = NavigationType.ContentPage)
        {
            var page = CreatePageWithViewModel(viewModelType, parameter);

            if (navigationType == NavigationType.ContentPageWithNavigation)
            {
                Application.Current.MainPage = new NavigationPage(page);
            }
            else 
            {
                Application.Current.MainPage = page;
            }
        }

        public async Task NavigateToAsync(Type viewModelType, object parameter = null)
        {
            try
            {
                //Validate input
                if (Application.Current.MainPage == null)
                {
                    SetRoot(viewModelType, parameter, NavigationType.ContentPage);
                    return;
                }

                var page = CreatePageWithViewModel(viewModelType, parameter);
                //Navigation to next page
                if (Application.Current.MainPage is NavigationPage navigationPage)
                {
                    await navigationPage.PushAsync(page);
                }

                //Set detail for Flyout Page
                else if (Application.Current.MainPage is FlyoutPage flyoutPage)
                {
                    //The detail content must be inside a NavigationPage to display Hamburger menu
                    flyoutPage.Detail = new NavigationPage(page);
                    flyoutPage.IsPresented = false;
                }
                else
                {
                    Application.Current.MainPage = page;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task PopAsync()
        {
            try
            {
                //Navigation to previous page
                if (Application.Current.MainPage is NavigationPage navigationPage)
                {
                    await navigationPage.PopAsync();
                }

                //Set detail for Flyout Page
                else if (Application.Current.MainPage is FlyoutPage flyoutPage && flyoutPage.Detail is NavigationPage navigationPageDetail)
                {
                    await navigationPageDetail.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //This method is used for Navigation between Master/Detail inside Mainmenu Page.
        //This page need to have different behaviour in Phone/UWP
        //because in UWP it will use FlyoutPage but in phone, we don't use flyoutPage
        public async Task MasterDetailNavigateToAsync(Type viewModelType, object parameter = null)
        {
            try
            {
                var destinationPage = CreatePageWithViewModel(viewModelType, parameter);

                var mainPage = (Application.Current.MainPage as FlyoutPage).Detail;

                if (mainPage is NavigationPage mainNavigationPage)
                {
                    if (mainNavigationPage.RootPage is FlyoutPage flyoutPage)
                    {
                        //Because the MainMenu is a flyoutPage
                        //so DO NOT wrapped the detail of FlyoutPage inside NavigationPage
                        //or it will display 1 more navigation bar on top of the detailPage
                        flyoutPage.Detail = destinationPage;
                    }
                    else
                    {
                        await mainNavigationPage.Navigation.PushAsync(destinationPage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private BaseViewModel CreateViewModel(Type viewModelType, object parameter)
        {
            BaseViewModel viewModel = Locator.Current.GetService(viewModelType) as BaseViewModel;
            viewModel.Initialize(parameter);

            return viewModel;
        }

        private Page CreatePageWithViewModel(Type viewModelType, object parameter)
        {
            BaseViewModel viewModel = CreateViewModel(viewModelType, parameter);

            Page page = CreatePage(viewModelType, parameter);
            page.BindingContext = viewModel;

            return page;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.Name.Replace("ViewModel", "Page");
            if (_pagesByKey.TryGetValue(viewName, out var viewType))
            {
                return viewType;
            }

            throw new Exception($"ViewModel not found for page: {viewModelType.FullName}");
        }

        
    }
}
