using PizzaShop.API.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServicesConfiguration(builder.Configuration);

builder
	.Build()
	.AddWebApplicationConfiguration()
	.Run();