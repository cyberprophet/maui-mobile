using ShareInvest.Services;
using ShareInvest.ViewModels;

namespace ShareInvest.Pages;

public partial class AccountsPage : ContentPage
{
    public AccountsPage(AccountsViewModel vm)
    {
        InitializeComponent();
                
        PropertyService.SetStatusBarColor();
        
        BindingContext = vm;
    }
    protected override async void OnAppearing()
    {
        await ViewModel.InitializeAsync();

        base.OnAppearing();
    }
    protected override async void OnDisappearing()
    {
        await ViewModel.DisposeAsync();

        base.OnDisappearing();
    }
    AccountsViewModel ViewModel
    {
        get => BindingContext as AccountsViewModel;
    }
}