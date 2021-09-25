using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace q5id.guardian.Services
{
    public static class AppCenterService
    {
       public static async Task<Boolean> HadMemoryWarning()
       {
            bool hadMemoryWarning = await Crashes.HasReceivedMemoryWarningInLastSessionAsync();
            return hadMemoryWarning;
       }

        public static async Task<ErrorReport> GetLastSessionCrashReport()
        {
            ErrorReport crashReport = await Crashes.GetLastSessionCrashReportAsync();
            return crashReport;
        }

        public static async Task ToogleStatusCrashReport(bool isEnable)
        {
            await Crashes.SetEnabledAsync(isEnable);
        }

        public static void TrackError(Exception exception, Dictionary<string, string> data = null)
        {
            Crashes.TrackError(exception, properties: data);
        }

        public static async Task ToogleStatusAnalytics(bool isEnable)
        {
            await Analytics.SetEnabledAsync(isEnable);
        }

        public static void TrackEvent(String eventName, Dictionary<string, string> data = null)
        {
            Analytics.TrackEvent(eventName, data);
        }
    }
}
