using ShareInvest.Infrastructure.Http;
using ShareInvest.Mappers;
using ShareInvest.Models;
using ShareInvest.Properties;

namespace ShareInvest.Services;

public class ExternalLoginService : CoreHttpClient,
                                    ILoginService<ObservableAccount>
{
    public string ProviderKey
    {
        get; private set;
    }
    public string LoginProvider
    {
        get; private set;
    }
    public string AccessToken
    {
        get; private set;
    }
    public string RefreshToken
    {
        get; private set;
    }
    public string IdToken
    {
        get; private set;
    }
    public async IAsyncEnumerable<ObservableAccount> AuthenticateAsync(string scheme)
    {
        var result = await WebAuthenticator.Default.AuthenticateAsync(new WebAuthenticatorOptions
        {
            CallbackUrl = new Uri("app://"),

            PrefersEphemeralWebBrowserSession = false,

            Url = new Uri(Path.Combine(Status.Address,
                                       Resources.AUTH,
                                       scheme ?? Resources.KAKAO))
        });
#if DEBUG
        Status.GetProperites(result);
#endif
        IdToken = result.IdToken;
        RefreshToken = result.RefreshToken;
        AccessToken = result.AccessToken;
        LoginProvider = scheme;
        ProviderKey = result.Properties.Single(o => o.Key.Equals(nameof(ProviderKey),
                                                                 StringComparison.OrdinalIgnoreCase))
                                       .Value;

        foreach (var kv in from o in result.Properties
                           where o.Key.StartsWith(nameof(IntegrationAccount.AccountNumber))
                           select o)

            yield return await GetAccountAsync(kv.Value);
    }
    public ExternalLoginService() : base(Status.Address)
    {

    }
    async Task<ObservableAccount> GetAccountAsync(string acc)
    {
        var res = await TryGetAsync<Account>(1,
                                             string.Concat(nameof(Account),
                                                           '?',
                                                           nameof(acc),
                                                           '=',
                                                           acc));
#if DEBUG
        Status.GetProperites(res);
#endif
        var asset = new ObservableAccount(res.AccNo,
                                          res.Date,
                                          res.NumberOfPrints,
                                          res.Balance,
                                          res.Asset,
                                          res.PresumeAsset,
                                          res.Deposit,
                                          res.PresumeDeposit,
                                          res.TotalPurchaseAmount,
                                          res.OrderableCash);

        for (int i = 0; i < asset.Balances.Length; i++)
        {
            asset.Balances[i] = new ObservableBalance(res.Balances[i].Code,
                                                      res.Balances[i].Name,
                                                      res.AccNo,
                                                      res.Balances[i].Quantity,
                                                      res.Balances[i].Average,
                                                      res.Balances[i].Current,
                                                      res.Balances[i].Valuation,
                                                      res.Balances[i].Amount,
                                                      res.Balances[i].Rate,
                                                      res.Balances[i].Purchase,
                                                      res.Balances[i].PaymentBalance,
                                                      res.Balances[i].PreviousPurchaseQuantity,
                                                      res.Balances[i].PreviousSalesQuantity,
                                                      res.Balances[i].PurchaseQuantity,
                                                      res.Balances[i].SalesQuantity);
        }
        return asset;
    }
}