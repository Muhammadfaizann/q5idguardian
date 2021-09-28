using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Fusillade;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Polly;
using q5id.guardian.Models;
using q5id.guardian.Services.Bases;
using Xamarin.Essentials;

namespace q5id.guardian.Services
{
    public class AppApiManager
    {
        IConnectivity _connectivity = CrossConnectivity.Current;
        public bool IsConnected { get; set; }
        public bool IsReachable { get; set; }
        IApiService<IGuardianApi> guardianApi;
        Dictionary<int, CancellationTokenSource> runningTasks = new Dictionary<int, CancellationTokenSource>();
        public static string SUBSCRIPTION_KEY = "Ocp-Apim-Subscription-Key";
        private static string INSTANCES_ID = "df5fc5c2-990a-4c1b-b8cd-fe4301312e2e";
        private static string DATAVAULT_ID = "746cd9c1-3ca3-419e-b100-f16ba8aead57";
        private static string GUARDIAN_BASE_URL = "https://gw.skypointcloud.com";


        public AppApiManager(string guardianBaseUrl)
        {
            IsConnected = _connectivity.IsConnected;
            guardianApi = new ApiService<IGuardianApi>(guardianBaseUrl, new Dictionary<string, string>()
            {
                { SUBSCRIPTION_KEY, GetSubScriptionKey() }
            });
            _connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        private static AppApiManager mInstances = null;

        public static AppApiManager Instances
        {
            get
            {
                if (mInstances == null)
                {
                    mInstances = new AppApiManager(GUARDIAN_BASE_URL);
                }
                return mInstances;
            }
        }

        public static void Init(string token)
        {
            Preferences.Set(SUBSCRIPTION_KEY, token);
        }

        void OnConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            IsConnected = e.IsConnected;

            if (!e.IsConnected)
            {
                // Cancel All Running Task
                var items = runningTasks.ToList();
                foreach (var item in items)
                {
                    item.Value.Cancel();
                    runningTasks.Remove(item.Key);
                }
            }
        }

        private string GetSubScriptionKey()
        {
            return Preferences.Get(SUBSCRIPTION_KEY, "");
        }

        public async Task<ApiResponse<AppServiceResponse<List<Choice>>>> GetChoices()
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<AppServiceResponse<List<Choice>>>(guardianApi.GetApi(Priority.UserInitiated).GetChoices( INSTANCES_ID, DATAVAULT_ID, cts.Token));
            runningTasks.Add(task.Id, cts);

            return await task;
        }

