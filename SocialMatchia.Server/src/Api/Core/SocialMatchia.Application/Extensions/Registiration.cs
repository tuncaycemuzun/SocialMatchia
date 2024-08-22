using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SocialMatchia.Application.Extensions
{
    public static class Registiration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assm = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assm));
            services.AddValidatorsFromAssembly(assm);

            //services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            //services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            return services;
        }
    }
}
