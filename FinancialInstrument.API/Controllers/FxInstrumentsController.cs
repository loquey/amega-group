using FinancialInstrument.Infrastructure.Repositories;
using FinancialInstrument.Infrastructure.ServiceClients;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialInstrument.API.Controllers
{
    [Route("api/fx")]
    [ApiController]
    public class FxInstrumentsController(ITickerRepository tickerRepository, ILogger<FxInstrumentsController> logger) 
        : ControllerBase
    {
        [HttpGet("tickers")]
        public IActionResult GetTickers()
        {
            logger.LogInformation("Ticker endpoint called");
            return Ok(tickerRepository.GetTickers());
        }

        [HttpGet("ticker/{tickerSymbol:alpha}/price")]
        public async Task<IActionResult> GetTickerPrice([FromRoute] string tickerSymbol, [FromServices] ITiingoClient tiingoClient)
        {
            logger.LogInformation("Retrieving price for ticker symbol {symbol}", tickerSymbol);

            var tickerPricing = await tiingoClient.GetTickerPriceAsync(tickerSymbol);

            return Ok(tickerPricing);
        }
    }
}
