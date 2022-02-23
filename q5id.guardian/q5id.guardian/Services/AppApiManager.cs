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
using Newtonsoft.Json.Linq;
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
        IApiService<IQ5idApi> q5idApi;
        Dictionary<int, CancellationTokenSource> runningTasks = new Dictionary<int, CancellationTokenSource>();

#if DEBUG
        // For end-to-end debugging in android using emulator use http://10.0.2.2:5000.
        // For iOS and using physical devices, use the ip address and port 
        // of the machine running the API, e.g. http://192.168.0.58:5000
        private static string GUARDIAN_API_BASE_URL = "http://10.0.2.2:5000";
#else
        private static string GUARDIAN_API_BASE_URL = "https://guard-app-msvc-westus-dev-qa.azurewebsites.net";
#endif
    
        public event EventHandler OnUnauthorized;

        public AppApiManager()
        {
            IsConnected = _connectivity.IsConnected;
            q5idApi = new ApiService<IQ5idApi>(GUARDIAN_API_BASE_URL);
            _connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        private static AppApiManager mInstances = null;

        public static AppApiManager Instances
        {
            get
            {
                if (mInstances == null)
                {
                    mInstances = new AppApiManager();
                }
                return mInstances;
            }
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

        public async Task<ApiResponse<List<Choice>>> GetChoices()
        {
            var cts = new CancellationTokenSource();
            var taskGetChoices = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).GetChoices(cts.Token));
            runningTasks.Add(taskGetChoices.Id, cts);
            return await taskGetChoices;
        }

        public async Task<ApiResponse<AppServiceResponse>> UploadImage(string entityName, ImageData image)
        {
            var cts = new CancellationTokenSource();
            var body = new
            {
                File = image.GetBase64Data(),
                Extension = image.Extension
            };
            var task = RemoteRequestAsync<AppServiceResponse>(q5idApi.GetApi(Priority.UserInitiated).UploadImage(entityName, body, cts.Token));
            runningTasks.Add(task.Id, cts);

            return await task;
        }

        public async Task<ApiResponse<AppServiceResponse<Entity<Love>>>> CreateLovedOnes(Love love)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).CreateLovedOne(love.GetParam(), cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<AppServiceResponse<Entity<Love>>>> UpdateLovedOnes(Love love)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).UpdateLovedOne(love.GetParam(), cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<EntityResponse<Love>>> DeleteLovedOnes(Love love)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).DeleteLovedOne(love.Id, love.ObjectId, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<List<Love>>> GetListLovedOnes(string userId)
        {
            var cts = new CancellationTokenSource();
            Task<ApiResponse<List<Love>>> task = null;
            if(userId != null)
            {
                task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).GetUserLovedOnes(userId, cts.Token));
            }
            else
            {
                task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).GetAllLovedOnes(cts.Token));
            }
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<AppServiceResponse<Alert>>> CreateAlert(Alert alert)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).CreateAlert(alert.GetParam(), cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<AppServiceResponse<RapidSOSResponse>>> TriggerRapidSOS(RapidSOSRequest request)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).RapidSOSTrigger(new
            {
                alertId = request.AlertId,
            }, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }


        public async Task<ApiResponse<AppServiceResponse<Alert>>> UpdateAlert(Alert alert)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).UpdateAlert(alert.GetParam(), cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<List<Alert>>> GetListAlert()
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<List<Alert>>(q5idApi.GetApi(Priority.UserInitiated).GetAllAlerts(cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<List<Alert>>> GetListFeedHistoryAlert()
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<List<Alert>>(q5idApi.GetApi(Priority.UserInitiated).GetFeedHistoryAlerts(cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<List<Alert>>> GetNearbyListAlert(double lat, double lng, double radiusInKm)
        {
            lat = Math.Round(lat, 9);
            lng = Math.Round(lng, 9);
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<List<Alert>>(q5idApi.GetApi(Priority.UserInitiated).GetNearbyAlerts(lat, lng, radiusInKm, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<List<Alert>>> GetAlertDetail(string alertId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<List<Alert>>(q5idApi.GetApi(Priority.UserInitiated).GetAlertDetail(alertId, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }


        public async Task<ApiResponse<List<User>>> GetUsers(string email)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).GetUserByEmail(email, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<List<User>>> GetUserProfile(string userId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).GetUserDetail(userId, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<Entity<User>>> CreateAccount(User user)
        {
            var cts = new CancellationTokenSource();
            var param = new
            {
                email = user.Email,
                phone = user.Phone,
                username = user.Email,
                password = user.Password,
                firstName = user.FirstName,
                lastName = user.LastName
            };
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).CreateAccount(param, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<AppServiceResponse<User>>> UpdateUser(User user)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).UpdateUser(user.GetParam(), cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<EntityResponse<UserDevice>>> DeleteUserDevice(UserDevice model)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).DeleteUserDevice(model.Id, model.ObjectId, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<List<UserDevice>>> GetListUserDevices(string userId)
        {
            var cts = new CancellationTokenSource();
            Task<ApiResponse<List<UserDevice>>> task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).GetUserDevicesByUser(userId, cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<AppServiceResponse<UserDevice>>> CreateUserDevice(UserDevice model)
        {
            model.Latitude = Math.Round(model.Latitude, 9);
            model.Longitude = Math.Round(model.Longitude, 9);

            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).CreateUserDevice(model.GetParam(), cts.Token)); //TODO: Is this get or should be put because we are updating user device?
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<ApiResponse<AuthResponse>> Login(string username)
        {
            var cts = new CancellationTokenSource();
            var param = new
            {
                username = username
            };
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).Login(param, cts.Token));
            runningTasks.Add(task.Id, cts);
            var response = await task;
            var result = new ApiResponse<AuthResponse>();
            result.IsSuccess = response.IsSuccess;
            result.Message = response.Message;
            result.ResponseStatusCode = response.ResponseStatusCode;
            if (response.ResponseObject != null)
            {
                var jObject = response.ResponseObject;
                if(jObject["error"] != null)
                {
                    var error = jObject["error"].Value<String>();
                    result.IsSuccess = false;
                    result.Message = error;
                }
                else
                {
                    try
                    {
                        AuthResponse user = jObject.ToObject<AuthResponse>();
                        result.ResponseObject = user;
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine("Can not parse auth response: " + ex.ToMessage());
                        result.ResponseObject = null;
                    }
                }
            }
            return result;
        }

        public async Task<ApiResponse<User>> PollStatus(string username, AuthResponse authResp)
        {
            Debug.WriteLine("≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥.");
            Debug.WriteLine("    Polling PID Status   ");
            Debug.WriteLine("≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥||");
            Debug.WriteLine(".≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈|");
            var cts = new CancellationTokenSource();
            var param = new
            {
                username = username,
                statusUri = authResp.StatusUri
            };
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(authResp.AccessToken);
            var base64Text = Convert.ToBase64String(plainTextBytes);
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).PollStatus($"Bearer {base64Text}",param, cts.Token));
            runningTasks.Add(task.Id, cts);
            var response = await task;
            var result = new ApiResponse<User>();
            result.IsSuccess = response.IsSuccess;
            result.Message = response.Message;
            result.ResponseStatusCode = response.ResponseStatusCode;
            if (response.ResponseObject != null)
            {
                var jObject = response.ResponseObject;
                if (jObject["error"] != null)
                {
                    var error = jObject["error"].Value<String>();
                    result.IsSuccess = false;
                    result.Message = error;
                }
                else
                {
                    try
                    {
                        User user = jObject.ToObject<User>();
                        result.ResponseObject = user;
                        Debug.WriteLine("≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥.");
                        Debug.WriteLine($"    {user.Status}   ");
                        Debug.WriteLine("≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥≈≤≥||");
                        Debug.WriteLine(".≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈|");
                    }
                    catch (Exception ex)
                    {
                        result.ResponseObject = null;
                        Debug.WriteLine("Can not parse user: " + ex.ToMessage());
                        Debug.WriteLine("║│║│║│║│║│║│║│║│║│║│║│║│║│║│║│║│║│║│║│");
                    }
                }
            }
            return result;
        }

        public async Task<ApiResponse<User>> ForgotPassword(string email)
        {
            var cts = new CancellationTokenSource();
            var param = new
            {
                username = email,
                password = ""
            };
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).ForgotPassword(param, cts.Token));
            runningTasks.Add(task.Id, cts);
            var response = await task;
            var result = new ApiResponse<User>();
            result.IsSuccess = response.IsSuccess;
            result.Message = response.Message;
            result.ResponseStatusCode = response.ResponseStatusCode;
            if (response.ResponseObject != null)
            {
                var jObject = response.ResponseObject;
                if (jObject["error"] != null)
                {
                    var error = jObject["error"].Value<String>();
                    result.IsSuccess = false;
                    result.Message = error;
                }
                else
                {
                    try
                    {
                        result.ResponseObject = new User();
                        result.IsSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Can not parse user: " + ex.ToMessage());
                        result.ResponseObject = null;
                        result.IsSuccess = false;
                    }
                }
            }
            return result;
        }

        public async Task<ApiResponse<AppServiceResponse<Feed>>> CreateFeed(Feed feed)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).CreateAlertFeed(feed.GetParam(), cts.Token));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<GooglePlaceAutoCompleteResult> GetPlaces(string text)
        {
            GooglePlaceAutoCompleteResult results = null;
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).GetPlaces(text, cts.Token));
            runningTasks.Add(task.Id, cts);
            var response = await task;
            if (response.IsSuccess && response.ResponseObject.IsSuccessStatusCode)
            {
                var json = await response.ResponseObject.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                {
                    results = await Task.Run(() =>
                       JsonConvert.DeserializeObject<GooglePlaceAutoCompleteResult>(json)
                    ).ConfigureAwait(false);

                }
            }
            return results;
        }

        public async Task<GooglePlace> GetPlaceDetails(string placeId)
        {
            GooglePlace result = null;
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync(q5idApi.GetApi(Priority.UserInitiated).GetPlaceDetails(placeId, cts.Token));
            runningTasks.Add(task.Id, cts);
            var response = await task;
            if (response.IsSuccess && response.ResponseObject.IsSuccessStatusCode)
            {
                var json = await response.ResponseObject.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                {
                    result = new GooglePlace(JObject.Parse(json));
                }
            }
            return result;
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
                catch (Refit.ApiException apiEx)
                {
                    data.ResponseStatusCode = (int)apiEx.StatusCode;
                    data.ResponseObject = default(T);
                    data.Message = apiEx.Message;
                    data.Errors = new List<string>()
                    {
                        apiEx.Message
                    };
                    Debug.WriteLine("API Exception: ");
                    Debug.WriteLine(apiEx);
                    if (apiEx.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        OnUnauthorized?.Invoke(this, null);
                    }
                }
                catch (Exception ex)
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
