using System.ComponentModel.DataAnnotations;

namespace FinancialInstrument.Infrastructure.Configuration
{
    public class TiingoConfig
    {
        public const string SectionName = "Tiingo";

        [Required]
        public string BaseUrl { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string WebSocketUrl { get; set; }
    }
}