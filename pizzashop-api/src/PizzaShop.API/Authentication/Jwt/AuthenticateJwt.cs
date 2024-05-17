using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PizzaShop.API.Settings;

namespace PizzaShop.API.Authentication.Jwt
{
	public class AuthenticateJwt(IOptions<JwtSettings> jwtSettings) : IAuthenticate
	{
		private readonly JwtSettings _jwtSettings = jwtSettings.Value;
		private const string Issuer = "PizzaShop";
		private const string Audience = "PizzaShop";

		public string Generate<T>(DateTime dataExpiracao, T payload)
		{
			var hashKey = ConvertKeyToBytes(_jwtSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = Issuer,
				Audience = Audience,
				Expires = dataExpiracao.ToUniversalTime(),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(hashKey), SecurityAlgorithms.HmacSha256Signature),
				Subject = new ClaimsIdentity(AddPayload(payload))
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
		}

		public bool Valid(string token)
		{
			try
			{
				new JwtSecurityTokenHandler()
					.ValidateToken(token, JwtTokenValidationParameters(_jwtSettings.Secret), out var _);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static TokenValidationParameters JwtTokenValidationParameters(string key)
		{
			var hashKey = ConvertKeyToBytes(key);

			return new()
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = Issuer,
				ValidAudience = Audience,
				IssuerSigningKey = new SymmetricSecurityKey(hashKey),
				ClockSkew = TimeSpan.Zero,
			};
		}

		public T GetPayload<T>(string token)
		{
			var jwtSecurityToken = new JwtSecurityToken(token);
			var payload = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "payload")?.Value;
			ArgumentException.ThrowIfNullOrWhiteSpace(payload);
			return JsonSerializer.Deserialize<T>(payload);
		}

		private static List<Claim> AddPayload<T>(T payload)
		{
			var claims = new List<Claim>();

			if (payload is not null)
				claims.Add(new(nameof(payload), JsonSerializer.Serialize(payload)));

			claims.Add(new(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

			return claims;
		}

		private static byte[] ConvertKeyToBytes(string key)
			=> Encoding.UTF8.GetBytes(key);
	}
}