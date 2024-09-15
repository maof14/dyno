using Common;
using System.Net.Http.Json;

namespace Service;

public class TokenService : ITokenService
{
    private HttpClient _httpClient;

    public TokenService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("TokenClient");
    }

    public string Token { get; }

    public async Task GetTokenAsync(string username, string password)
    {
        var token = await _httpClient.PostAsJsonAsync(Routes.Auth, new
        {
            Username = username,
            Password = password
        });
    }
}
