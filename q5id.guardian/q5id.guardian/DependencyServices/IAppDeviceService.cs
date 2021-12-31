using System;
namespace q5id.guardian.DependencyServices
{
    public interface IAppDeviceService
    {
        void OpenSubscriptionManager();
        void DeviceLog(string message, object data);
        string GetDeviceId();
    }
}
