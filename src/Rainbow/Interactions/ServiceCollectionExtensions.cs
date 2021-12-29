using Microsoft.Extensions.DependencyInjection;

namespace Rainbow.Interactions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlipHandlers(this IServiceCollection sc)
    {
        sc.AddSingleton<RevokeFlagBlipHandler>();
        return sc;
    }
}