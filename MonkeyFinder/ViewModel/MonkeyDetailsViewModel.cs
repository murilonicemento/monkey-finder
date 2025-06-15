namespace MonkeyFinder.ViewModel;

[QueryProperty(nameof(Monkey), "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    [ObservableProperty] private Monkey monkey;
    private readonly IMap _map;

    public MonkeyDetailsViewModel(IMap map)
    {
        _map = map;
    }

    [RelayCommand]
    private async Task OpenMapAsync()
    {
        try
        {
            await _map.OpenAsync(Monkey.Latitude, Monkey.Longitude, new MapLaunchOptions
            {
                Name = Monkey.Name,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);

            await Shell.Current.DisplayAlert("Error!", $"Unable to open map: {exception.Message}", "OK");
        }
    }

    // [RelayCommand]
    // private async Task GoBackAsync()
    // {
    //     await Shell.Current.GoToAsync("..");
    // }
}