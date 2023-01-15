using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;

using ShareInvest.Properties;

namespace ShareInvest.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    public bool IsNotBusy => isBusy is false;

    public abstract Task InitializeAsync();

    public abstract Task DisposeAsync();

    protected CancellationTokenSource cts;

    protected async Task DisplayAlert(string message)
    {
        await Shell.Current.DisplayAlert(title,
                                         message,
                                         Resources.OK);
    }
    protected async Task SendToastMessage(string message)
    {
        if (IsNotBusy)
            await Toast.Make(message,
                             ToastDuration.Short)
                       .Show(cts.Token);
    }
    [ObservableProperty]
    string title;

    [ObservableProperty,
     NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
}