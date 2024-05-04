using Microsoft.Extensions.Options;
using PizzaShop.API.Configurations;
using PizzaShop.API.Domain.Models;
using PizzaShop.API.Infrastructure.Configurations;
using PizzaShop.API.Settings;
using PizzaShop.API.UseCases;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebApiConfiguration(builder.Configuration);

var app = builder.Build();

var pizzaShopConfigs = app.Services.GetRequiredService<IOptions<PizzaShopConfigs>>();
app.RunMigrations(pizzaShopConfigs);

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapPost("/restaurants", async (AddRestaurantDto request, AddRestaurant service) =>
{
	await service.Execute(request);
	return Results.Created();
}).WithOpenApi();

app.Run();