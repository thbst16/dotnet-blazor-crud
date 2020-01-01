using BlazorCrud.Shared.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace BlazorCrud.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var patientContext = services.GetRequiredService<PatientContext>();
                    var organizationContext = services.GetRequiredService<OrganizationContext>();
                    var claimContext = services.GetRequiredService<ClaimContext>();
                    var uploadContext = services.GetRequiredService<UploadContext>();
                    var userContext = services.GetRequiredService<UserContext>();
                    DataInitializer.Initialize(patientContext, organizationContext, claimContext, uploadContext, userContext);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
