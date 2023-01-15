using CommunityToolkit.Mvvm.ComponentModel;

namespace ShareInvest.Models;

public partial class ObservableStock : ObservableObject
{
    [ObservableProperty]
    int current;

    [ObservableProperty]
    int compareToPreviousDay;

    [ObservableProperty]
    double rate;

    [ObservableProperty]
    ulong volume;

    [ObservableProperty]
    ulong transactionAmount;

    [ObservableProperty]
    string compareToPreviousSign;

    [ObservableProperty]
    char sign;

    [ObservableProperty]
    Color color;

    [ObservableProperty]
    FontAttributes attributes;

    public ObservableStock(string code,
                           string name,
                           string current,
                           string rate,
                           string compareToPreviousDay,
                           string compareToPreviousSign,
                           string volume,
                           string transactionAmount,
                           string state)
    {
        ConvertParameter(current,
                         rate,
                         compareToPreviousDay,
                         compareToPreviousSign,
                         volume);

        this.transactionAmount = Convert.ToUInt64(transactionAmount);

        Code = code;
        Name = name;
        State = state.Replace('|', ' ');
    }
    public ObservableStock(string current,
                           string rate,
                           string compareToPreviousDay,
                           string compareToPreviousSign,
                           string volume)
    {
        ConvertParameter(current,
                         rate,
                         compareToPreviousDay,
                         compareToPreviousSign,
                         volume);
    }
    public ObservableStock(string current,
                           string rate,
                           string compareToPreviousDay,
                           string compareToPreviousSign,
                           string volume,
                           string transactionAmount)
    {
        ConvertParameter(current,
                         rate,
                         compareToPreviousDay,
                         compareToPreviousSign,
                         volume);

        this.transactionAmount = Convert.ToUInt64(transactionAmount);
    }
    public string Code
    {
        get;
    }
    public string Name
    {
        get;
    }
    public string State
    {
        get;
    }
    void ConvertParameter(string current,
                          string rate,
                          string compareToPreviousDay,
                          string compareToPreviousSign,
                          string volume)
    {
        switch (current[0])
        {
            case '-':
                color = AppTheme.Dark == Application.Current?.RequestedTheme ? Colors.DeepSkyBlue :
                                                                               Colors.Blue;
                sign = '▼';
                attributes = "4".Equals(compareToPreviousSign) ? FontAttributes.Bold :
                                                                 FontAttributes.None;
                break;

            case '+':
                attributes = "1".Equals(compareToPreviousSign) ? FontAttributes.Bold :
                                                                 FontAttributes.None;
                color = Colors.Red;
                sign = '▲';
                break;

            default:
                color = AppTheme.Dark == Application.Current?.RequestedTheme ? Colors.Snow :
                                                                               Colors.DimGray;
                sign = ' ';
                break;
        }
        this.volume = Convert.ToUInt64(volume);
        this.rate = Math.Abs(Convert.ToDouble(rate) * 1e-2);
        this.current = Math.Abs(Convert.ToInt32(current));
        this.compareToPreviousDay = Math.Abs(Convert.ToInt32(compareToPreviousDay));
        this.compareToPreviousSign = compareToPreviousSign;
    }
}