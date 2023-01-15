using CommunityToolkit.Maui;

using DevExpress.Maui;

using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;

using ShareInvest.Configures;

namespace ShareInvest;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseDevExpress()
            .UseMauiCompatibility()
            .UseMauiCommunityToolkit(o =>
            {
                o.SetShouldSuppressExceptionsInConverters(false);
                o.SetShouldSuppressExceptionsInBehaviors(false);
                o.SetShouldSuppressExceptionsInAnimations(false);
            })
            .UseMauiMaps()
            .ConfigureEssentials(o =>
            {
                o.UseVersionTracking();
            })
            .ConfigureServices()
            .ConfigureViewModels()
            .ConfigurePages()
            .ConfigureFonts(o =>
            {
                o.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                o.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                o.AddFont("univia-pro-regular.ttf", "Univia-Pro");
                o.AddFont("roboto-bold.ttf", "Roboto-Bold");
                o.AddFont("roboto-regular.ttf", "Roboto");
            });
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}