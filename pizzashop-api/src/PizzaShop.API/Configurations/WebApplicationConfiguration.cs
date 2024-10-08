﻿using Microsoft.Extensions.Options;
using PizzaShop.API.Endpoints;
using PizzaShop.API.Infrastructure.Configurations;
using PizzaShop.API.Settings;

namespace PizzaShop.API.Configurations
{
	public static class WebApplicationConfiguration
	{
		public static WebApplication AddWebApplicationConfiguration(this WebApplication app)
		{
			var pizzaShopConfigs = app.Services.GetRequiredService<IOptions<PizzaShopConfigs>>();
			app.RunMigrations(pizzaShopConfigs);
			app.UseCors("PizzaShop");
			app.MapAllEndpoints();
			app.UseSwaggerCustom();
			app.UseStatusCodePages();
			app.UseExceptionHandler();
			return app;
		}

		private static void MapAllEndpoints(this WebApplication app)
		{
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseRestaurantEndpoints();
			app.UseUserEndpoints();
			app.UseAuthenticationEndpoints();
			app.UseOrderEndpoints();
			app.UseMetricsEndpoints();
		}

		private static void UseSwaggerCustom(this WebApplication app)
		{
			if (app.Environment.IsDevelopment())
			{
				var descriptions = app.DescribeApiVersions();
				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					foreach (var description in descriptions)
					{
						options.SwaggerEndpoint(
							 $"/swagger/{description.GroupName}/swagger.json",
								description.GroupName.ToUpperInvariant());
					}
				});
			}
		}
	}
}