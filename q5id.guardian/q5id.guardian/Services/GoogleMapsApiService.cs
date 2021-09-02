﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using q5id.guardian.Models;
using Xamarin.Forms.Maps;

namespace q5id.guardian.Services
{
    public class GoogleMapsApiService
    {
        static string _googleMapsKey;

        private static GoogleMapsApiService mInstances = null;

        public static GoogleMapsApiService Instances
        {
            get
            {
                if (mInstances == null)
                {
                    mInstances = new GoogleMapsApiService();
                }
                return mInstances;
            }
        }

        private const string ApiBaseAddress = "https://maps.googleapis.com/maps/";
        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
        public static void Initialize(string googleMapsKey)
        {
            _googleMapsKey = googleMapsKey;
        }


        public async Task<GooglePlaceAutoCompleteResult> GetPlaces(string text)
        {
            GooglePlaceAutoCompleteResult results = null;

            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync($"api/place/autocomplete/json?input={Uri.EscapeUriString(text)}&key={_googleMapsKey}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                    {
                        results = await Task.Run(() =>
                           JsonConvert.DeserializeObject<GooglePlaceAutoCompleteResult>(json)
                        ).ConfigureAwait(false);

                    }
                }
            }

            return results;
        }

        public async Task<GooglePlace> GetPlaceDetails(string placeId)
        {
            GooglePlace result = null;
            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync($"api/place/details/json?placeid={Uri.EscapeUriString(placeId)}&key={_googleMapsKey}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                    {
                        result = new GooglePlace(JObject.Parse(json));
                    }
                }
            }

            return result;
        }

        public async Task<GooglePlace> FindPlaceByPosition(Position position)
        {
            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync($"api/place/nearbysearch/json?location={position.Latitude},{position.Longitude}&radius=100&key={_googleMapsKey}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    
                    if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                    {
                        var jsonObj = JObject.Parse(json);
                        if(jsonObj["results"] is JArray jArrayResults && jArrayResults.Count > 0)
                        {
                            JObject firstResult = new JObject();
                            firstResult["result"] = jArrayResults[0];
                            return new GooglePlace(firstResult);
                        }
                    }
                }
            }
            return null;
        }
    }
}
