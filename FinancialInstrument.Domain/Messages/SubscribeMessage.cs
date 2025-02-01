using System.Text.Json.Serialization;

namespace FinancialInstrument.Domain.Messages
{
    public class SubscribeMessage
    {
        [JsonPropertyName("event")]
        public EventTypes Event { get; set; }

        [JsonPropertyName("ticker")]
        public string Ticker { get; set; }

        [JsonPropertyName("subscription_id")]
        public Guid? SubscriptionId { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EventTypes
    {
        Subscribe,
        UnSubscribe,
    }
}