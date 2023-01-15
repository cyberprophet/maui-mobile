using ShareInvest.ViewModels;

namespace ShareInvest.Configures;

public static class ViewModelsExtensions
{
    public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<AccountsViewModel>()
                        .AddSingleton<StocksViewModel>()
                        .AddSingleton<MapViewModel>();

        return builder;
    }
}