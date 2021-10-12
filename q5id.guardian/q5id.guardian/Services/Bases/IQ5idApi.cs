using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using q5id.guardian.Models;
using Refit;

namespace q5id.guardian.Services.Bases
{
    public interface IQ5idApi
    {
        [Get("/Choice")]
        Task<List<Choice>> GetChoices(CancellationToken cancellationToken);

        [Post("/Profile")]
        Task<AppServiceResponse<Entity<Love>>> CreateLovedOne([Body] Object body, CancellationToken cancellationToken);

        [Put("/Profile")]
        Task<JObject> UpdateLovedOne([Body] Object body, CancellationToken cancellationToken);

        [Delete("/Profile")]
        Task<JObject> DeleteLovedOne([Refit.AliasAs("id")] string id, CancellationToken cancellationToken);

        [Get("/Profile/All")]
        Task<List<Love>> GetAllLovedOnes(CancellationToken cancellationToken);

        [Get("/Profile/UserId")]
        Task<List<Love>> GetUserLovedOnes([Refit.AliasAs("userId")] string id, CancellationToken cancellationToken);

        [Post("/Alert")]
        Task<JObject> CreateAlert([Body] Object body, CancellationToken cancellationToken);

        [Put("/Alert")]
        Task<JObject> UpdateAlert([Body] Object body, CancellationToken cancellationToken);

        [Get("/Alert/All")]
        Task<List<Alert>> GetAllAlerts(CancellationToken cancellationToken);

        [Get("/Alert")]
        Task<List<Alert>> GetAlertDetail([Refit.AliasAs("id")] string id, CancellationToken cancellationToken);

        [Post("/User")]
        Task<JObject> CreateUser([Body] Object body, CancellationToken cancellationToken);

        [Put("/User")]
        Task<JObject> UpdateUser([Body] Object body, CancellationToken cancellationToken);

        [Get("/User/Email")]
        Task<List<User>> GetUserByEmail([Refit.AliasAs("email")] string email, CancellationToken cancellationToken);

        [Get("/User")]
        Task<List<User>> GetUserDetail([Refit.AliasAs("id")] string id, CancellationToken cancellationToken);

        [Post("/AlertFeed")]
        Task<JObject> CreateAlertFeed([Body] Object body, CancellationToken cancellationToken);
    }
}
