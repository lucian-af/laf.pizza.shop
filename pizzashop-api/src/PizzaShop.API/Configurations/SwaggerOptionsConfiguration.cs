using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PizzaShop.API.Configurations
{
	public class SwaggerOptionsConfiguration(IApiVersionDescriptionProvider provider) : IConfigureNamedOptions<SwaggerGenOptions>
	{
		private readonly IApiVersionDescriptionProvider _provider = provider;

		/// <summary>
		/// Configura cada uma das API Versions
		/// </summary>
		/// <param name="options"></param>
		public void Configure(SwaggerGenOptions options)
		{
			foreach (var description in _provider.ApiVersionDescriptions)
			{
				options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
			}
		}

		/// <summary>
		/// Configure Swagger Options. Inherited from the Interface
		/// </summary>
		/// <param name="name"></param>
		/// <param name="options"></param>
		public void Configure(string name, SwaggerGenOptions options)
			=> Configure(options);

		/// <summary>
		/// Adiciona informações personalizadas para uma versão da API
		/// </summary>
		/// <param name="description"></param>
		/// <returns>Information about the API</returns>
		private static OpenApiInfo CreateVersionInfo(ApiVersionDescription desc)
		{
			var info = new OpenApiInfo()
			{
				Title = "Pizza.Shop API",
				Version = desc.ApiVersion.ToString()
			};

			if (desc.IsDeprecated)
				info.Description += "Esta versão da API foi descontinuada. Use uma das novas APIs disponíveis no explorer.";

			return info;
		}
	}
}