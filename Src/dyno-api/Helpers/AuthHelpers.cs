using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dyno_api.Helpers;

public interface IAuthHelpers
{
    string GenerateJwtToken();
}

public class AuthHelpers : IAuthHelpers
{
    private readonly IConfiguration _configuration;

    public AuthHelpers(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSecret"]); // Use same secret key as in authentication setup

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("sub", "user-id") }), // Use actual user data here
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}

