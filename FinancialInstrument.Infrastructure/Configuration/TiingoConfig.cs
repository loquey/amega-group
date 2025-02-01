using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
