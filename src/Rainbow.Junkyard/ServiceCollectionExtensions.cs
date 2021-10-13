using Microsoft.Extensions.DependencyInjection;
using Rainbow.Entities;

namespace Rainbow.Junkyard
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJunkyard(this IServiceCollection services, string repositoryUrl)
        {
            services.AddSingleton<IRepository<FlaggedUser>>(new JunkyardRepo(repositoryUrl));
            return services;
        }
    }
}