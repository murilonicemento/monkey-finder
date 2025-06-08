using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    private HttpClient _httpClient;
    private List<Monkey> monkeys = new();

    public MonkeyService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<Monkey>> GetMonkeys()
    {
        if (monkeys?.Count > 0)
            return monkeys;

        const string url = "https://montemagno.com/monkeys.json";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            monkeys = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        }

        return monkeys;
    }
}