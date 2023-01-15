using CommunityToolkit.Mvvm.ComponentModel;

namespace ShareInvest.Models;

public partial class ObservableAccount : ObservableObject
{
    [ObservableProperty]
    int numberOfPrints;

    [ObservableProperty]
    long balance;

    [ObservableProperty]
    long asset;

    [ObservableProperty]
    long presumeAsset;

    [ObservableProperty]
    long deposit;

    [ObservableProperty]
    long presumeDeposit;

    [ObservableProperty]
    long totalPurchaseAmount;

    [ObservableProperty]
    long orderableCash;

    public ObservableAccount(string accNo,
                             string date,
                             string numberOfPrints,
                             string balance,
                             string asset,
                             string presumeAsset,
                             string deposit,
                             string presumeDeposit,
                             string totalPurchaseAmount,
                             string orderableCash)
    {
        ConvertParameter(numberOfPrints,
                         balance,
                         asset,
                         presumeAsset,
                         deposit,
                         presumeDeposit,
                         totalPurchaseAmount);

        Account = string.Concat(accNo[..4],
                                '-',
                                accNo[4..^2]);
        AccNo = accNo;
        Date = date;
        Balances = new ObservableBalance[this.numberOfPrints];

        this.orderableCash = Convert.ToInt64(orderableCash);
    }
    public ObservableAccount(string numberOfPrints,
                             string balance,
                             string asset,
                             string presumeAsset,
                             string deposit,
                             string presumeDeposit,
                             string totalPurchaseAmount,
                             string orderableCash)
    {
        ConvertParameter(numberOfPrints,
                         balance,
                         asset,
                         presumeAsset,
                         deposit,
                         presumeDeposit,
                         totalPurchaseAmount);

        this.orderableCash = Convert.ToInt64(orderableCash);
    }
    public ObservableAccount(string orderableCash)
    {
        this.orderableCash = Convert.ToInt64(orderableCash);
    }
    public ObservableBalance[] Balances
    {
        get;
    }
    public string Date
    {
        get;
    }
    public string AccNo
    {
        get;
    }
    public string Account
    {
        get;
    }
    void ConvertParameter(string numberOfPrints,
                          string balance,
                          string asset,
                          string presumeAsset,
                          string deposit,
                          string presumeDeposit,
                          string totalPurchaseAmount)
    {
        this.numberOfPrints = Convert.ToInt32(numberOfPrints);
        this.balance = Convert.ToInt64(balance);
        this.asset = Convert.ToInt64(asset);
        this.presumeAsset = Convert.ToInt64(presumeAsset);
        this.deposit = Convert.ToInt64(deposit);
        this.presumeDeposit = Convert.ToInt64(presumeDeposit);
        this.totalPurchaseAmount = Convert.ToInt64(totalPurchaseAmount);
    }
}