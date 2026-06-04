using Microsoft.AspNetCore.Authentication.Cookies;
using StayPilot.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "StayPilot.Admin.Auth";
        options.LoginPath = "/Admin/Auth/Login";
        options.LogoutPath = "/Admin/Auth/Logout";
        options.AccessDeniedPath = "/Admin/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpClient("StayPilotApi", client =>
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];

    if (string.IsNullOrWhiteSpace(baseUrl))
    {
        throw new InvalidOperationException("API base URL is not configured.");
    }

    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped<IHotelSearchApiService, HotelSearchApiService>();
builder.Services.AddScoped<IHomeContentApiService, HomeContentApiService>();
builder.Services.AddScoped<IContactMessageApiService, ContactMessageApiService>();
builder.Services.AddScoped<IAdminDashboardApiService, AdminDashboardApiService>();
builder.Services.AddScoped<IAdminContactMessageApiService, AdminContactMessageApiService>();
builder.Services.AddScoped<IAdminHeroSectionApiService, AdminHeroSectionApiService>();
builder.Services.AddScoped<IAdminDestinationApiService, AdminDestinationApiService>();
builder.Services.AddScoped<IAdminBlogPostApiService, AdminBlogPostApiService>();
builder.Services.AddScoped<IAdminApiHealthService, AdminApiHealthService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();