using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
