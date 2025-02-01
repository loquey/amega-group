using FinancialInstrument.Application.MessageSerialiazers;
using FinancialInstrument.Domain.Messages;

using Microsoft.Extensions.Logging;

using System.Net.WebSockets;

namespace FinancialInstrument.Application.SocketHandlers
{
    public interface ISocketHandler
    {
        Task Handle(WebSocket webSocket);
    }

    public class DefaultSocketHandler(IMessageSerializer messageSerializer,
        ISocketRegistry socketRegistry,
        ILogger<DefaultSocketHandler> logger) : ISocketHandler
    {
        public async Task Handle(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!receiveResult.CloseStatus.HasValue)
            {
                if (!messageSerializer.TryDeserialize<SubscribeMessage>(buffer, receiveResult.Count, out var message))
                {
                    logger.LogError("Unable to deserialize data");
                    return;
                }
                logger.LogDebug("INPUT message {@msg}", message);

                if (message.Event == EventTypes.Subscribe)
                {
                    var subscriptionId = socketRegistry.RegisterSocket(webSocket, message!);
                    var response = messageSerializer.Serialize(subscriptionId);
                    await webSocket.SendAsync(
                        new ArraySegment<byte>(response, 0, response.Count()),
                        receiveResult.MessageType,
                        receiveResult.EndOfMessage,
                        CancellationToken.None);
                }
                else if (message.Event == EventTypes.UnSubscribe)
                    socketRegistry.DeregisterSocket(webSocket, message!);

                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }
    }
}