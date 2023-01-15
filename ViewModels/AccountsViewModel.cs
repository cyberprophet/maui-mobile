using DevExpress.Maui.Scheduler.Internal;

using Microsoft.AspNetCore.SignalR.Client;

using ShareInvest.Infrastructure;
using ShareInvest.Mappers;
using ShareInvest.Models;
using ShareInvest.Observers.Socket;
using ShareInvest.Properties;
using ShareInvest.Services;

using System.Collections.ObjectModel;

namespace ShareInvest.ViewModels;

public class AccountsViewModel : ViewModelBase
{
    public ObservableCollection<ObservableAccount> AccountCollection
    {
        get;
    }
    public override async Task DisposeAsync()
    {
        if (NetworkAccess.Internet == connectivity.NetworkAccess)
        {
            if (HubConnectionState.Connected == hub.State)
            {
                IsBusy = true;

                while (groupNames.TryPop(out string groupName))
                {
                    await hub.RemoveFromGroupAsync(groupName);
                }
                OnAssetStatus?.Dispose();
                OnGroup?.Dispose();

                await hub.StopAsync();
            }
            accounts.Clear();
            AccountCollection.Clear();
        }
        IsBusy = false;
    }
    public override async Task InitializeAsync()
    {
        if (IsNotBusy)
            try
            {
                if (NetworkAccess.Internet == connectivity.NetworkAccess)
                {
                    IsBusy = true;

                    var value = await SecureStorage.Default.GetAsync(Resources.AUTH);

                    if (HubConnectionState.Disconnected == hub.State)
                    {
                        await hub.StartAsync();
                    }
                    if (string.IsNullOrEmpty(value))
                    {
                        await SecureStorage.Default.SetAsync(Resources.AUTH,
                                                             Resources.KAKAO);
                    }
                    await foreach (var acc in login.AuthenticateAsync(value))
                    {
                        if (hub is StockHubService sh)
                        {
                            await sh.InstructToRenewAssetStatus(acc.AccNo);
                        }
                        AccountCollection.Add(acc);
                    }
                    if (AccountCollection.Count == 0)
                    {

                    }
                }
                else
                {
                    Title = nameof(connectivity.NetworkAccess);

                    await DisplayAlert(Resources.NETWORKACCESS);
                }
            }
            catch (Exception ex)
            {
                Title = nameof(Exception);

                await DisplayAlert(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
    }
    public AccountsViewModel(IConnectivity connectivity,
                             ILoginService<ObservableAccount> login,
                             IHubService hub,
                             IPropertyService property)
    {
        this.connectivity = connectivity;
        this.property = property;
        this.login = login;
        this.hub = hub;

        var sh = hub as StockHubService;

        sh.Send += async (sender, e) =>
        {
            switch (e)
            {
                case InstructEventArgs asset:

                    var index = AccountCollection.FindIndex(o => asset.AccNo.Equals(o.AccNo) &&
                                                                 asset.Date.Equals(o.Date));

                    switch (asset.Convey)
                    {
                        case Account acc when accounts.Add(acc) is false:

                            var account = accounts.First(o => o.AccNo.Equals(asset.AccNo));

                            if (string.IsNullOrEmpty(acc.OrderableCash))
                            {
                                account.NumberOfPrints = acc.NumberOfPrints;
                                account.Balance = acc.Balance;
                                account.Asset = acc.Asset;
                                account.PresumeAsset = acc.PresumeAsset;
                                account.Deposit = acc.Deposit;
                                account.PresumeDeposit = acc.PresumeDeposit;
                                account.TotalPurchaseAmount = acc.TotalPurchaseAmount;
                            }
                            else
                            {
                                if (AccountCollection.TryGetValue(index, out ObservableAccount oa))
                                {
                                    property.SetValuesOfColumn(oa,
                                                               new ObservableAccount(account.AccNo,
                                                                                     account.Date,
                                                                                     account.NumberOfPrints,
                                                                                     account.Balance,
                                                                                     account.Asset,
                                                                                     account.PresumeAsset,
                                                                                     account.Deposit,
                                                                                     account.PresumeDeposit,
                                                                                     account.TotalPurchaseAmount,
                                                                                     acc.OrderableCash));
                                }
                                account.OrderableCash = acc.OrderableCash;
                            }
                            return;
                    }
                    return;

                case GroupEventArgs group:
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(group.Name);
#endif
                    groupNames.Push(group.Name);
                    return;
            }
            await SendToastMessage(sender.GetType().Name);
        };
        OnAssetStatus = sh.On(nameof(IHubs.UpdateTheStatusOfAssets));
        OnGroup = sh.On(nameof(IHubs.AddToGroupAsync));

        AccountCollection = new ObservableCollection<ObservableAccount>();
    }
    IDisposable OnGroup
    {
        get; set;
    }
    IDisposable OnAssetStatus
    {
        get; set;
    }
    readonly HashSet<Account> accounts = new();
    readonly Stack<string> groupNames = new();
    readonly IHubService hub;
    readonly IConnectivity connectivity;
    readonly IPropertyService property;
    readonly ILoginService<ObservableAccount> login;
}