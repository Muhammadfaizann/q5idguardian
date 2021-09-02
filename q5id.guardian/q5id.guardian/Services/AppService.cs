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

        public async Task<ApiResponse<List<Structure>>> GetSettings()
        {
            string url = $"{BASE_URL}/datavaultmanagement/datavault/instances/{INSTANCES_ID}";
            return await Get<List<Structure>>(url);
        }

        public async Task<ApiResponse<ImageResponse>> UploadImage(string entityId, ImageData image)
        {
            string url = $"{BASE_URL}/datavaultdata/entitydata/instances/{INSTANCES_ID}/upload/{DATAVAULT_ID}/{entityId}";
            var body = new
            {
                File = image.GetBase64Data(),
                Extension = image.Extension
            };
            return await Post<ImageResponse>(url, body);
        }

        public async Task<ApiResponse<EntityResponse<Love>>> CreateLovedOnes(string entityId, Love love)
        {
            string url = $"{BASE_URL}/datavaultdata/entitydata/instances/{INSTANCES_ID}";
            var body = new
            {
                datavaultId = DATAVAULT_ID,
                entityId = entityId,
                data = love
            };
            return await Post<EntityResponse<Love>>(url, body);
        }

        public async Task<ApiResponse<EntityResponse<Love>>> UpdateLovedOnes(string entityId, Love love)
        {
            string url = $"{BASE_URL}/datavaultdata/entitydata/instances/{INSTANCES_ID}/{love.Id}";
            var body = new
            {
                datavaultId = DATAVAULT_ID,
                entityId = entityId,
                data = love
            };
            return await Put<EntityResponse<Love>>(url, body);
        }

        public async Task<ApiResponse<EntityResponse<Love>>> DeleteLovedOnes(string lovedonesId)
        {
            string url = $"{BASE_URL}/datavaultdata/entitydata/instances/{INSTANCES_ID}/{lovedonesId}";
            return await Delete<EntityResponse<Love>>(url);
        }

        public async Task<ApiResponse<List<Entity<Love>>>> GetListLovedOnes(string entityId)
        {
            string url = $"{BASE_URL}/datavaultdata/entitydata/instances/{INSTANCES_ID}/entity/{entityId}";
            return await Get<List<Entity<Love>>>(url);
        }

        public async Task<ApiResponse<EntityResponse<Alert>>> CreateAlert(string entityId, Alert alert)
        {
            string url = $"{BASE_URL}/datavaultdata/entitydata/instances/{INSTANCES_ID}";
            var body = new
            {
                datavaultId = DATAVAULT_ID,
                entityId = entityId,
                data = alert
            };
            return await Post<EntityResponse<Alert>>(url, body);
        }

        public async Task<ApiResponse<EntityResponse<Alert>>> UpdateAlert(string entityId, Alert alert)
        {
            string url = $"{BASE_URL}/datavaultdata/entitydata/instances/{INSTANCES_ID}/{alert.Id}";
            var body = new
            {
                datavaultId = DATAVAULT_ID,
                entityId = entityId,
                data = alert
            };
            return await Put<EntityResponse<Alert>>(url, body);
        }

        public async Task<ApiResponse<List<Entity<Alert>>>> GetListAlert(string entityId)
        {
            string url = $"{BASE_URL}/datavaultdata/entitydata/instances/{INSTANCES_ID}/entity/{entityId}";
            return await Get<List<Entity<Alert>>>(url);
        }
    }
}
