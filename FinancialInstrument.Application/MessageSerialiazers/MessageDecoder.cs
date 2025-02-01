using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinancialInstrument.Application.MessageSerialiazers
{
    public interface IMessageDecoder
    {
        string GetTicker();
        bool IsPriceUpdate();
        bool MatchesTicker(string ticker);
    }

    public class MessageDecoder : IMessageDecoder
    {
        private JsonDocument _Doc;

        public static IMessageDecoder Create(string strDoc)
            => new MessageDecoder(JsonDocument.Parse(strDoc));

        private MessageDecoder(JsonDocument doc)
        {
            _Doc = doc;
        }

        public bool IsPriceUpdate()
        {
            if (_Doc.RootElement.ValueKind != JsonValueKind.Array ||
                _Doc.RootElement[2].GetString() != "ticker") return false;

            return true;
        }

        public bool MatchesTicker(string ticker)
        {
            if (!IsPriceUpdate() || _Doc.RootElement[3].GetString() != ticker) 
                return false;

            return true;
        }

        public string GetTicker()
        {
            if (!IsPriceUpdate()) return string.Empty;

            return _Doc.RootElement[3]!.GetString() ?? string.Empty;
        }
    }
}
