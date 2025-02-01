using FinancialInstrument.Infrastructure.Configuration;
using FinancialInstrument.Infrastructure.Repositories;
using FinancialInstrument.Infrastructure.ServiceClients;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialInstrument.Infrastructure
{
    public static class DIExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITickerRepository, TickerRepository>();
            services.AddOptions<TiingoConfig>()
                .Bind(configuration.GetSection("Tiingo"))
                .ValidateDataAnnotations();

            services.AddScoped<TiingoTokenDelegatinHandler>();
            services.AddHttpClient<ITiingoClient, TiingoClient>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>($"{TiingoConfig.SectionName}:BaseUrl")!);
            }).AddHttpMessageHandler<TiingoTokenDelegatinHandler>();

            return services;
        }
    }
}