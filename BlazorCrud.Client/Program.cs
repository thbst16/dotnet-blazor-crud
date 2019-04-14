using BlazorCrud.Client.Services;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Sotsera.Blazor.Toaster.Core.Models;
using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorCrud.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(configure =>
            {
                configure.Add(ServiceDescriptor.Singleton<IModelValidator, ModelValidator>());
                configure.AddStorage();
                configure.AddToaster(config =>
                {
                    config.PositionClass = Defaults.Classes.Position.TopRight;
                    config.PreventDuplicates = true;
                    config.NewestOnTop = false;
                });
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();
    }
}
