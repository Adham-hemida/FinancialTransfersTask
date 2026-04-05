using FinancialTransfers.Infrastructure.Data;
using FinancialTransfers.Infrastructure.Implementation.Repositories;
using FinancialTransfers.Infrastructure.Implementation.Services;
using FinancialTransfers.Infrastructure.Implementation.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialTransfers.Infrastructure;
public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IAccountService, AccountSevice>();
		services.AddScoped<ITransferService, TransferService>();
		services.AddScoped<ITransferRepository, TransferRepository>();


		return services;
	}
}