using System.Text.Json.Serialization;

namespace FinancialInstrument.Domain.Messages
{
    internal class TickerPriceUpdate
    {
        [JsonPropertyName("messageType")]
        public string MessageType { get; set; }

        [JsonPropertyName("service")]
        public string Service { get; set; }

        [JsonPropertyName("data")]
        public List<object> Data { get; set; }
    }
}