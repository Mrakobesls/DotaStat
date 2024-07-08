namespace CommonSources.OpenDota;

public abstract class OpenDotaHttpClientBase
{
    protected readonly HttpClient HttpClient;

    protected OpenDotaHttpClientBase(HttpClient httpClient)
    {
        HttpClient = httpClient;

        HttpClient.BaseAddress = new Uri("https://api.opendota.com/api");
    }
}
