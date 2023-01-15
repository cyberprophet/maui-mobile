using Microsoft.Maui.Maps;

using ShareInvest.Services;
using ShareInvest.ViewModels;

namespace ShareInvest.Pages;

public partial class MapPage : ContentPage
{
    public MapPage(MapViewModel vm)
    {
        InitializeComponent();

        PropertyService.SetStatusBarColor();

        BindingContext = vm;
    }
    protected override async void OnAppearing()
    {
        await ViewModel.InitializeAsync();

        base.OnAppearing();

        map.MoveToRegion(new MapSpan(ViewModel.Location, 1e-2, 1e-2));
    }
    protected override async void OnDisappearing()
    {
        await ViewModel.DisposeAsync();

        base.OnDisappearing();
    }
    MapViewModel ViewModel
    {
        get => BindingContext as MapViewModel;
    }
    /*
    void AddMapStyle()
    {
        var stream = new MemoryStream(Properties.Resources.SILVER_MAP_STYLE);

        string json;

        using (var reader = new StreamReader(stream))
        {
            json = reader.ReadToEnd();
        }
        map.MapStyle = MapStyle.FromJson(json);
    }
    */
}