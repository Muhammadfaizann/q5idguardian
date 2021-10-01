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
    public interface IGuardianApi
    {
        [Get("/datavaultmanagement/choice/instances/{instancesId}/datavault/{dataVaultId}")]
        Task<AppServiceResponse<List<Choice>>> GetChoices(string instancesId, string dataVaultId, CancellationToken cancellationToken);

        [Get("/datavaultmanagement/datavault/instances/{instancesId}")]
        Task<AppServiceResponse<List<Structure>>> GetSettings(string instancesId, CancellationToken cancellationToken);
        
        [Post("/datavaultdata/entitydata/instances/{instancesId}/upload/{dataVaultId}/{entityId}")]
        Task<AppServiceResponse> UploadImage(string instancesId, string dataVaultId, string entityId, [Body] object body, CancellationToken cancellationToken);

        [Post("/datavaultdata/entitydata/instances/{instancesId}")]
        Task<AppServiceResponse<EntityResponse<T>>> CreateEntity<T>(string instancesId, [Body] Object body, CancellationToken cancellationToken) where T : BaseEntity;
        
        [Put("/datavaultdata/entitydata/instances/{instancesId}/{primaryId}")]
        Task<AppServiceResponse<EntityResponse<T>>> UpdateEntity<T>(string instancesId, string primaryId, [Body] object body, CancellationToken cancellationToken) where T : BaseEntity;

        [Delete("/datavaultdata/entitydata/instances/{instancesId}/{entityId}/{primaryId}")]
        Task<EntityResponse<T>> DeleteEntity<T>(string instancesId, string entityId, string primaryId, CancellationToken cancellationToken) where T : BaseEntity;

        [Get("/datavaultdata/instances/{instancesId}/entitydata/{entityId}")]
        Task<EntityListResponse<T>> GetEntities<T>(string instancesId, string entityId, [AliasAs("$filter")] string filter, CancellationToken cancellationToken) where T : BaseEntity;

        [Get("/datavaultdata/entitydata/instances/{instancesId}/{entityId}/{userId}")]
        Task<AppServiceResponse<Entity<T>>> GetEntityDetail<T>(string instancesId, string entityId, string userId, CancellationToken cancellationToken) where T : BaseEntity;
    }
}
