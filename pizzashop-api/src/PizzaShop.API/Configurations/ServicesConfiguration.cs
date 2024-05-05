using Asp.Versioning;
using Microsoft.Extensions.Options;
using PizzaShop.API.Infrastructure.Configurations;
using PizzaShop.API.Settings;
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
			services.AddDependencyInjections();
			services.AddVersioning();
			services.AddCors();

			return services;
		}

		public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
		{
			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptionsConfiguration>();
			services.AddScoped<AddRestaurant>();

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
	}
}