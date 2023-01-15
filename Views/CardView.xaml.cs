namespace ShareInvest.Views;

public partial class CardView : ContentView
{
    public CardView()
    {
        InitializeComponent();
    }
    public string CardTitle
    {
        get => (string)GetValue(cardTitleProperty);

        set => SetValue(cardTitleProperty, value);
    }
    static readonly BindableProperty cardTitleProperty

        = BindableProperty.Create(nameof(CardTitle),
                                  typeof(string),
                                  typeof(CardView),
                                  string.Empty);
}