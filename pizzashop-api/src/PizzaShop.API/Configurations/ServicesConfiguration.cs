using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PizzaShop.API.Authentication;
using PizzaShop.API.Authentication.Jwt;
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
			services.AddSwaggerCustom();
			services.AddDatabaseConfiguration(configuration.GetConnectionString("PizzaShop"));
			services.Configure<PizzaShopConfigs>(configuration.GetSection(nameof(PizzaShopConfigs)));
			services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
			services.AddDependencyInjections();
			services.AddVersioning();
			services.AddCors();
			services.AddJwtAuthentication(configuration);

			return services;
		}

		public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
		{
			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionsConfiguration>();
			services.AddScoped<AddRestaurant>();
			services.AddScoped<GenerateMagicLink>();
			services.AddScoped<IGenerateCode, GenerateUuid>();
			services.AddScoped<IMailAdapter, MailAdapter>();
			services.AddScoped<IMailBuilder, MailBuilder>();
			services.AddScoped<IAuthenticate, AuthenticateJwt>();

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

		public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwt(configuration);
			services.AddAuthorization();

			return services;
		}

		public static IServiceCollection AddSwaggerCustom(this IServiceCollection services)
		{
			services.AddSwaggerGen(gen =>
			{
				gen.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description =
					"JWT Authorizarion header using Bearer Scheme. Enter 'Bearer Example': \'Bearer 1a2b3c4d5e6f...\'"
				});
				gen.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						Array.Empty<string>()
					}
				});
			});

			return services;
		}
	}
}