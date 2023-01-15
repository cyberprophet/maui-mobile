using ShareInvest.Infrastructure.Http;
using ShareInvest.Models;
using ShareInvest.Models.OpenAPI.Response;

namespace ShareInvest.Services;

public class StockService : CoreHttpClient
{
    public async Task<ObservableStock[]> GetAsync(string order, bool asc = false)
    {
        var res = await TryGetAsync<Stock[]>(0,
                                             string.Concat(nameof(OPTKWFID),
                                                           '?',
                                                           nameof(order),
                                                           '=',
                                                           order,
                                                           '&',
                                                           nameof(asc),
                                                           '=',
                                                           asc));
        return res.Select(o => new ObservableStock(o.Code,
                                                   o.Name,
                                                   o.Current,
                                                   o.Rate,
                                                   o.CompareToPreviousDay,
                                                   o.CompareToPreviousSign,
                                                   o.Volume,
                                                   o.TransactionAmount,
                                                   o.State))
                  .ToArray();
    }
    public StockService() : base(Status.Address)
    {

    }
}