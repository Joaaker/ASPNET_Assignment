using System.Security.Claims;
using Business.Interfaces;
using Business.Services;
using Business.Factories;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Data.Seeders;
using WebApp.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var connectionString = builder.Configuration.GetConnectionString("AzureBlobStorage");
var containerName = "images";
builder.Services.AddScoped<IFileHandler>(_ => new AzureFileHandler(connectionString!, containerName));

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IProjectMemberService, ProjectMemberService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationDismissRepository, NotificationDismissRepository>();
builder.Services.AddScoped<IMemberAddressRepository, MemberAddressRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddIdentity<MemberEntity, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<MemberEntity>, MemberClaimsPrincipalFactory>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/sign-in";
    options.SlidingExpiration = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;

})
.AddGitHub(options =>
{
    options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"]!;
    options.Scope.Add("user:email");
    options.Scope.Add("read:user");

    options.Events.OnCreatingTicket = async context =>
    {
        await Task.Delay(0);
        if (context.User.TryGetProperty("name", out var name))
        {
            var fullName = name.GetString();
            if (!string.IsNullOrEmpty(fullName))
            {
                var names = fullName.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                if (names.Length > 0)
                {
                    context.Identity?.AddClaim(new Claim(ClaimTypes.GivenName, names[0]));
                }
                if (names.Length > 1)
                {
                    context.Identity?.AddClaim(new Claim(ClaimTypes.Surname, names[1]));
                }
            }
        }
    };
});

var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

await DatabaseSeeder.SeedRolesAsync(app.Services);
await DatabaseSeeder.SeedAdminAsync(app.Services);

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=SignUp}")
    .WithStaticAssets();

app.MapHub<NotificationHub>("/notificationHub");

app.Run();