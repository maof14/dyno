namespace Common;

public interface ITokenService
{
    Task<string> GetTokenAsync();
    string Token { get; }
}
