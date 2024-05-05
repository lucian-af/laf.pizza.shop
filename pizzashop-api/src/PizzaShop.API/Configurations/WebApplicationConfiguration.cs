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
			app.MapRestaurantEndpoints();
			app.MapUserEndpoints();
			app.AddSwaggerCustom();
			app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			return app;
		}

		private static void AddSwaggerCustom(this WebApplication app)
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