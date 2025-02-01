using FinancialInstrument.Domain.DbEntities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstrument.Infrastructure.Repositories
{
    public interface ITickerRepository
    {
        IEnumerable<Ticker> GetTickers();
    }

    public class TickerRepository : ITickerRepository
    {
        //Todo: This will likely be coming from a database 
        private static IEnumerable<Ticker> TickerCollection = [
                new Ticker { TickerName = "Euro Usd pair", TickerSymnbol = "EUR/USD"},
                new Ticker { TickerName = "Usd Jpy pair", TickerSymnbol = "USD/JPY"},
                new Ticker { TickerName = "Bitcoin usd pair", TickerSymnbol = "BTC/USD"},
            ];

        public IEnumerable<Ticker> GetTickers()
            => TickerCollection;
    }
}
