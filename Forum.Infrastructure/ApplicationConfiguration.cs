using Forum.Application.Repositories;
using Forum.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Infrastructure
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services, 
            string databaseName)
        {
            services.AddDbContext<DatabaseContext>((options) =>
            {
                options.UseSqlite(databaseName, config =>
                {
                    config.MigrationsHistoryTable("changelog");

                    config.MigrationsAssembly(typeof(DatabaseContext)
                        .Assembly.GetName()
                        .Name);
                });
            });

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
