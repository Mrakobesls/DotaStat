namespace CommonSources.Steam;

public abstract class SteamHttpClientBase
{
    protected readonly HttpClient HttpClient;

    protected SteamHttpClientBase(HttpClient httpClient)
    {
        HttpClient = httpClient;

        HttpClient.BaseAddress = new Uri("https://api.steampowered.com");
    }
}
