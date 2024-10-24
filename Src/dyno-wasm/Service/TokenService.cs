using Common;
using System.Net.Http.Json;
using ViewModels;

namespace Service;

public interface IClientTokenService : ITokenService
{
    Task<bool> RegisterAsync(string username, string password, string passwordRepeat);
    Task<bool> GetRegisteringAvailableStatus();
}

public class TokenService : IClientTokenService
{
    private HttpClient _httpClient;

    public TokenService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("TokenClient");
    }

    public string Token { get; private set; }

    public async Task<bool> GetRegisteringAvailableStatus()
    {
        return await _httpClient.GetFromJsonAsync<bool>($"{Routes.Auth}/{Routes.RegisteringAvailable}");
    }

    public async Task GetTokenAsync(string username, string password)
    {
        var token = await _httpClient.PostAsJsonAsync($"{Routes.Auth}/{Routes.Login}", new
        {
            Username = username,
            Password = password
        });

        if (token.IsSuccessStatusCode)
        {
            var body = await token.Content.ReadFromJsonAsync<TokenModel>();
            Token = body.Token;
        }
        else 
        { 
            throw new Exception("Failed to login."); 
        }
    }

    public async Task<bool> RegisterAsync(string username, string password, string passwordRepeat)
    {
        var registerModel = new RegisterModel {
            Username = username,
            Password = password,
            PasswordRepeat = passwordRepeat
        };

        var response = await _httpClient.PostAsJsonAsync($"{Routes.Auth}/{Routes.Register}", registerModel);

        if(response.IsSuccessStatusCode)
            return true;

        return false;
    }

    public void ResetToken()
    {
        Token = string.Empty;
    }
}