        public async Task<ApiResponse<AppServiceResponse<List<Structure>>>> GetSettings()
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<AppServiceResponse<List<Structure>>>(guardianApi.GetApi(Priority.UserInitiated).GetSettings(INSTANCES_ID, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<ImageResponse>> UploadImage(string entityId, ImageData image)
        {
            var cts = new CancellationTokenSource();
            var body = new
            {
                File = image.GetBase64Data(),
                Extension = image.Extension
            };
            var task = RemoteRequestAsync<ImageResponse>(guardianApi.GetApi(Priority.UserInitiated).UploadImage( INSTANCES_ID, DATAVAULT_ID, entityId, body, cts.Token));
            runningTasks.Add(task.Id, cts);

            return await task;
        }

        private async Task<ApiResponse<AppServiceResponse<EntityResponse<T>>>> CreateEntity<T>(string entityId, T entity) where T : BaseEntity
        {
            var cts = new CancellationTokenSource();
            object body = new
            {
                datavaultId = DATAVAULT_ID,
                entityId = entityId,
                data = entity.GetParam()
            };
            var task = RemoteRequestAsync<AppServiceResponse<EntityResponse<T>>>(guardianApi.GetApi(Priority.UserInitiated).CreateEntity<T>(INSTANCES_ID, body, cts.Token));
            //return await task;
            return await task;
        }

        private async Task<ApiResponse<AppServiceResponse<EntityResponse<T>>>> UpdateEntity<T>(string entityId, T entity, string primaryId) where T : BaseEntity
        {
            var cts = new CancellationTokenSource();
            var body = new
            {
                datavaultId = DATAVAULT_ID,
                entityId = entityId,
                data = entity.GetParam()
            };
            var task = RemoteRequestAsync<AppServiceResponse<EntityResponse<T>>>(guardianApi.GetApi(Priority.UserInitiated).UpdateEntity<T>(INSTANCES_ID, primaryId, body, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<AppServiceResponse<EntityResponse<Love>>>> CreateLovedOnes(string entityId, Love love)
        {
            return await CreateEntity(entityId, love);
        }

        public async Task<ApiResponse<AppServiceResponse<EntityResponse<Love>>>> UpdateLovedOnes(string entityId, Love love)
        {
            return await UpdateEntity(entityId, love, love.ProfileId);
        }

        public async Task<ApiResponse<EntityResponse<Love>>> DeleteLovedOnes(string entityId, string lovedonesId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<EntityResponse<Love>>(guardianApi.GetApi(Priority.UserInitiated).DeleteEntity<Love>(INSTANCES_ID, entityId, lovedonesId, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<EntityListResponse<Love>>> GetListLovedOnes(string entityId, string userId)
        {
            var cts = new CancellationTokenSource();
            string filter = userId != null ? $"ContactId eq '{userId}'" : null;
            var task = RemoteRequestAsync<EntityListResponse<Love>>(guardianApi.GetApi(Priority.UserInitiated).GetEntities<Love>(INSTANCES_ID, entityId, filter, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<AppServiceResponse<EntityResponse<Alert>>>> CreateAlert(string entityId, Alert alert)
        {
            return await CreateEntity(entityId, alert);
        }

        public async Task<ApiResponse<AppServiceResponse<EntityResponse<Alert>>>> UpdateAlert(string entityId, Alert alert)
        {
            return await UpdateEntity(entityId, alert, alert.AlertId);
        }

        public async Task<ApiResponse<EntityListResponse<Alert>>> GetListAlert(string entityId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<EntityListResponse<Alert>>(guardianApi.GetApi(Priority.UserInitiated).GetEntities<Alert>(INSTANCES_ID, entityId, null, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }


        public async Task<ApiResponse<EntityListResponse<User>>> GetUsers(string entityId, string accountName)
        {
            var cts = new CancellationTokenSource();
            string filter = accountName != null ? $"nickname eq '{accountName}'" : null;
            var task = RemoteRequestAsync<EntityListResponse<User>>(guardianApi.GetApi(Priority.UserInitiated).GetEntities<User>(INSTANCES_ID, entityId, filter, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<AppServiceResponse<Entity<User>>>> GetUserProfile(string entityId, string userId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<AppServiceResponse<Entity<User>>>(guardianApi.GetApi(Priority.UserInitiated).GetEntityDetail<User>(INSTANCES_ID, entityId, userId, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<AppServiceResponse<EntityResponse<User>>>> CreateUser(string entityId, User user)
        {
            return await CreateEntity(entityId, user);
        }

        public async Task<ApiResponse<AppServiceResponse<EntityResponse<User>>>> UpdateUser(string entityId, User user)
        {
            return await UpdateEntity(entityId, user, user.ContactId);
        }

        public async Task<ApiResponse<AppServiceResponse<EntityResponse<Feed>>>> CreateFeed(string entityId, Feed feed)
        {
            return await CreateEntity(entityId, feed);
        }

        public async Task<ApiResponse<EntityListResponse<Feed>>> GetFeeds(string entityId, string alertId)
        {
            var cts = new CancellationTokenSource();
            string filter = alertId != null ? $"alertid eq '{alertId}'" : null;
            var task = RemoteRequestAsync<EntityListResponse<Feed>>(guardianApi.GetApi(Priority.UserInitiated).GetEntities<Feed>(INSTANCES_ID, entityId, filter, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        protected async Task<ApiResponse<T>> RemoteRequestAsync<T>(Task<T> task)
        {
            ApiResponse<T> data = new ApiResponse<T>();
            data.IsSuccess = false;
            if (!IsConnected)
            {
                var strngResponse = "There's not a network connection";
                data.ResponseStatusCode = (int)HttpStatusCode.BadRequest;
                data.ResponseObject = default(T);
                data.Message = strngResponse;
                return data;
            }

            IsReachable = await _connectivity.IsRemoteReachable(GUARDIAN_BASE_URL);

            if (!IsReachable)
            {
                var strngResponse = "There's not an internet connection";
                data.ResponseStatusCode = (int)HttpStatusCode.BadRequest;
                data.ResponseObject = default(T);
                data.Message = strngResponse;
                return data;
            }

            data = await Policy
            .Handle<WebException>()
            .Or<TaskCanceledException>()
            .WaitAndRetryAsync
            (
                retryCount: 1,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
            )
            .ExecuteAsync(async () =>
            {
                try
                {
                    var result = await task;
                    runningTasks.Remove(task.Id);
                    data.ResponseStatusCode = (int)HttpStatusCode.OK;
                    data.IsSuccess = true;
                    data.ResponseObject = result;
                }
                catch(Exception ex)
                {
                    data.ResponseStatusCode = (int)HttpStatusCode.InternalServerError;
                    data.ResponseObject = default(T);
                    data.Message = ex.Message;
                    data.Errors = new List<string>()
                    {
                        ex.Message
                    };
                    
                    Debug.WriteLine("API Exception: ");
                    Debug.WriteLine(ex);
                }
                return data;
            });

            return data;
        }
    }
}
