using System;
using System.Collections.Generic;
using System.Net.Http;
using Fusillade;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using q5id.guardian.Models;
using q5id.guardian.Services.Bases;
using Refit;
using Xamarin.Essentials;

namespace q5id.guardian.Services
{
    public class ApiService<T> : IApiService<T>
    {
        private const int DEFAULT_SECOND_TIMEOUT = 30;
        private const string API_TIME_OUT_KEY = "api-timeout";

        public static RefitSettings GetNewtonsoftJsonRefitSettings() => new RefitSettings(new NewtonsoftJsonContentSerializer());

        Func<HttpMessageHandler, T> createClient;
        public ApiService(string apiBaseAddress, Dictionary<String, String> headers = null)
        {
            createClient = messageHandler =>
            {
                int timeout = DEFAULT_SECOND_TIMEOUT;
                if (headers != null && headers.ContainsKey(API_TIME_OUT_KEY))
                {
                    timeout = int.Parse(headers[API_TIME_OUT_KEY]);
                }
#if DEBUG
                var client = new HttpClient(new LoggingHttpHandler(new HttpClientHandler()))

#else
                var client = new HttpClient(messageHandler)
#endif
                {
                    BaseAddress = new Uri(apiBaseAddress),
                    Timeout = TimeSpan.FromSeconds(timeout)

                };
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> entry in headers)
                    {
                        // do something with entry.Value or entry.Key
                        client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                    }
                }
                UserSession userSession = Utils.Utils.GetToken();
                if(userSession != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"{Utils.Constansts.API_TOKEN_TYPE} {userSession.Session}");
                }
                
                return RestService.For<T>(client, GetNewtonsoftJsonRefitSettings());
            };
        }

        private T Background
        {
            get
            {
                return new Lazy<T>(() => createClient(
                    new RateLimitedHttpMessageHandler(new HttpClientHandler(), Priority.Background))).Value;
            }
        }

        private T UserInitiated
        {
            get
            {
                return new Lazy<T>(() => createClient(
                    new RateLimitedHttpMessageHandler(new HttpClientHandler(), Priority.UserInitiated))).Value;
            }
        }

        private T Speculative
        {
            get
            {
                return new Lazy<T>(() => createClient(
                    new RateLimitedHttpMessageHandler(new HttpClientHandler(), Priority.Speculative))).Value;
            }
        }

        public T GetApi(Priority priority)
        {
            switch (priority)
            {
                case Priority.Background:
                    return Background;
                case Priority.UserInitiated:
                    return UserInitiated;
                case Priority.Speculative:
                    return Speculative;
                default:
                    return UserInitiated;
            }
        }

    }
}
