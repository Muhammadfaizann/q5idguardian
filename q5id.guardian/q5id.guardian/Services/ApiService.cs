using System;
using System.Collections.Generic;
using System.Net.Http;
using Fusillade;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using q5id.guardian.Services.Bases;
using Refit;
using Xamarin.Essentials;

namespace q5id.guardian.Services
{
    public class ApiService<T> : IApiService<T>
    {
        public static RefitSettings GetNewtonsoftJsonRefitSettings() => new RefitSettings(new NewtonsoftJsonContentSerializer());

        Func<HttpMessageHandler, T> createClient;
        public ApiService(string apiBaseAddress, Dictionary<String, String> headers = null)
        {
            createClient = messageHandler =>
            {
                var client = new HttpClient(messageHandler)
                {
                    BaseAddress = new Uri(apiBaseAddress)
                    
                };
                if(headers != null)
                {
                    foreach (KeyValuePair<string, string> entry in headers)
                    {
                        // do something with entry.Value or entry.Key
                        client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                    }
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
