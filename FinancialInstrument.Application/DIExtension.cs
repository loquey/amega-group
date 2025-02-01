using FinancialInstrument.Application.MessageSerialiazers;
using FinancialInstrument.Application.SocketHandlers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstrument.Application
{
    public static class DIExtension 
    {
        public static IServiceCollection AddApplicatoin(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISocketHandler, DefaultSocketHandler>();
            services.AddScoped<IMessageSerializer, DefaultMessageSerializer>();
            services.AddSingleton<ISocketRegistry, SocketRegistry>();
            return services;
        }
    }
}
