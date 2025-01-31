using FinancialInstrument.Infrastructure.Repositories;

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
    }
}
