using FinancialTransfers.API.ExceptionHandler;

namespace FinancialTransfers.API;

public static class DependencyInjection
{
	public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
	{

		services.AddExceptionHandler<GlobalExceptionHandler>();
		services.AddProblemDetails();

		return services;
	}
}