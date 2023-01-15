using CommunityToolkit.Mvvm.ComponentModel;

namespace ShareInvest.Models;

public partial class ObservableBalance : ObservableObject
{
    [ObservableProperty]
    int quantity;

    [ObservableProperty]
    int previousQuantity;

    [ObservableProperty]
    int todayQuantity;

    [ObservableProperty]
    int average;

    [ObservableProperty]
    int current;

    [ObservableProperty]
    long valuation;

    [ObservableProperty]
    long amount;

    [ObservableProperty]
    double rate;

    [ObservableProperty]
    long purchase;

    [ObservableProperty]
    int paymentBalance;

    [ObservableProperty]
    Color color;

    [ObservableProperty]
    Color previousColor;

    [ObservableProperty]
    Color todayColor;

    public ObservableBalance(string code,
                             string name,
                             string accNo,
                             string quantity,
                             string average,
                             string current,
                             string valuation,
                             string amount,
                             string rate,
                             string purchase,
                             string paymentBalance,
                             string previousPurchaseQuantity,
                             string previousSalesQuantity,
                             string purchaseQuantity,
                             string salesQuantity)
    {
        ConvertParameter(quantity,
                         average,
                         current,
                         valuation,
                         amount,
                         rate,
                         purchase,
                         paymentBalance,
                         previousPurchaseQuantity,
                         previousSalesQuantity,
                         purchaseQuantity,
                         salesQuantity);

        Code = code;
        Name = name;
        AccNo = accNo;
    }
    public string Code
    {
        get;
    }
    public string Name
    {
        get;
    }
    public string AccNo
    {
        get;
    }
    void ConvertParameter(string quantity,
                          string average,
                          string current,
                          string valuation,
                          string amount,
                          string rate,
                          string purchase,
                          string paymentBalance,
                          string previousPurchaseQuantity,
                          string previousSalesQuantity,
                          string purchaseQuantity,
                          string salesQuantity)
    {
        previousQuantity = Convert.ToInt32(previousPurchaseQuantity) - Convert.ToInt32(previousSalesQuantity);
        todayQuantity = Convert.ToInt32(purchaseQuantity) - Convert.ToInt32(salesQuantity);

        color = rate[0] switch
        {
            '-' => AppTheme.Dark == Application.Current?.RequestedTheme ?

                Colors.DeepSkyBlue : Colors.Blue,

            '+' => Colors.Red,

            _ => AppTheme.Dark == Application.Current?.RequestedTheme ?

                Colors.Snow : Colors.DimGray
        };
        previousColor = previousQuantity switch
        {
            < 0 => AppTheme.Dark == Application.Current?.RequestedTheme ?

                Colors.DeepSkyBlue : Colors.Blue,

            > 0 => Colors.Red,

            _ => AppTheme.Dark == Application.Current?.RequestedTheme ?

                Colors.Snow : Colors.DimGray
        };
        todayColor = todayQuantity switch
        {
            < 0 => AppTheme.Dark == Application.Current?.RequestedTheme ?

                Colors.DeepSkyBlue : Colors.Blue,

            > 0 => Colors.Red,

            _ => AppTheme.Dark == Application.Current?.RequestedTheme ?

                Colors.Snow : Colors.DimGray
        };
        this.quantity = Convert.ToInt32(quantity);
        this.average = Convert.ToInt32(average);
        this.current = Convert.ToInt32(current);
        this.valuation = Convert.ToInt64(valuation);
        this.amount = Convert.ToInt64(amount[1..]);
        this.rate = Convert.ToInt32(rate[1..]) * 1e-6;
        this.purchase = Convert.ToInt64(purchase);
        this.paymentBalance = Convert.ToInt32(paymentBalance);
    }
}