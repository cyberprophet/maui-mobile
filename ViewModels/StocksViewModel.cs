using DevExpress.Maui.Scheduler.Internal;

using Microsoft.AspNetCore.SignalR.Client;

using ShareInvest.Infrastructure;
using ShareInvest.Mappers;
using ShareInvest.Models;
using ShareInvest.Observers.OpenAPI;
using ShareInvest.Properties;
using ShareInvest.Services;

using System.Collections.ObjectModel;

namespace ShareInvest.ViewModels;

public class StocksViewModel : ViewModelBase
{
    public ObservableCollection<ObservableStock> StockCollection
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

                foreach (var stock in StockCollection)
                {
                    if (string.IsNullOrEmpty(stock.Code) is false)
                        await hub.RemoveFromGroupAsync(stock.Code);
                }
                await hub.StopAsync();
            }
            StockCollection.Clear();
#if DEBUG
            System.Diagnostics.Debug.WriteLine(hub.State);
#endif
            ChunkCount = 0;
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

                    Stocks = await service.GetAsync(Title);

                    if (HubConnectionState.Disconnected == hub.State)
                    {
                        await hub.StartAsync();
                    }
                    LoadStocks();
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
    public void LoadStocks()
    {
        IsBusy = true;

        var length = (ChunkCount + 1) * chunkSize;

        for (int i = StockCollection.Count; i < length; i++)
        {
            if (i < Stocks?.Length)
            {
                var stock = Stocks[i];

                if (string.IsNullOrEmpty(stock.Code) is false)
                {
                    StockCollection.Add(stock);

                    hub.AddToGroupAsync(stock.Code);
                }
                continue;
            }
            break;
        }
        ChunkCount++;

        IsBusy = false;
    }
    public StocksViewModel(StockService service,
                           IHubService hub,
                           IPropertyService property,
                           IConnectivity connectivity)
    {
        chunkSize = 0x10;
        this.hub = hub;
        this.property = property;
        this.service = service;
        this.connectivity = connectivity;

        (hub as StockHubService).Send += async (sender, e) =>
        {
            if (e is RealMessageEventArgs res)
            {
                var index = Array.FindIndex(Stocks, o => res.Key.Equals(o.Code));

                if (index >= 0 &&
                    StockCollection.TryGetValue(index,
                                                out ObservableStock observe))
                {
                    var resource = res.Data.Split('\t');

                    property.SetValuesOfColumn(observe,
                                               resource.Length switch
                                               {
                                                   7 => new ObservableStock(resource[1],
                                                                            resource[3],
                                                                            resource[2],
                                                                            resource[6],
                                                                            resource[5]),

                                                   _ => new ObservableStock(resource[1],
                                                                            resource[3],
                                                                            resource[2],
                                                                            resource[0xC],
                                                                            resource[7],
                                                                            resource[8])
                                               });
                }
                return;
            }
            await SendToastMessage(sender.GetType().Name);
        };
        cts = new CancellationTokenSource();

        Title = nameof(Models.OpenAPI.Response.OPTKWFID.MarketCap);

        StockCollection = new ObservableCollection<ObservableStock>();
    }
    ObservableStock[] Stocks
    {
        get; set;
    }
    uint ChunkCount
    {
        get; set;
    }
    readonly uint chunkSize;
    readonly StockService service;
    readonly IPropertyService property;
    readonly IHubService hub;
    readonly IConnectivity connectivity;
}