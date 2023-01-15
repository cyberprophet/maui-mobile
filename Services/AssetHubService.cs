using Microsoft.AspNetCore.SignalR.Client;

using ShareInvest.Infrastructure;
using ShareInvest.Infrastructure.Socket;
using ShareInvest.Properties;

namespace ShareInvest.Services;

public class AssetHubService : CoreSignalR, IHubService
{
    public AssetHubService() : base(string.Concat(Status.Address,
                                                  Resources.KIWOOM))
    {

    }
    public HubConnectionState State
    {
        get;
    }
    public Task AddToGroupAsync(string code)
    {
        throw new NotImplementedException();
    }
    public Task RemoveFromGroupAsync(string code)
    {
        throw new NotImplementedException();
    }
    public Task StartAsync()
    {
        throw new NotImplementedException();
    }
    public Task StopAsync()
    {
        throw new NotImplementedException();
    }
}