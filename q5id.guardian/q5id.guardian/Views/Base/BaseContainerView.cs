using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace q5id.guardian.Views.Base
{
    public abstract class BaseContainerView : ContentView
    {
        public HomePage MainPage;

        public BaseContainerView(HomePage homePage)
        {
            MainPage = homePage;
        }
    }
}
