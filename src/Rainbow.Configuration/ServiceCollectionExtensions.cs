using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Core;
using Rainbow.Core.Entities;
using System;

namespace Rainbow.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, Action<DbContextOptionsBuilder> guildConfigOptionsAction = null)
        {
            services.AddDbContextFactory<GuildConfigurationContext>(guildConfigOptionsAction);
            services.AddSingleton<IDatabase<GuildConfiguration>, GuildConfigurationDatabase>();
            return services;
        }
    }
}