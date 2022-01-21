using MvvmCross.IoC;
using MvvmCross.ViewModels;
using q5id.guardian.Models;
using q5id.guardian.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace q5id.guardian
{
    public class MainApp : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            UserSession userSession = Utils.Utils.GetToken();
            if (userSession != null)
            {
                RegisterAppStart<HomeViewModel>();
            }
            else
            {
                RegisterAppStart<IntroViewModel>();
            }
           
        }

        /// <summary>
        /// Do any UI bound startup actions here
        /// </summary>
        public override Task Startup()
        {
            return base.Startup();
        }
        /// <summary>
        /// If the application is restarted (eg primary activity on Android 
        /// can be restarted) this method will be called before Startup
        /// is called again
        /// </summary>
        public override void Reset()
        {
            base.Reset();
        }
    }
}
