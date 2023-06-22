using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Entities;
using Microsoft.IdentityModel.Tokens;

namespace api.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration) {
        _configuration = configuration;
    }

    public string GenerateToken(UserEntity user) {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var key = Encoding.UTF8.GetBytes(
            _configuration["Authentication:SecretKey"]
        );

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, user.Username.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}