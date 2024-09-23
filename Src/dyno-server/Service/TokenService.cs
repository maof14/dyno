using Common;
using ViewModels;

namespace dyno_server.Service;

public class TokenService : ITokenService
{
    private HttpClient _httpClient;

    public TokenService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("TokenClient");
    }

    public string Token { get; private set; }

    public Task<bool> GetRegisteringAvailableStatus()
    {
        throw new NotImplementedException();
    }

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

    public Task<bool> RegisterAsync(string username, string password, string passwordRepeat)
    {
        throw new NotImplementedException();
    }

    public void ResetToken()
    {
        Token = string.Empty;
    }
}
