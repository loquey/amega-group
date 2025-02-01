
using FinancialInstrument.Application.MessageSerialiazers;
using FinancialInstrument.Application.SocketHandlers;

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

            while (true)
            {
                await socketRegistry.BroadcastMessageAsync(messageSerializer.Serialize(new { msg = "Hello websocket broadcast" }));

                await Task.Delay(3000);
            }
        }
    }
}
