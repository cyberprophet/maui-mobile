using Microsoft.Extensions.DependencyInjection.Extensions;

using ShareInvest.Infrastructure;
using ShareInvest.Mappers;
using ShareInvest.Models;
using ShareInvest.Services;

namespace ShareInvest.Configures;

public static class ServicesExtensions
{
    public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton(Connectivity.Current)
                        .AddSingleton<StockService>();

        builder.Services.TryAddTransient<IPropertyService, PropertyService>();
        builder.Services.TryAddTransient<IHubService, StockHubService>();
        builder.Services.TryAddTransient<ILoginService<ObservableAccount>, ExternalLoginService>();

        return builder;
    }
}