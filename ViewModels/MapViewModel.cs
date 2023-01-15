using ShareInvest.Properties;

namespace ShareInvest.ViewModels;

public partial class MapViewModel : ViewModelBase
{
    public Location Location
    {
        get; private set;
    }
    public MapViewModel(IConnectivity connectivity)
    {
        this.connectivity = connectivity;
    }
    public async override Task DisposeAsync()
    {
        cts?.Cancel();

        await Task.CompletedTask;
    }
    public async override Task InitializeAsync()
    {
        if (IsNotBusy)
            try
            {
                if (NetworkAccess.Internet == connectivity.NetworkAccess)
                {
                    IsBusy = true;

                    Location = await Geolocation.Default.GetLastKnownLocationAsync();
                }
                else
                {
                    Title = nameof(connectivity.NetworkAccess);

                    await DisplayAlert(Resources.NETWORKACCESS);
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                Title = nameof(FeatureNotSupportedException);

                await DisplayAlert(ex.Message);
            }
            catch (FeatureNotEnabledException ex)
            {
                Title = nameof(FeatureNotEnabledException);

                await DisplayAlert(ex.Message);
            }
            catch (PermissionException ex)
            {
                Title = nameof(PermissionException);

                await DisplayAlert(ex.Message);
            }
            catch (Exception ex)
            {
                Title = nameof(Exception);
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
                await GetCurrentLocation();
            }
            finally
            {
                IsBusy = false;
            }
    }
    async Task GetCurrentLocation()
    {
        GeolocationRequest request = new(GeolocationAccuracy.Best,
                                         TimeSpan.FromSeconds(0xA));

        Location = await Geolocation.Default.GetLocationAsync(request,
                                                              cts.Token);
    }
    readonly IConnectivity connectivity;
}