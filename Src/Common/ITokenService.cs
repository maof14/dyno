namespace Common;

public interface ITokenService
{
    Task GetTokenAsync(string username, string password);
    void ResetToken();
    Task<bool> RegisterAsync(string username, string password, string passwordRepeat);
    Task<bool> GetRegisteringAvailableStatus();

    string Token { get; }
}
