using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PizzaShop.API.Authentication.Jwt;
using PizzaShop.API.Settings;

namespace PizzaShop.API.Configurations
{
	public static class AuthenticateConfiguration
	{
		public static void AddJwt(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
		{
			var jwtSettings = new JwtSettings();
			configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
			authenticationBuilder.AddJwtBearer(
				JwtBearerDefaults.AuthenticationScheme,
				options => options.TokenValidationParameters = AuthenticateJwt.JwtTokenValidationParameters(jwtSettings.Secret));
		}
	}
}