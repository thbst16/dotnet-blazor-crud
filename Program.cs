using BlazorCrud.Components;
using BlazorCrud.Data;
using BlazorCrud.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// In-memory data persistence of people
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("People"));
builder.Services.AddScoped<IPersonService, PersonService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    try
    {
        var appDbContext = services.GetRequiredService<ApplicationDbContext>();
        DataGenerator.Initialize(appDbContext);
    }
    catch
    {
        Console.WriteLine("An error occured creating the database");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();