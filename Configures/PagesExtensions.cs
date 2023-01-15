using ShareInvest.Pages;

namespace ShareInvest.Configures;

public static class PagesExtensions
{
    public static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<AccountsPage>()
                        .AddSingleton<StocksPage>()
                        .AddSingleton<MapPage>();

        return builder;
    }
}