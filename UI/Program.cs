using Domain.Interfaces;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Infrastructure.Services;
using Quartz;
using Serilog;
using UI.BackgroundServices;
using UI.Extensions;
using UI.Features;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddMemoryCache();
builder.Services.AddHxServices();
builder.Services.AddHxMessenger();
builder.Services.AddHxMessageBoxHost();
builder.Services.AddSerilog();
builder.Services.AddWTelegram(configuration);
builder.Services.AddScoped<ITaskSettingService, TaskSettingService>();
builder.Services.AddSingleton<ITelegramAuthorization, TelegramAuthorization>();
builder.Services.AddSingleton<TelegramBackgroundAuthorizationService>();
builder.Services.AddSingleton<DbInitBackgroundService>();
builder.Services.AddHostedService(provider => provider.GetService<TelegramBackgroundAuthorizationService>());
builder.Services.AddHostedService(provider => provider.GetService<DbInitBackgroundService>());
builder.Services.AddScoped<ISchedulerService, SchedulerService>();

builder.Services.AddQuartz(p =>
    p.UsePersistentStore(p =>
    {
        var path = Path.Combine(AppContext.BaseDirectory, "identifier.sqlite");
        p.UseSQLite($"Data Source={path}");
        p.UseSystemTextJsonSerializer();
    }));

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();