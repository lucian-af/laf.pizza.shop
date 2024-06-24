using Microsoft.AspNetCore.Diagnostics;
using PizzaShop.API.Domain.Exceptions;

namespace PizzaShop.API.Configurations
{
	public class ExceptionToProblemDetailsHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
	{
		private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;

		public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			switch (exception)
			{
				case ArgumentException:
				case NullValueException:
				case DomainException:
					httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
					return _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
					{
						HttpContext = httpContext,
						ProblemDetails =
							{
								Type = exception.GetType().Name,
								Title = "BadRequest",
								Status = httpContext.Response.StatusCode,
								Detail = exception.Message,
							}
					});

				case UnauthorizedException:
					httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
					return _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
					{
						HttpContext = httpContext,
						ProblemDetails =
							{
								Type = exception.GetType().Name,
								Title = "Forbidden",
								Status = httpContext.Response.StatusCode,
								Detail = exception.Message,
							}
					});

				default:
					httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
					return _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
					{
						HttpContext = httpContext,
						ProblemDetails =
							{
								Type = exception.GetType().Name,
								Title = "Error",
								Status = httpContext.Response.StatusCode,
								Detail = "Internal Server Error",
							}
					});
			}
		}
	}
}