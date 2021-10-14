using Microsoft.Extensions.DependencyInjection;
using Rainbow.Core;
using Rainbow.Core.Entities;
using Rainbow.Junkyard.Internal;

namespace Rainbow.Junkyard
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJunkyard(this IServiceCollection services, string repositoryUrl, string username, string password)
        {
            services.AddSingleton<IDatabase<FlaggedUser>>(new JunkyardRepo(repositoryUrl, username, password));
            return services;
        }
    }
}