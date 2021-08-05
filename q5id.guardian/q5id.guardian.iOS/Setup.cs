using Foundation;
using Microsoft.Extensions.Logging;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.Platforms.Ios.Presenters;
using q5id.guardian.iOS.Presenter;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace q5id.guardian.iOS
{
    public class Setup : MvxFormsIosSetup<MainApp, App>
    {
        protected override ILoggerProvider CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }

        protected override ILoggerFactory CreateLogFactory()
        {
            // serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.NSLog()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }
        protected override IMvxIosViewPresenter CreateViewPresenter()
        {
            var presenter = new MvxCustomIosPresenter(this.ApplicationDelegate, this.Window, this.FormsApplication);
            Mvx.IoCProvider.RegisterSingleton<IMvxFormsViewPresenter>(presenter);
            return presenter;
        }

    }
}