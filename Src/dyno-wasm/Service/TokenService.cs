using Common;
using System.Net.Http.Json;
using System.Text.Json;
using ViewModels;

namespace Service;

public class TokenService : ITokenService
{
    private HttpClient _httpClient;

    public TokenService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("TokenClient");
    }

    public string Token { get; private set; }

    public async Task GetTokenAsync(string username, string password)
    {
        var token = await _httpClient.PostAsJsonAsync($"{Routes.Auth}/{Routes.Login}", new
        {
            Username = username,
            Password = password
        });

        var body = await token.Content.ReadFromJsonAsync<TokenModel>();

        Token = body.Token;
    }

    public void ResetToken()
    {
        Token = string.Empty;
    }
}
