using Asp.Versioning;

namespace PizzaShop.API.Endpoints
{
	public static class MainEndpoints
	{
		public static RouteGroupBuilder GetRouteGroupBuilder(IEndpointRouteBuilder app, int version = 1)
		{
			ArgumentNullException.ThrowIfNull(app);

			var apiVersion = app.NewApiVersionSet()
				.HasApiVersion(new ApiVersion(version))
				.ReportApiVersions()
				.Build();

			return app.MapGroup("api/v{apiVersion:apiVersion}")
					  .WithApiVersionSet(apiVersion)
					  .RequireAuthorization();
		}
	}
}