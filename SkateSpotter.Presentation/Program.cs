using Microsoft.AspNetCore.Authentication.Cookies;
using SkateSpotter.Data.Repositories;
using SkateSpotter.Logic.Interfaces;
using SkateSpotter.Logic.Services;

try
{
    var builder = WebApplication.CreateBuilder(args);

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

    // Data
    builder.Services.AddScoped<ISpotRepository>(_ => new SpotRepository(connectionString));
    builder.Services.AddScoped<IReviewRepository>(_ => new ReviewRepository(connectionString));
    builder.Services.AddScoped<IUserRepository>(_ => new UserRepository(connectionString));

    // Logic
    builder.Services.AddScoped<ISpotService, SpotService>();
    builder.Services.AddScoped<IReviewService, ReviewService>();

    builder.Services.AddControllersWithViews();

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
        });

    // Data
    builder.Services.AddScoped<IUserRepository>(_ => new UserRepository(connectionString));

    // Logic
    builder.Services.AddScoped<IUserService, UserService>();
    var app = builder.Build();

    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Spot}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"FOUT: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
    Console.ReadKey();
}