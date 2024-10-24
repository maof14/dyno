using Common;
using Service;

namespace Helpers
{
    public class ClientJwtAuthorizationHandler : JwtAuthorizationHandler
    {
        public ClientJwtAuthorizationHandler(IClientTokenService tokenService) : base(tokenService) { }

    }
}
