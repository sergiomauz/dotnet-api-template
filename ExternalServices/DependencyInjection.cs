using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ExternalServices
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Initialize all the External services
        /// </summary>
        /// <param name="services">Contract collection of service descriptor</param>
        /// <param name="configuration">App configuration</param>
        /// <returns>Contract collection of service descriptor</returns>
        /// 
        public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
        {

            //
            return services;
        }
    }
}
