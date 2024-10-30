namespace Common;

public interface ITokenService
{
    Task GetTokenAsync(string username, string password);
    void ResetToken();
    string Token { get; }
}
