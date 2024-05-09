using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

		public string Generate(DateTime dataExpiracao, Dictionary<string, string> payload = null)
		{
			var hashKey = ConvertKeyToBytes(_jwtSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = Issuer,
				Audience = Audience,
				Expires = dataExpiracao.ToUniversalTime(),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(hashKey), SecurityAlgorithms.HmacSha256Signature),
				Subject = new ClaimsIdentity(AddClaims(payload))
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

		private static List<Claim> AddClaims(Dictionary<string, string> payload)
		{
			var claims = new List<Claim>();

			if (payload?.Count > 0)
				foreach (var (key, value) in payload)
					claims.Add(new(key, value));

			claims.Add(new(JwtRegisteredClaimNames.Iat, DateTime.Today.ToString()));

			return claims;
		}

		private static byte[] ConvertKeyToBytes(string key)
			=> Encoding.UTF8.GetBytes(key);
	}
}