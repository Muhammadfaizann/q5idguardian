using MvvmCross.Forms.Views;
using q5id.guardian.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace q5id.guardian.Views
{
    public static class BasePageUtils
    {
        public static Thickness SafeAreaInsets;
    }

    public class BasePage<TViewModel> : MvxContentPage<TViewModel> where TViewModel : BaseViewModel
    {
        protected Layout _mainContainer;
        public Layout MainContainer
        {
            get
            {
                return _mainContainer;
            }
            protected set
            {
                _mainContainer = value;
            }
        }

        public Thickness SafeAreaInsets
        {
            get
            {
                if (BasePageUtils.SafeAreaInsets.IsEmpty)
                {
                    if (Xamarin.Forms.Application.Current.MainPage is Xamarin.Forms.NavigationPage navigationPage)
                    {
                        BasePageUtils.SafeAreaInsets = navigationPage.RootPage.On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
                    }
                    else
                    {
                        BasePageUtils.SafeAreaInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
                    }
                }
                return BasePageUtils.SafeAreaInsets;
            }
        }

        public BasePage()
        {
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                var safeInsets = On<iOS>().SetUseSafeArea(false);
            }

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);

           this.LayoutChanged += MyPage_LayoutChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
           
        }

        protected async virtual void MyPage_LayoutChanged(object sender, EventArgs e)
        {
            // We only need the first call, so we unsubscribe immediately
            LayoutChanged -= MyPage_LayoutChanged;

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                var padding = SafeAreaInsets;
                (MainContainer ?? this.Content as Layout).Padding = new Thickness(padding.Left,padding.Top,padding.Right,0);
                await Task.Delay(100);
                //var safeInsets = On<iOS>().SetUseSafeArea(false);
            }
        }

        public List<T> GetAllChildrens<T>(Layout<View> layout)
        {
            List<T> views = new List<T>();
            foreach (var item in layout.Children)
            {
                if (item is Layout<View> parent)
                {
                    views.AddRange(GetAllChildrens<T>(parent));
                }
                else if (item is T child)
                {
                    views.Add(child);
                }
            }
            return views;
        }
    }
}
