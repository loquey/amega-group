using FinancialInstrument.Infrastructure.Configuration;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstrument.Infrastructure.ServiceClients
{
    internal class TiingoTokenDelegatinHandler(ILogger<TiingoTokenDelegatinHandler> logger, IOptions<TiingoConfig> tiingoConfig)
        : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.RequestUri = new Uri($"{request.RequestUri!.AbsoluteUri}&token={tiingoConfig.Value.ApiKey}");
            
            logger.LogDebug("absolute url is {url}", request.RequestUri.AbsoluteUri);
            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
