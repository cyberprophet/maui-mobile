using ShareInvest.Models;
using ShareInvest.Pages;

namespace ShareInvest.ViewModels;

public class ShellViewModel
{
    public ShellViewModel()
    {
        unspecified = AppTheme.Dark > Application.Current?.PlatformAppTheme;

        Create();
    }
    public ShellViewModel(bool unspecified)
    {
        this.unspecified = unspecified;

        Create();
    }
    public AppSection Account
    {
        get; set;
    }
    public AppSection Stock
    {
        get; set;
    }
    public AppSection Map
    {
        get; set;
    }
    void Create()
    {
        Map = new AppSection
        {
            TargetType = typeof(MapPage),
            Title = Properties.Resources.MAP,
            Icon = unspecified ? "share_dark.svg" :
                                 "share.svg"
        };
        Account = new AppSection
        {
            TargetType = typeof(AccountsPage),
            Title = Properties.Resources.ASSETS,
            Icon = unspecified ? "clock_dark.svg" :
                                 "clock.svg"
        };
        Stock = new AppSection
        {
            TargetType = typeof(StocksPage),
            Title = Properties.Resources.STOCK,
            Icon = unspecified ? "home_dark.svg" :
                                 "home.svg"
        };
    }
    readonly bool unspecified;
}