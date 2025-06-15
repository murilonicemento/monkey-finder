using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    private MonkeyService _monkeyService;
    private IConnectivity _connectivity;
    private readonly IGeolocation _geolocation;
    public ObservableCollection<Monkey> Monkeys { get; } = [];

    public MonkeysViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
    {
        _monkeyService = monkeyService;
        _connectivity = connectivity;
        _geolocation = geolocation;
        Title = "Monkey Finder";
    }

    [RelayCommand]
    private async Task GetClosestMonkeyAsync()
    {
        if (IsBusy || Monkeys.Count == 0)
            return;

        try
        {

        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);

            await Shell.Current.DisplayAlert("Error!", $"Unable to get closest monkey: {exception.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task GoToDetailsAsync(Monkey monkey)
    {
        if (monkey == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?id={monkey.Name}", true,
            new Dictionary<string, object> { { "Monkey", monkey } });
    }

    [RelayCommand]
    private async Task GetMonkeysAsync()
    {
        if (IsBusy)
            return;

        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet issue!", $"Check your internet and try again.", "OK");

                return;
            }

            IsBusy = true;

            var monkeys = await _monkeyService.GetMonkeys();

            if (Monkeys.Count != 0)
                monkeys.Clear();

            foreach (var monkey in monkeys)
                Monkeys.Add(monkey);
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);

            await Shell.Current.DisplayAlert("Error!", $"Unable to get monkeys: {exception.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}