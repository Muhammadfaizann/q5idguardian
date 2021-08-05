using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Presenters;
using MvvmCross.Forms.Presenters;
using q5id.guardian.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace q5id.guardian.Droid.Presenter
{
    public class MvxCustomAndroidPresenter : MvxFormsAndroidViewPresenter
    {
        public MvxCustomAndroidPresenter(IEnumerable<Assembly> androidViewAssemblies, Application formsApplication) : base(androidViewAssemblies, formsApplication)
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