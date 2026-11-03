using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Books.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Books.Utils;

public class AuthService(IConfiguration config)
{
    public string GenerateToken(User user)
    {
        var jwtKey = config["JWT_KEY"];
        var jwtIssuer = config["JWT_ISSUER"];
        var jwtAudience = config["JWT_AUDIENCE"];
        
        int jwtExpireMinutes = int.TryParse(config["JWT_EXPIRE_MINUTES"], out var minutes)
            ? minutes
            : 60;

        var key = Encoding.UTF8.GetBytes(jwtKey!);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtExpireMinutes),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}