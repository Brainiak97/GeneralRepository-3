using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Domain.Models;

namespace Shared.Auth
{
    public class JwtService(JwtSettings settings) : IJwtService
    {
        private readonly JwtSettings _settings = settings;

        public string GenerateToken(User user, IEnumerable<Role> roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.Username),
                new(JwtRegisteredClaimNames.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateToken(int userId, string email, string purpose)
        {
            var claims = new List<Claim>
            {
                new("sub", userId.ToString()),
                new("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", email),
                new("purpose", purpose)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token, string? expectedPurpose)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = "HealthDiary.UserService",
                    ValidateAudience = true,
                    ValidAudience = "HealthDiary"
                }, out var validatedToken);


                var jwtToken = (JwtSecurityToken)validatedToken;
                var purposeClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "purpose");

                if (purposeClaim?.Value != expectedPurpose)
                    return null;

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
