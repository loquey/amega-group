namespace FinancialInstrument.Infrastructure.ServiceClients.Messages
{
    public class TickerPriceMessage
    {
        public string Ticker { get; set; }
        public DateTime QuoteTimestamp { get; set; }
        public decimal BidPrice { get; set; }
        public decimal AidSize { get; set; }
        public decimal AskPrice { get; set; }
        public decimal AskSize { get; set; }
        public decimal MidPrice { get; set; }
    }
}