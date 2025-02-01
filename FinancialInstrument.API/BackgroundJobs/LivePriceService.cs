using FinancialInstrument.Application.MessageSerialiazers;
using FinancialInstrument.Application.SocketHandlers;
using FinancialInstrument.Infrastructure.Configuration;
using FinancialInstrument.Infrastructure.Repositories;

using Microsoft.Extensions.Options;

using System.Net.WebSockets;
using System.Text;

namespace FinancialInstrument.API.BackgroundJobs
{
    public class LivePriceService(ILogger<LivePriceService> logger, IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Starting live price service");

            using var scope = serviceProvider.CreateScope();

            ISocketRegistry socketRegistry = scope.ServiceProvider.GetRequiredService<ISocketRegistry>();
            IMessageSerializer messageSerializer = scope.ServiceProvider.GetRequiredService<IMessageSerializer>();
            IOptions<TiingoConfig> tiingoConfig = scope.ServiceProvider.GetRequiredService<IOptions<TiingoConfig>>();
            ITickerRepository tickerRepository = scope.ServiceProvider.GetRequiredService<ITickerRepository>();

            var tiingoClient = new ClientWebSocket();
            try
            {
                await tiingoClient.ConnectAsync(new Uri(tiingoConfig.Value.WebSocketUrl), stoppingToken);
            }
            catch (Exception ex) { logger.LogError(ex, "errpr"); }
            var tickers = tickerRepository.GetTickers().Select(s => s.TickerSymnbol).ToArray();

            var subscribeMessage = new
            {
                @event = "subscribe",
                subscription = new { name = "ticker" },
                pair = tickers
            };

            await tiingoClient.SendAsync(
                new ArraySegment<byte>(messageSerializer.Serialize(subscribeMessage)),
                WebSocketMessageType.Text,
                true,
                stoppingToken
            );

            var readBuffer = new byte[4096];
            while (true)
            {
                if (tiingoClient.State != WebSocketState.Open) break;

                var receiveResult = await tiingoClient.ReceiveAsync(
                    new ArraySegment<byte>(readBuffer), CancellationToken.None);

                string stringBuffer = Encoding.UTF8.GetString(readBuffer, 0, receiveResult.Count);
                var decoder = MessageDecoder.Create(stringBuffer);
                if (decoder.IsPriceUpdate())
                {
                    logger.LogInformation("Price update:  {@msg}", stringBuffer);
                    await socketRegistry.BroadcastMessageAsync(decoder, Encoding.UTF8.GetBytes(stringBuffer));
                }
                else
                    logger.LogInformation("Provider message : {@mesg}", stringBuffer);
                await Task.Delay(2000);
            }
        }
    }
}