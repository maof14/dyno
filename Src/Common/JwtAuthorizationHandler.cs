namespace Common;

public class JwtAuthorizationHandler : DelegatingHandler
{
    private readonly ITokenService _tokenService;

    public JwtAuthorizationHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _tokenService.Token;
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        return base.SendAsync(request, cancellationToken);
    }
}
