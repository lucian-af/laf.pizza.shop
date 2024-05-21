using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using PizzaShop.API.Authentication;
using PizzaShop.API.Authentication.Jwt;
using PizzaShop.API.Domain.Entities;
using PizzaShop.API.Domain.Interfaces;
using PizzaShop.API.Domain.Services;
using PizzaShop.API.Infrastructure.Configurations;
using PizzaShop.API.Settings;
using PizzaShop.API.Smtp.Adapters;
using PizzaShop.API.Smtp.Builders;
using PizzaShop.API.UseCases;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PizzaShop.API.Configurations
{
	public static class ServicesConfiguration
	{
		public static IServiceCollection AddServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			if (configuration is null)
				throw new ArgumentNullException(nameof(configuration), "IConfiguration is null.");

			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			services.AddDatabaseConfiguration(configuration.GetConnectionString("PizzaShop"));
			services.Configure<PizzaShopConfigs>(configuration.GetSection(nameof(PizzaShopConfigs)));
			services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
			services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
			services.AddDependencyInjections();
			services.AddVersioning();
			services.AddCors();
			services.AddCookieConfiguration();
			services.AddProblemDetails(options =>
			{
				options.CustomizeProblemDetails = ctx =>
				{
					ctx.ProblemDetails.Extensions.Add("trace-id", ctx.HttpContext.TraceIdentifier);
					ctx.ProblemDetails.Extensions.Add("instance", $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
				};
			});
			services.AddExceptionHandler<ExceptionToProblemDetailsHandler>();

			return services;
		}

		public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
		{
			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionsConfiguration>();
			services.AddScoped<CustomCookieAuthenticationEvents>();
			services.AddHttpContextAccessor();
			services.AddScoped<IGenerateCode, GenerateUuid>();
			services.AddScoped<IMailAdapter, MailAdapter>();
			services.AddScoped<IMailBuilder, MailBuilder>();
			services.AddScoped<IAuthenticate, AuthenticateJwt>();

			services.AddScoped<AddRestaurant>();
			services.AddScoped<GenerateMagicLink>();
			services.AddScoped<AuthenticateByMagicLink>();
			services.AddScoped<SignOut>();
			services.AddScoped<GetCurrentUser>();
			services.AddScoped<GetManagedRestaurant>();

			return services;
		}

		public static IServiceCollection AddVersioning(this IServiceCollection services)
		{
			services.AddApiVersioning(options =>
			{
				options.DefaultApiVersion = new ApiVersion(1);
				options.ReportApiVersions = true;
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ApiVersionReader = ApiVersionReader.Combine(
					 new UrlSegmentApiVersionReader(),
					 new HeaderApiVersionReader("X-Api-Version"));
			}).AddApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'V";
				options.SubstituteApiVersionInUrl = true;
			}).EnableApiVersionBinding();

			return services;
		}

		public static IServiceCollection AddCookieConfiguration(this IServiceCollection services)
		{
			services
				.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
				{
					options.Cookie.Name = AuthCookies.AuthCookieName;
					options.Cookie.Path = "/";
					options.Cookie.HttpOnly = true;
					options.EventsType = typeof(CustomCookieAuthenticationEvents);
					options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				});
			services.AddAuthorization();

			return services;
		}
	}
}