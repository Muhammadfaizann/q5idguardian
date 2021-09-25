using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Xamarin.Essentials.Permissions;

namespace q5id.guardian.Services
{
    public static class PermissionService
    {
        public static async Task<PermissionStatus> CheckPermissionAsync<T>(T permission)
            where T : BasePermission
        {
            try
            {
                return await permission.CheckStatusAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot check permission: " + ex.Message);
            }
            return PermissionStatus.Denied;
        }

        public static async Task<Boolean> RequestPermissionAsync<T>(T permission)
            where T : BasePermission
        {
            try
            {
                var status = await permission.CheckStatusAsync();
                if (status != PermissionStatus.Granted)
                {
                    await permission.RequestAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot required permission: " + ex.Message);
            }
            return false;
        }
    }
}
