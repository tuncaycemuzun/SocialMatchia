using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMatchia.Application.Interfaces.Repositories;
using SocialMatchia.Infrastructure.Persistence.Context;
using SocialMatchia.Infrastructure.Persistence.Repositories;

namespace SocialMatchia.Infrastructure.Persistence.Extensions
{
    public static class Registiration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SocialMatchiaDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
