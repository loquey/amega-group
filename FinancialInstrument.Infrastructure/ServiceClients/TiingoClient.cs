using FinancialInstrument.Infrastructure.ServiceClients.Messages;

using Microsoft.Extensions.Logging;

using System.Net.Http.Json;

namespace FinancialInstrument.Infrastructure.ServiceClients
{
    public interface ITiingoClient
    {
        Task<IEnumerable<TickerPriceMessage>> GetTickerPriceAsync(string tickerSymbol);
    }

    internal class TiingoClient(HttpClient httpclient,
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