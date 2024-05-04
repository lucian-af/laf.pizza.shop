using Microsoft.Extensions.Options;
using PizzaShop.API.Infrastructure.Configurations;
using PizzaShop.API.Settings;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabaseConfiguration(configuration.GetConnectionString("PizzaShop"));
builder.Services.Configure<PizzaShopConfigs>(builder.Configuration.GetSection(nameof(PizzaShopConfigs)));

var app = builder.Build();

var pizzaShopConfigs = app.Services.GetRequiredService<IOptions<PizzaShopConfigs>>();
app.RunMigrations(pizzaShopConfigs);

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapGet("/", (IOptions<PizzaShopConfigs> configs) =>
{
	return configs.Value.Mode;
})
.WithOpenApi();

app.Run();
