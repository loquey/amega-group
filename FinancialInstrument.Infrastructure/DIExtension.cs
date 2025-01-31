using FinancialInstrument.Infrastructure.Repositories;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstrument.Infrastructure
{
    static public class DIExtension
    {
        static public IServiceCollection AddInfrastructure (this IServiceCollection services)
        {
            services.AddScoped<ITickerRepository, TickerRepository>();
            return services;
        }
    }
}
