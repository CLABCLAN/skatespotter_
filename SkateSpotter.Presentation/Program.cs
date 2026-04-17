using SkateSpotter.Data.Interfaces;
using SkateSpotter.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddScoped<IReviewRepository>(sp =>
    new ReviewRepository(connectionString));

builder.Services.AddScoped<ISpotRepository>(sp =>
    new SpotRepository(connectionString));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Spot}/{action=Index}/{id?}");

app.Run();
