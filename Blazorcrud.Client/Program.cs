using Blazorcrud.Client;
using Blazorcrud.Client.Services;
using Blazorcrud.Client.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped(x => {
    var apiUrl = new Uri("http://localhost:5001");
    return new HttpClient() {BaseAddress = apiUrl};
});
builder.Services.AddSingleton<PageHistoryState>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();