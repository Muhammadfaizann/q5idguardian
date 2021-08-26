using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using q5id.guardian.Models;
using q5id.guardian.Services.Bases;
using Xamarin.Essentials;

namespace q5id.guardian.Services
{
    public class AppService : BaseService
    {
        private static string INSTANCES_ID = "df5fc5c2-990a-4c1b-b8cd-fe4301312e2e";
        private static string DATAVAULT_ID = "746cd9c1-3ca3-419e-b100-f16ba8aead57";
        private static string BASE_URL = "https://gw.skypointcloud.com";

        private static AppService mInstances = null;

        public static AppService Instances
        {
            get
            {
                if(mInstances == null)
                {
                    mInstances = new AppService();
                }
                return mInstances;
            }
        }

        public static void Init(string token)
        {
            Preferences.Set(SUBSCRIPTION_KEY, token);
        }

        public async Task<ApiResponse<List<Choice>>> GetChoices()
        {
            string url = $"{BASE_URL}/datavaultmanagement/choice/instances/{INSTANCES_ID}/datavault/{DATAVAULT_ID}";
            return await Get<List<Choice>>(url);
        }

        public async Task<ApiResponse<Structure>> GetSettings()
        {
            string url = $"{BASE_URL}/datavaultmanagement/datavault/instances/{INSTANCES_ID}";
            return await Get<Structure>(url);
        }
    }
}
