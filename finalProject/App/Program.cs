
using App.Data;
using App.Data.Repositories;
using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRaceRepository, EFRaceRepository>();
builder.Services.AddScoped<IRaceResultRepository, EFRaceResultRepository>();
builder.Services.AddScoped<IPilotRepository, EFPilotRepository>();
builder.Services.AddScoped<ICarRepository, EFCarRepository>();
// builder.Services.AddScoped<IRepository<Race>, EFRaceRepository>();
// builder.Services.AddScoped<IRepository<Pilot>, EFPilotRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.LoginPath = "/Pilots/Login/";
    });

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
};



var connectionString = "server=localhost;port=9000;user=root;password=example;database=app_db";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

builder.Services.AddDbContext<AppDbContext>(
    dbContextOptions => dbContextOptions
        .UseLazyLoadingProxies()
        .UseMySql(connectionString, serverVersion)
        .LogTo(Console.Write, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy(cookiePolicyOptions);
app.Use(async (context, next) => {
    Console.WriteLine("MW 1 ===>");
    await next();
    Console.WriteLine("<=== MW 1");
});
app.Use(async (context, next) => {
    Console.WriteLine("MW 2 ===>");
    await next();
    Console.WriteLine("<=== MW 2");
});

app.MapControllerRoute(
    name: "raceList",
    defaults: new { controller = "Races", action = "List" },
    pattern: "/races");
app.MapControllerRoute(
    name: "register",
    defaults: new { controller = "Pilotes", action = "Register" },
    pattern: "/pilots");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreated();
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Seed();
    
    // TODO SEED DATA
}
app.Run();