using Foundation;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Presenters;
using MvvmCross.Forms.Presenters;
using q5id.guardian.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace q5id.guardian.iOS.Presenter
{
    public class MvxCustomIosPresenter : MvxFormsIosViewPresenter
    {
        public MvxCustomIosPresenter(IUIApplicationDelegate applicationDelegate, UIWindow window, Xamarin.Forms.Application formsApplication) : base(applicationDelegate, window, formsApplication)
        {
        }

        private IMvxFormsPagePresenter _formsPagePresenter;
        public override IMvxFormsPagePresenter FormsPagePresenter
        {
            get
            {
                if (_formsPagePresenter == null)
                {
                    _formsPagePresenter = new MvxFormCustomPresenter(this);
                    Mvx.IoCProvider.RegisterSingleton(_formsPagePresenter);
                }
                return _formsPagePresenter;
            }
            set
            {
                _formsPagePresenter = value;
            }
        }
    }
}