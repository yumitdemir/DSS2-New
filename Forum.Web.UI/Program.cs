using Forum.Web.UI.Clients.Authentication;
using Forum.Web.UI.Clients.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace Forum.Web.UI
{
    public class BackendApi
    {
        public string? Address { get; set; }

        public Uri? CreateUri(string? relativePart)
        {
            if (Address is null || relativePart is null)
            {
                return null;
            }

            var baseUri = new Uri(Address, UriKind.Absolute);
            var relativeUri = new Uri(relativePart, UriKind.Relative);

            return new Uri(baseUri, relativeUri);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<BackendApi>(
                builder.Configuration.GetSection(nameof(BackendApi)));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(setting =>
                {
                    setting.AccessDeniedPath = "/Home/Index";
                    setting.LoginPath = "/Home/Index";
                    setting.Cookie.IsEssential = true;
                    setting.Cookie.HttpOnly = true;
                    setting.SlidingExpiration = true;
                    setting.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                });

            builder.Services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            builder.Services.RegisterClient<IUserClient>("/api/users");
            builder.Services.RegisterClient<IAuthenticationClient>("/api/authentication");

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }

    public static class RefitClientExtensions
    {
        public static IServiceCollection RegisterClient<T>(
            this IServiceCollection services,
            string controllerRoute)
            where T : class
        {
            services.AddRefitClient<T>()
               .ConfigureHttpClient((provider, client) =>
               {
                   var settings = provider
                       .GetRequiredService<IOptions<BackendApi>>();

                   client.BaseAddress = settings.Value.CreateUri(controllerRoute);

                   client.DefaultRequestHeaders.Accept.Clear();
                   client.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue(
                           MediaTypeNames.Application.Json));
               });

            return services;
        }
    }
}