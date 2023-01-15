using ShareInvest.ViewModels;

namespace ShareInvest.Shells;

public partial class MobileShell : Shell
{
    public MobileShell()
    {
        Application.Current
                   .RequestedThemeChanged += (sender, e) =>
                   {
                       BindingContext = new ShellViewModel(AppTheme.Dark > e.RequestedTheme);
                   };
        InitializeComponent();

        BindingContext = new ShellViewModel();
    }
}