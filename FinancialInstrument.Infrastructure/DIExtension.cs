using FinancialInstrument.Infrastructure.Configuration;
using FinancialInstrument.Infrastructure.Repositories;
using FinancialInstrument.Infrastructure.ServiceClients;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstrument.Infrastructure
{
    static public class DIExtension
    {
        static public IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
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
