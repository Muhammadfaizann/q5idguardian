using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace q5id.guardian.Services.Bases
{
    public  interface IBackgroundLocation
    {
        Task StartListening();
        Task StopListening();


    }
}
