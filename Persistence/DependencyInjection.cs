using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Infrastructure.Persistence;


namespace Persistence
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Initialize all the Persistence services
        /// </summary>
        /// <param name="services">Contract collection of service descriptor</param>
        /// <param name="configuration">App configuration</param>
        /// <returns>Contract collection of service descriptor</returns>
        /// 
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            // Add db context
            services.AddDbContext<SqlServerDbContext>(options => options.UseSqlServer(connectionStrings.DefaultConnection));

            // Add db transactions
            services.AddScoped<IDatabaseTransaction, DatabaseTransaction>();

            //
            return services;
        }
    }
}
