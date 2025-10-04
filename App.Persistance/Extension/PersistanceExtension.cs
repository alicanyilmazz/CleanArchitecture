using App.Application.Services;
using App.Persistance.Context;
using App.Persistance.Context.Option;
using App.Persistance.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Persistance.Extension;

public static class PersistanceExtension
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService,ProductService>();
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
            options.UseSqlServer(connectionString!.SqlServer, sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                sqlServerOptionsAction.EnableRetryOnFailure();
            });
        });
        return services;
    }
}
