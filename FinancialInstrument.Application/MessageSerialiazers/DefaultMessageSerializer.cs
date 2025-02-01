using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinancialInstrument.Application.MessageSerialiazers
{
    public interface IMessageSerializer
    {
        byte[] Serialize<T>(T subscriptionId);
        bool TryDeserialize<T>(byte[] buffer, int count, out T? message);
    }

    public class DefaultMessageSerializer (ILogger<DefaultMessageSerializer> logger) : IMessageSerializer
    {
        public byte[] Serialize<T>(T subscriptionId)
        {
            var str = JsonSerializer.Serialize(subscriptionId);
            return Encoding.UTF8.GetBytes(str);
        }

        public bool TryDeserialize<T>(byte[] buffer, int count, out T? message)
        {
            message = default;

            string data = Encoding.UTF8.GetString(buffer, 0, count);
            try
            {
                message = JsonSerializer.Deserialize<T>(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Deserialization error");
                return false;
            }

            return true;
        }
    }
}
