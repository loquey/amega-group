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
        private static IEnumerable<Ticker> TickerCollection = [
                new Ticker { TickerName = "Euro Usd pair", TickerSymnbol = "eurusd"},
                new Ticker { TickerName = "Usd Jpy pair", TickerSymnbol = "usdjpy"},
                new Ticker { TickerName = "Bitcoin usd pair", TickerSymnbol = "btcusd"},
            ];

        public IEnumerable<Ticker> GetTickers()
            => TickerCollection;
    }
}
