using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectManager.Domain.Context;
using ProjectManager.Domain.Exceptions;
using ProjectManager.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectManager.Application.Helpers
{
    public static class TokenHelper
    {
        public static string Create(User user, IConfiguration configuration)
        {
			try
			{
				// Propiedades adicionales adjuntas a nuestro token JWT
				var claims = new[]
				{
					new Claim("UserId", user.UserId.ToString()),
					new Claim("Message", "Hello world!")
				};

				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] ?? throw new TokenHelperJwtException(ResponseConsts.JwtSecretKeyNotArgumented)));
				var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

				var expiration = configuration["Jwt:ExpirationInMinutes"] ?? throw new TokenHelperJwtException(ResponseConsts.JwtExpirationInMinutesNotArgumented);
				var expirationDate = DateTime.Now.AddMinutes(Convert.ToInt32(expiration));

				var token = new JwtSecurityToken(
					audience: configuration["Jwt:Audience"] ?? throw new TokenHelperJwtException(ResponseConsts.JwtAudienceNotArgumented),
					issuer: configuration["Jwt:Issuer"] ?? throw new TokenHelperJwtException(ResponseConsts.JwtIssuerNotArguemented),
					claims: claims,
					expires: expirationDate,
					signingCredentials: credentials
                );

				return new JwtSecurityTokenHandler().WriteToken(token);
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
