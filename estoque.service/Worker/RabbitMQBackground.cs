namespace estoque.service.Worker;

public class RabbitMQBackground : BackgroundService
{
    IServiceProvider _provider;
    public RabbitMQBackground(IServiceProvider provider)
    {
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Escutando fila...");
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _provider.CreateScope())
            {
                try
                {
                    IMessageConsumer consumer = scope.ServiceProvider.GetService<IMessageConsumer>();
                    consumer.VerificarFila();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Não foi possivel conectar ao BUS: {e.Message}");
                }
            }
            await Task.Delay(8000, stoppingToken);
        }
    }
}
