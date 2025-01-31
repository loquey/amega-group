using FinancialInstrument.Infrastructure.Configuration;
using FinancialInstrument.Infrastructure.ServiceClients.Messages;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstrument.Infrastructure.ServiceClients
{

    public interface ITiingoClient
    {
        Task<IEnumerable<TickerPriceMessage>> GetTickerPriceAsync(string tickerSymbol);
    }

    class TiingoClient(HttpClient httpclient, 
            IOptions<TiingoConfig> tiingoConfig ,
            ILogger<TiingoClient> logger) : ITiingoClient
    {
        public async Task<IEnumerable<TickerPriceMessage>> GetTickerPriceAsync(string tickerSymbol)
        {
            logger.LogInformation("Calling tiingo service");

            var response = await httpclient.GetFromJsonAsync<IEnumerable<TickerPriceMessage>>($"top?tickers={tickerSymbol}");

            logger.LogInformation("Ticker response is {@resp}", response);

            return response;
        }

    }
}
