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
            string requestcontent = "";
            string responseContent = "";
            if (request.Content != null)
            {
                requestcontent = await request.Content.ReadAsStringAsync();
            }
            if (response.Content != null)
            {
                responseContent = await response.Content.ReadAsStringAsync();
            }
            Console.WriteLine("==================");
            Console.WriteLine("Request:");
            Console.WriteLine(request.ToString());
            Console.WriteLine("Request Content: "+ requestcontent);
            Console.WriteLine("Response:");
            Console.WriteLine(response.ToString());
            Console.WriteLine("Response Content: " + responseContent);
            Console.WriteLine();

            return response;
        }
    }
}
