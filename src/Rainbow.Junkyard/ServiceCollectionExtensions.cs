using Microsoft.Extensions.DependencyInjection;
using Rainbow.Entities;
using Rainbow.Junkyard.Internal;

namespace Rainbow.Junkyard
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJunkyard(this IServiceCollection services, string repositoryUrl, string username, string password)
        {
            services.AddSingleton<IRepository<FlaggedUser>>(new JunkyardRepo(repositoryUrl, username, password));
            return services;
        }
    }
}