using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace QueueVisibilityTimeoutPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new HostBuilder();
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddAzureStorage(a => a.VisibilityTimeout = TimeSpan.FromSeconds(30));
            });
            builder.ConfigureLogging((context, b) =>
            {
                b.AddConsole();
            });

            builder.ConfigureServices((context, services) =>
            {
                services.Configure<QueuesOptions>(options =>
                {
                    options.MaxPollingInterval = TimeSpan.FromSeconds(2);
                    options.VisibilityTimeout = TimeSpan.FromSeconds(40);
                });
            });
            var host = builder.Build();
            using (host)
            {
                host.Run();
            }
        }
    }
}