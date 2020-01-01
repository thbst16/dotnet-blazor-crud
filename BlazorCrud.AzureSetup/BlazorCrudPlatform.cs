using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.AppService.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;
using System.Collections.Generic;

namespace BlazorCrud.AzureSetup
{
    class BlazorCrudPlatform
    {
        static int Main(string[] args)
        {
            Random rand = new Random();
            string BlazorCrudClient = "becksblazor";
            string BlazorCrudApi = "becksapi";
            string BlazorCrudResourceGroup = "blazor-crud-prod-reg";

            // Test if arguments were supplied
            if (args.Length == 0)
            {
                Console.WriteLine("Please indicate whether you want the environment 'up' or 'down'");
                Console.WriteLine("Usage: dotnet BlazorCrud.AzureSetup.dll <up / down>");
                return 1;
            }

            if (args[0].ToString().ToUpper() == "UP")
            {
                IAzure azure = GetAzureContext();
                // Creates Azure Web Apps for API and Blazor Binaries
                CreateWebApps(azure, BlazorCrudClient, BlazorCrudApi, BlazorCrudResourceGroup);
                return 0;
            }

            if (args[0].ToString().ToUpper() == "DOWN")
            {
                IAzure azure = GetAzureContext();
                // Deletes Azure resource group, which removes all contained web apps
                DeleteWebApps(azure, BlazorCrudClient, BlazorCrudApi, BlazorCrudResourceGroup);
                return 0;
            }

            else
            {
                Console.WriteLine("Invalid argument detected.");
                Console.WriteLine("Usage: dotnet BlazorCrud.AzureSetup.dll <up / down>");
                return 1;
            }
        }

        private static IAzure GetAzureContext()
        {
            IAzure azure = Azure.Authenticate("my.azureauth").WithDefaultSubscription();
            var currentSubscription = azure.GetCurrentSubscription();
            return azure;
        }

        public static void CreateWebApps(IAzure azure, string BlazorCrudClient, string BlazorCrudApi, string BlazorCrudResourceGroup)
        {
            try
            {
                // ================================================================
                // Create the BlazorCRUD Client web app with a new app service plan

                Console.WriteLine("Creating web app " + BlazorCrudClient + " in resource group " + BlazorCrudResourceGroup + "...");
                var app1 = azure.WebApps
                    .Define(BlazorCrudClient)
                    .WithRegion(Region.USEast)
                    .WithNewResourceGroup(BlazorCrudResourceGroup)
                    .WithNewWindowsPlan(PricingTier.StandardS1)
                    .WithHttpsOnly(true)
                    .Create();

                Console.WriteLine("Created web app " + app1.Name);
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();

                //=================================================================
                // Create the BlazorCRUD API web app with the same app service plan

                Console.WriteLine("Creating web app " + BlazorCrudApi + " in resource group " + BlazorCrudResourceGroup + "...");
                var plan = azure.AppServices.AppServicePlans.GetById(app1.AppServicePlanId);
                var app2 = azure.WebApps
                        .Define(BlazorCrudApi)
                        .WithExistingWindowsPlan(plan)
                        .WithExistingResourceGroup(BlazorCrudResourceGroup)
                        .WithHttpsOnly(true)
                        .Create();

                Console.WriteLine("Created web app " + app2.Name);
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
                // Add CORS values to API
                Console.WriteLine("Adding CORS values to API...");
                SiteConfig siteConfig = new SiteConfig();
                List<string> corsValues = new List<string>(new string[]
                {
                    "https://" + BlazorCrudClient + ".azurewebsites.net",
                });
                siteConfig.Cors = new CorsSettings(corsValues);
                app1.Inner.SiteConfig = siteConfig;
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
            catch (Exception g)
            {
                Console.WriteLine(g);
            }
        }

        public static void DeleteWebApps(IAzure azure, string BlazorCrudClient, string BlazorCrudApi, string BlazorCrudResourceGroup)
        {
            try
            {
                // Delete resource group, which deletes all child resources
                Console.WriteLine("Deleting Resource Group: " + BlazorCrudResourceGroup + "...");
                azure.ResourceGroups.DeleteByName(BlazorCrudResourceGroup);
                Console.WriteLine("Deleted Resource Group: " + BlazorCrudResourceGroup);
            }
            catch (Microsoft.Rest.Azure.CloudException)
            {
                Console.WriteLine("Did not create any resources in Azure. No clean up is necessary");
            }
            catch (Exception g)
            {
                Console.WriteLine(g);
            }
        }
    }
}