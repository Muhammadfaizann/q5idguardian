using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                if (mInstances == null)
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

        public async Task<ApiResponse<AppServiceResponse<List<Choice>>>> GetChoices()
        {
            string url = $"{BASE_URL}/datavaultmanagement/choice/instances/{INSTANCES_ID}/datavault/{DATAVAULT_ID}";
            return await Get<AppServiceResponse<List<Choice>>>(url);
        }

        public async Task<ApiResponse<AppServiceResponse<List<Structure>>>> GetSettings()
        {
            string url = $"{BASE_URL}/datavaultmanagement/datavault/instances/{INSTANCES_ID}";
            return await Get<AppServiceResponse<List<Structure>>>(url);
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

        private async Task<ApiResponse<EntityResponse<T>>> CreateEntity<T>(string entityId, T entity) where T : BaseEntity
        {
            string url = $"{BASE_URL}/datavaultdata/entitydata/instances/{INSTANCES_ID}";
            var body = new
            {
                datavaultId = DATAVAULT_ID,
                entityId = entityId,
                data = entity.GetParam()
            };
            return await Post<EntityResponse<T>>(url, body);
        }

        private async Task<ApiResponse<EntityResponse<T>>> UpdateEntity<T>(string entityId, T entity, string primaryId) where T : BaseEntity
        {
            string url = $"{BASE_URL}/datavaultdata/entitydata/instances/{INSTANCES_ID}/{primaryId}";
            var body = new
            {
                datavaultId = DATAVAULT_ID,
                entityId = entityId,
                data = entity.GetParam()
            };
            return await Put<EntityResponse<T>>(url, body);
        }

        public async Task<ApiResponse<EntityResponse<Love>>> CreateLovedOnes(string entityId, Love love)
        {
            return await CreateEntity(entityId, love);
        }

        public async Task<ApiResponse<EntityResponse<Love>>> UpdateLovedOnes(string entityId, Love love)
        {
            return await UpdateEntity(entityId, love, love.ProfileId);
        }

        public async Task<ApiResponse<EntityResponse<Love>>> DeleteLovedOnes(string entityId, string lovedonesId)
        {
            string url = $"{BASE_URL}/datavaultdata/entitydata/instances/{INSTANCES_ID}/{entityId}/{lovedonesId}";
            return await Delete<EntityResponse<Love>>(url);
        }

        public async Task<ApiResponse<EntityListResponse<Love>>> GetListLovedOnes(string entityId, string userId)
        {
            string query = userId != null ? $"?$filter=ContactId eq '{userId}'" : "";
            string url = $"{BASE_URL}/datavaultdata/instances/{INSTANCES_ID}/entitydata/{entityId}" + query;
            return await Get<EntityListResponse<Love>>(url);
        }

        public async Task<ApiResponse<EntityResponse<Alert>>> CreateAlert(string entityId, Alert alert)
        {
            return await CreateEntity(entityId, alert);
        }

        public async Task<ApiResponse<EntityResponse<Alert>>> UpdateAlert(string entityId, Alert alert)
        {
            return await UpdateEntity(entityId, alert, alert.AlertId);
        }

        public async Task<ApiResponse<EntityListResponse<Alert>>> GetListAlert(string entityId)
        {
            string url = $"{BASE_URL}/datavaultdata/instances/{INSTANCES_ID}/entitydata/{entityId}";
            return await Get<EntityListResponse<Alert>>(url);
        }


        public async Task<ApiResponse<EntityListResponse<User>>> GetUsers(string entityId, string accountName)
        {
            string url = $"{BASE_URL}/datavaultdata/instances/{INSTANCES_ID}/entitydata/{entityId}?$filter=nickname eq '{accountName}'";

            return await Get<EntityListResponse<User>>(url);
        }

        public async Task<ApiResponse<AppServiceResponse<Entity<User>>>> GetUserProfile(string entityId, string userId)
        {
            string url = $"{BASE_URL}/datavaultdata/entitydata/instances/{INSTANCES_ID}/{entityId}/{userId}";

            return await Get<AppServiceResponse<Entity<User>>>(url);
        }

        public async Task<ApiResponse<EntityResponse<User>>> CreateUser(string entityId, User user)
        {
            string url = $"{BASE_URL}/datavaultdata/entitydata/instances/{INSTANCES_ID}";
            var body = new
            {
                datavaultId = DATAVAULT_ID,
                entityId = entityId,
                data = user
            };
            return await Post<EntityResponse<User>>(url, body);
        }

        public async Task<ApiResponse<EntityResponse<User>>> UpdateUser(string entityId, User user)
        {
            return await UpdateEntity(entityId, user, user.ContactId);
        }

        public async Task<ApiResponse<EntityResponse<Feed>>> CreateFeed(string entityId, Feed feed)
        {
            return await CreateEntity(entityId, feed);
        }

        public async Task<ApiResponse<EntityListResponse<Feed>>> GetFeeds(string entityId, string alertId)
        {
            string url = $"{BASE_URL}/datavaultdata/instances/{INSTANCES_ID}/entitydata/{entityId}?$filter=AlertId eq '{alertId}'";
            return await Get<EntityListResponse<Feed>>(url);
        }
    }
}
