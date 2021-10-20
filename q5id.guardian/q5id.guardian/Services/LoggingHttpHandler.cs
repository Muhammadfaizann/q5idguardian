using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace q5id.guardian.Services
{
    public class LoggingHttpHandler : DelegatingHandler
    {
        public LoggingHttpHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            if (request.Content != null)
            {
                Console.WriteLine(await request.Content.ReadAsStringAsync());
            }
            Console.WriteLine();

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            Console.WriteLine("==================");
            Console.WriteLine("Request:");
            Console.WriteLine(request.ToString());
            Console.WriteLine("Response:");
            Console.WriteLine(response.ToString());
            if (response.Content != null)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Content: "+content);
            }
            Console.WriteLine();

            return response;
        }
    }
}
