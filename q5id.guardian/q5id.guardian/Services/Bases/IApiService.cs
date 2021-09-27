using System;
using Fusillade;

namespace q5id.guardian.Services.Bases
{
    public interface IApiService<T>
    {
        T GetApi(Priority priority);
    }
}
