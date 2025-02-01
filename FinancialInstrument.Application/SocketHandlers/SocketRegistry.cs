using FinancialInstrument.Application.MessageSerialiazers;
using FinancialInstrument.Domain.Messages;

using Microsoft.Extensions.Logging;

using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace FinancialInstrument.Application.SocketHandlers
{
    public interface ISocketRegistry
    {
        void DeregisterSocket(WebSocket webSocket, SubscribeMessage subscribeMessage);

        NewSubscriptionResponse RegisterSocket(WebSocket socket, SubscribeMessage message);

        Task BroadcastMessageAsync(IMessageDecoder decoder, byte[] message);
    }

    public class SocketRegistry(ILogger<SocketRegistry> logger) : ISocketRegistry
    {
        private static ConcurrentDictionary<Guid, SocketSubscrition> _SocketList =
            new ConcurrentDictionary<Guid, SocketSubscrition>();

        public async Task BroadcastMessageAsync(IMessageDecoder decoder, byte[] message)
        {
            logger.LogInformation("{count} sockets registered", _SocketList.Count);
            await Parallel.ForEachAsync(_SocketList, async (entry, cancellationToken) =>
            {
                var (_, socketEntry) = entry;

                if (socketEntry.socket.State != WebSocketState.Open ||
                    !socketEntry.tickers.Contains(decoder.GetTicker())) return;

                await socketEntry.socket.SendAsync(
                    new ArraySegment<byte>(message, 0, message.Count()),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None);
                return;
            });
        }

        public void DeregisterSocket(WebSocket webSocket, SubscribeMessage message)
        {
            if (message.SubscriptionId is null ||
                !_SocketList.ContainsKey(message.SubscriptionId.Value)) return;

            _SocketList[message.SubscriptionId.Value].tickers.Remove(message.Ticker);
        }

        public NewSubscriptionResponse RegisterSocket(WebSocket socket, SubscribeMessage message)
        {
            if (message.SubscriptionId is null)
            {
                message.SubscriptionId = Guid.NewGuid();
                _SocketList.TryAdd(message.SubscriptionId.Value, new SocketSubscrition(socket, [message.Ticker]));
                logger.LogInformation("New socket registered, {count} registered", _SocketList.Count);
            }
            else if (_SocketList.ContainsKey(message.SubscriptionId.Value))
                _SocketList[message.SubscriptionId.Value].tickers.Add(message.Ticker);

            return new NewSubscriptionResponse { SubscriptionId = message.SubscriptionId.Value };
        }
    }

    internal record SocketSubscrition(WebSocket socket, HashSet<string> tickers);
}