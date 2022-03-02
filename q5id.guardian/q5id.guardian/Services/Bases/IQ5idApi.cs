using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using q5id.guardian.Models;
using Refit;

namespace q5id.guardian.Services.Bases
{
    public interface IQ5idApi
    {
        [Get("/Choice/All")]
        Task<List<Choice>> GetChoices(CancellationToken cancellationToken);

        [Post("/{entityName}/Upload")]
        Task<AppServiceResponse> UploadImage([Refit.AliasAs("entityName")] string entityName, [Body] Object body, CancellationToken cancellationToken);

        [Post("/Profile")]
        Task<AppServiceResponse<Entity<Love>>> CreateLovedOne([Body] Object body, CancellationToken cancellationToken);

        [Put("/Profile")]
        Task<AppServiceResponse<Entity<Love>>> UpdateLovedOne([Body] Object body, CancellationToken cancellationToken);

        [Delete("/Profile")]
        Task<EntityResponse<Love>> DeleteLovedOne([Refit.AliasAs("id")] string id, [Refit.AliasAs("objectId")] string objectId, CancellationToken cancellationToken);

        [Get("/Profile/All")]
        Task<List<Love>> GetAllLovedOnes(CancellationToken cancellationToken);

        [Get("/Profile/UserId")]
        Task<List<Love>> GetUserLovedOnes([Refit.AliasAs("userId")] string id, CancellationToken cancellationToken);
        [Post("/RapidSOS/trigger/alert")]
        Task<AppServiceResponse<RapidSOSResponse>> RapidSOSTrigger([Body] Object body, CancellationToken cancellationToken);

        [Post("/Alert")]
        Task<AppServiceResponse<Alert>> CreateAlert([Body] Object body, CancellationToken cancellationToken);

        [Put("/Alert")]
        Task<AppServiceResponse<Alert>> UpdateAlert([Body] Object body, CancellationToken cancellationToken);

        [Get("/Alert/All")]
        Task<List<Alert>> GetAllAlerts(CancellationToken cancellationToken);

        [Get("/Alert/FeedHistory")]
        Task<List<Alert>> GetFeedHistoryAlerts(CancellationToken cancellationToken);

        [Get("/Alert/Nearby/WithMyAlerts")]
        Task<List<Alert>> GetNearbyAlerts([Refit.AliasAs("latitude")] double latitude, [Refit.AliasAs("longitude")] double longitude, [Refit.AliasAs("radiusInKilometers")] double radiusInKilometers, CancellationToken cancellationToken);

        [Get("/Alert")]
        Task<List<Alert>> GetAlertDetail([Refit.AliasAs("id")] string id, CancellationToken cancellationToken);

        [Post("/User")]
        Task<AppServiceResponse<User>> CreateUser([Body] Object body, CancellationToken cancellationToken);

        [Put("/User")]
        Task<AppServiceResponse<User>> UpdateUser([Body] Object body, CancellationToken cancellationToken);

        [Get("/UserDevice/UserId")]
        Task<List<UserDevice>> GetUserDevicesByUser([Refit.AliasAs("userId")] string userId, CancellationToken cancellationToken);

        [Post("/UserDevice")]
        Task<AppServiceResponse<UserDevice>> CreateUserDevice([Body] Object body, CancellationToken cancellationToken);

        [Delete("/UserDevice")]
        Task<EntityResponse<UserDevice>> DeleteUserDevice([Refit.AliasAs("id")] string id, [Refit.AliasAs("objectId")] string objectId, CancellationToken cancellationToken);

        [Post("/Authentication/Reset")]
        Task<JObject> ForgotPassword([Body] Object body, CancellationToken cancellationToken);

        [Post("/Authentication/CreateUser")]
        Task<Entity<User>> CreateAccount([Body] Object body, CancellationToken cancellationToken);

        [Post("/Authentication")]
        Task<JObject> Login([Body] Object body, CancellationToken cancellationToken);

#if DEBUG
        [Post("/Authentication/PollDev")]
        Task<JObject> PollDevStatus([Header("Authorization")] string token, [Body] Object body, CancellationToken cancellationToken);
#else
        [Post("/Authentication/Poll")]
        Task<JObject> PollStatus([Header("Authorization")] string token, [Body] Object body,  CancellationToken cancellationToken);
#endif

        [Get("/User/Email")]
        Task<List<User>> GetUserByEmail([Refit.AliasAs("email")] string email, CancellationToken cancellationToken);

        [Get("/User/UserId")]
        Task<List<User>> GetUserDetail([Refit.AliasAs("userId")] string id, CancellationToken cancellationToken);

        [Post("/AlertFeed")]
        Task<AppServiceResponse<Feed>> CreateAlertFeed([Body] Object body, CancellationToken cancellationToken);

        [Get("/GooglePlaces/autocomplete")]
        Task<HttpResponseMessage> GetPlaces([Refit.AliasAs("address")] string address, CancellationToken cancellationToken);

        [Get("/GooglePlaces/details")]
        Task<HttpResponseMessage> GetPlaceDetails([Refit.AliasAs("placeid")] string placeId, CancellationToken cancellationToken);
    }
}
