using Microsoft.Extensions.Logging;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.Platforms.Android.Presenters;
using q5id.guardian.Droid.Presenter;
using Serilog;
using Serilog.Extensions.Logging;


namespace q5id.guardian.Droid
{
    public class Setup : MvxFormsAndroidSetup<MainApp, App>
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
                .WriteTo.AndroidLog()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var presenter = new MvxCustomAndroidPresenter(this.GetViewAssemblies(), this.FormsApplication);
            Mvx.IoCProvider.RegisterSingleton<IMvxFormsViewPresenter>(presenter);
            return presenter;
        }
    }
}