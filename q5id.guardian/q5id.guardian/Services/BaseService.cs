using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using q5id.guardian.Services.Bases;
using Xamarin.Essentials;

namespace q5id.guardian.Services
{
    public static class ServiceExtention
    {
        public static string ToMessage(this FlurlHttpException fhx)
        {
            #if DEBUG
            return fhx.Message;
            #else
            return "Sorry, something went wrong. Please try again later.";
            #endif
        }
        public static string ToMessage(this Exception ex)
        {
            #if DEBUG
            return ex.Message;
            #else
            return "Sorry, something went wrong. Please try again later.";
            #endif
        }
    }

    public class BaseService
    {
        public static string SUBSCRIPTION_KEY = "Ocp-Apim-Subscription-Key";

        protected async Task<T> DoPost<T>(string url, object body, bool isAuthorized = true) where T : ApiResponse, new()
        {
            T result = null;
            try
            {
                string token = Preferences.Get(SUBSCRIPTION_KEY, null);
                if (isAuthorized)
                {
                    result = await url.WithHeader(SUBSCRIPTION_KEY, token).PostJsonAsync(body).ReceiveJson<T>();
                }
                else
                {
                    result = await url.PostJsonAsync(body).ReceiveJson<T>();
                }
                result.IsSuccess = true;
                result.ResponseStatusCode = 200;
            }
            catch (FlurlHttpException fhx)
            {
                result = new T()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { fhx.ToMessage() },
                    ResponseStatusCode = fhx.StatusCode != null ? (int)fhx.StatusCode : 500,
                };
                Debug.WriteLine($"POST {url} - {JsonConvert.SerializeObject(body)} : {fhx.Message}");
            }
            catch (Exception ex)
            {
                result = new T()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToMessage() },
                    ResponseStatusCode = 500
                };
                Debug.WriteLine($"POST {url} - {JsonConvert.SerializeObject(body)} : {ex.Message}");
            }

            return result;
        }

        protected async Task<T> DoPostWithoutResponse<T>(string url, object body, bool isAuthorized = true) where T : ApiResponse, new()
        {
            T result = null;
            string token = Preferences.Get(SUBSCRIPTION_KEY, null);
            try
            {
                if (isAuthorized)
                {
                    await url.WithHeader(SUBSCRIPTION_KEY, token).PostJsonAsync(body);
                }
                else
                {
                    await url.PostJsonAsync(body);
                }
                result = new T()
                {
                    IsSuccess = true,
                    ResponseStatusCode = 200
                };

            }
            catch (FlurlHttpException fhx)
            {
                result = new T()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { fhx.ToMessage() },
                    ResponseStatusCode = fhx.StatusCode != null ? (int)fhx.StatusCode : 500
                };
                Debug.WriteLine($"POST {url} - {JsonConvert.SerializeObject(body)} : {fhx.Message}");
            }
            catch (Exception ex)
            {
                result = new T()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToMessage() },
                    ResponseStatusCode = 500
                };
                Debug.WriteLine($"POST {url} - {JsonConvert.SerializeObject(body)} : {ex.Message}");
            }

            return result;
        }

        protected async Task<T> DoPostForm<T>(string url, object body, bool isAuthorized = true) where T : ApiResponse, new()
        {
            T result = null;
            try
            {
                string token = Preferences.Get(SUBSCRIPTION_KEY, null);
                if (isAuthorized)
                {
                    result = await url.WithHeader(SUBSCRIPTION_KEY, token).PostUrlEncodedAsync(body).ReceiveJson<T>();
                }
                else
                {
                    result = await url.PostUrlEncodedAsync(body).ReceiveJson<T>();
                }
                result.IsSuccess = true;
                result.ResponseStatusCode = 200;
            }
            catch (FlurlHttpException fhx)
            {
                result = new T()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { fhx.ToMessage() },
                    ResponseStatusCode = fhx.StatusCode != null ? (int)fhx.StatusCode : 500
                };
                Debug.WriteLine($"POST {url} - {JsonConvert.SerializeObject(body)} : {fhx.Message}");
            }
            catch (Exception ex)
            {
                result = new T()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToMessage() },
                    ResponseStatusCode = 500
                };
                Debug.WriteLine($"POST {url} - {JsonConvert.SerializeObject(body)} : {ex.Message}");
            }

            return result;
        }

        protected async Task<T> DoGet<T>(string url, bool isAuthorized = true) where T : ApiResponse, new()
        {
            T result;
            try
            {
                string token = Preferences.Get(SUBSCRIPTION_KEY, null);
                if (isAuthorized)
                {
                    result = await url.WithHeader(SUBSCRIPTION_KEY, token).GetJsonAsync<T>();
                }
                else
                {
                    result = await url.GetJsonAsync<T>();
                }
                result.IsSuccess = true;
                result.ResponseStatusCode = 200;
            }
            catch (FlurlHttpException fhx)
            {
                result = new T()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { fhx.ToMessage() },
                    ResponseStatusCode = fhx.StatusCode != null ? (int)fhx.StatusCode : 500
                };
                Debug.WriteLine($"GET {url} : {fhx.Message}");
            }
            catch (Exception ex)
            {
                result = new T()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToMessage() },
                    ResponseStatusCode = 500
                };
                Debug.WriteLine($"GET {url} : {ex.Message}");
            }

            return result;
        }

        protected async Task<T> DoPut<T>(string url, object body, bool isAuthorized = true) where T : ApiResponse, new()
        {
            T result;
            try
            {
                string token = Preferences.Get(SUBSCRIPTION_KEY, null);
                if (isAuthorized)
                {
                    result = await url.WithHeader(SUBSCRIPTION_KEY, token).PutJsonAsync(body).ReceiveJson<T>();
                }
                else
                {
                    result = await url.PutJsonAsync(body).ReceiveJson<T>();
                }
                result.IsSuccess = true;
                result.ResponseStatusCode = 200;

            }
            catch (FlurlHttpException fhx)
            {
                result = new T()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { fhx.ToMessage() },
                    ResponseStatusCode = fhx.StatusCode != null ? fhx.StatusCode.Value : 500
                };
                Debug.WriteLine($"PUT {url} - {JsonConvert.SerializeObject(body)} : {fhx.Message}");
            }
            catch (Exception ex)
            {
                result = new T()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToMessage() },
                    ResponseStatusCode = 500
                };
                Debug.WriteLine($"PUT {url} - {JsonConvert.SerializeObject(body)} : {ex.Message}");
            }
            return result;
        }

        protected async Task<T> DoDelete<T>(string url, bool isAuthorized = true) where T : ApiResponse, new()
        {
            T result = null;
            try
            {
                string token = Preferences.Get(SUBSCRIPTION_KEY, null);
                if (isAuthorized)
                {
                    result = await url.WithHeader(SUBSCRIPTION_KEY, token).DeleteAsync().ReceiveJson<T>();
                }
                else
                {
                    result = await url.DeleteAsync().ReceiveJson<T>();
                }
                result.IsSuccess = true;
                result.ResponseStatusCode = 200;
            }
            catch (FlurlHttpException fhx)
            {
                result = new T()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { fhx.ToMessage() },
                    ResponseStatusCode = fhx.StatusCode != null ? fhx.StatusCode.Value : 500
                };
                Debug.WriteLine($"DELETE {url} : {fhx.Message}");
            }
            catch (Exception ex)
            {
                result = new T()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToMessage() },
                    ResponseStatusCode = 500
                };
                Debug.WriteLine($"DELETE {url} : {ex.Message}");
            }

            return result;
        }

        protected async Task<ApiResponse<T>> DoGetAll<T>(string url, bool isAuthorized = true) where T : class, new()
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            T result;
            try
            {
                string token = Preferences.Get(SUBSCRIPTION_KEY, null);
                if (isAuthorized)
                {
                    result = await url.WithHeader(SUBSCRIPTION_KEY, token).GetJsonAsync<T>();
                }
                else
                {
                    result = await url.GetJsonAsync<T>();
                }
                apiResponse.IsSuccess = true;
                apiResponse.ResponseStatusCode = 200;
                apiResponse.ResponseObject = result;
            }
            catch (FlurlHttpException fhx)
            {
                apiResponse = new ApiResponse<T>()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { fhx.ToMessage() },
                    ResponseStatusCode = fhx.StatusCode != null ? fhx.StatusCode.Value : 500
                };
                Debug.WriteLine($"GET {url} : {fhx.Message}");
            }
            catch (Exception ex)
            {
                apiResponse = new ApiResponse<T>()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToMessage() },
                    ResponseStatusCode = 500
                };
                Debug.WriteLine($"GET {url} : {ex.Message}");
            }

            return apiResponse;
        }

        protected async Task<ApiResponse<T>> Post<T>(string url, object body, bool isAuthorized = true) where T : class, new()
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            T result;
            try
            {
                string token = Preferences.Get(SUBSCRIPTION_KEY, null);
                if (isAuthorized)
                {
                    result = await url.WithHeader(SUBSCRIPTION_KEY, token).PostJsonAsync(body).ReceiveJson<T>();
                }
                else
                {
                    result = await url.PostJsonAsync(body).ReceiveJson<T>();
                }
                apiResponse.IsSuccess = true;
                apiResponse.ResponseStatusCode = 200;
                apiResponse.ResponseObject = result;
            }
            catch (FlurlHttpException fhx)
            {
                apiResponse = new ApiResponse<T>()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { fhx.ToMessage() },
                    ResponseStatusCode = fhx.StatusCode != null ? fhx.StatusCode.Value : 500
                };
                Debug.WriteLine($"POST {url} - {JsonConvert.SerializeObject(body)} : {fhx.Message}");
            }
            catch (Exception ex)
            {
                apiResponse = new ApiResponse<T>()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToMessage() },
                    ResponseStatusCode = 500
                };
                Debug.WriteLine($"POST {url} - {JsonConvert.SerializeObject(body)} : {ex.Message}");
            }
            return apiResponse;
        }

        protected async Task<ApiResponse<T>> Get<T>(string url, bool isAuthorized = true) where T : class, new()
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            T result;
            try
            {
                string token = Preferences.Get(SUBSCRIPTION_KEY, null);
                if (isAuthorized)
                {
                    result = await url.WithHeader(SUBSCRIPTION_KEY, token).GetJsonAsync<T>();
                }
                else
                {
                    result = await url.GetJsonAsync<T>();
                }
                apiResponse.IsSuccess = true;
                apiResponse.ResponseStatusCode = 200;
                apiResponse.ResponseObject = result;
            }
            catch (FlurlHttpException fhx)
            {
                apiResponse = new ApiResponse<T>()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { fhx.ToMessage() },
                    ResponseStatusCode = fhx.StatusCode != null ? fhx.StatusCode.Value : 500
                };
                Debug.WriteLine($"GET {url} : {fhx.Message}");
            }
            catch (Exception ex)
            {
                apiResponse = new ApiResponse<T>()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToMessage() },
                    ResponseStatusCode = 500
                };
                Debug.WriteLine($"GET {url} : {ex.Message}");
            }

            return apiResponse;
        }

        protected async Task<ApiResponse<T>> Delete<T>(string url, bool isAuthorized = true) where T : class, new()
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            T result;
            try
            {
                string token = Preferences.Get(SUBSCRIPTION_KEY, null);
                if (isAuthorized)
                {
                    result = await url.WithHeader(SUBSCRIPTION_KEY, token).DeleteAsync().ReceiveJson<T>();
                }
                else
                {
                    result = await url.DeleteAsync().ReceiveJson<T>();
                }
                apiResponse.IsSuccess = true;
                apiResponse.ResponseStatusCode = 200;
                apiResponse.ResponseObject = result;
            }
            catch (FlurlHttpException fhx)
            {
                apiResponse = new ApiResponse<T>()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { fhx.ToMessage() },
                    ResponseStatusCode = fhx.StatusCode != null ? fhx.StatusCode.Value : 500
                };
                Debug.WriteLine($"DELETE {url} : {fhx.Message}");
            }
            catch (Exception ex)
            {
                apiResponse = new ApiResponse<T>()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToMessage() },
                    ResponseStatusCode = 500
                };
                Debug.WriteLine($"DELETE {url} : {ex.Message}");
            }

            return apiResponse;
        }

        protected async Task<ApiResponse<T>> Put<T>(string url, object body, bool isAuthorized = true) where T : class, new()
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            T result;
            try
            {
                string token = Preferences.Get(SUBSCRIPTION_KEY, null);
                if (isAuthorized)
                {
                    result = await url.WithHeader(SUBSCRIPTION_KEY, token).PutJsonAsync(body).ReceiveJson<T>();
                }
                else
                {
                    result = await url.PutJsonAsync(body).ReceiveJson<T>();
                }
                apiResponse.IsSuccess = true;
                apiResponse.ResponseStatusCode = 200;
                apiResponse.ResponseObject = result;
            }
            catch (FlurlHttpException fhx)
            {
                apiResponse = new ApiResponse<T>()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { fhx.ToMessage() },
                    ResponseStatusCode = fhx.StatusCode != null ? fhx.StatusCode.Value : 500
                };
                Debug.WriteLine($"PUT {url} - {JsonConvert.SerializeObject(body)} : {fhx.Message}");
            }
            catch (Exception ex)
            {
                apiResponse = new ApiResponse<T>()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { ex.ToMessage() },
                    ResponseStatusCode = 500
                };
                Debug.WriteLine($"PUT {url} - {JsonConvert.SerializeObject(body)} : {ex.Message}");
            }
            return apiResponse;
        }
    }
}
