using AspNetCoreHealth.Models;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddHealthChecks();

builder.Services.Configure<HealthCheckPublisherOptions>(options =>
{
    options.Delay = TimeSpan.FromSeconds(20);       // Initial delay
    options.Period = TimeSpan.FromSeconds(100);     // Run every 100s
    options.Predicate = healthCheck => healthCheck.Tags.Contains("sample"); // run a subset of health checks i.e. here tagged with sample
});

builder.Services.AddSingleton<IHealthCheckPublisher, SampleHealthCheckPublisher>();

builder.Services.AddHealthChecks()
    .AddCheck<MyHealthCheckStartup>(
        "Startup",
        tags: new[] { "sp_tag" });

builder.Services.AddHealthChecks()
    .AddCheck<MyHealthCheckLiveness>(
        "Liveness",
        tags: new[] { "lp_tag" });

builder.Services.AddHealthChecks()
    .AddCheck<MyHealthCheckReadiness>(
        "Readiness",
        tags: new[] { "rp_tag" });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.MapHealthChecks("/healthz");

app.MapHealthChecks("/healthz/SP", new HealthCheckOptions()
{
    Predicate = healthCheck => healthCheck.Tags.Contains("sp_tag")
});
app.MapHealthChecks("/healthz/LP", new HealthCheckOptions()
{
    Predicate = healthCheck => healthCheck.Tags.Contains("lp_tag")
});
app.MapHealthChecks("/healthz/RP", new HealthCheckOptions()
{
    Predicate = healthCheck  => healthCheck.Tags.Contains("rp_tag")
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
