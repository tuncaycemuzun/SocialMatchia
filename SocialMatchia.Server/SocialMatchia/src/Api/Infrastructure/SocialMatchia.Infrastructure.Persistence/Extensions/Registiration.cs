using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Infrastructure.Persistence.Context;

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

            services.AddTransient(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));

            return services;
        }
    }
}
