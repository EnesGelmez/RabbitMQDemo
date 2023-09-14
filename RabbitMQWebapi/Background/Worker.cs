using RabbitMQDemo.Data;
using RabbitMQWebapi.Models.Configurations;
using RabbitMQWebapi.Models.RabbitMQ;
using RabbitMQWebapi.Utilities;

namespace RabbitMQWebapi.Background;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;
    private readonly IGeneralConfig _generalConfig;
    private readonly Microsoft.Extensions.Configuration.IConfiguration _msConfiguration;
    private readonly GeneralDbContext _generalDbContext;

    public Worker(IServiceProvider serviceProvider)
    {
        try
        {
            _serviceProvider = serviceProvider;
            var loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger<Worker>();
            _generalConfig = _serviceProvider.GetRequiredService<IGeneralConfig>();
            _msConfiguration = _serviceProvider.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
        }
        catch (Exception ex)
        {
            _logger?.LogError("Worker->Constructor" + ex);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string uri = "amqp://guest:guest@localhost:5672";
        string clientProvideName = "Worker ReceiverRabbitMQ";
        string exchangeName = "ControlCustomer";
        string queueName = "AddCustomerMethod";
        string routingKey = "RabbitMQDemo_Key";
        Decloration decloration = new Decloration(uri, clientProvideName, exchangeName, queueName, routingKey);
        using var scope = _serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetService<GeneralDbContext>();
        ReceiverRabbitMQ receiverRabbitMQ = new ReceiverRabbitMQ(_logger, decloration, context);

        try
        {
            if (_generalConfig.UseWorker)
            {
                _logger?.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        _logger?.LogInformation($"----------------------BEGIN CYCLE {DateTimeOffset.Now}----------------------");

                        receiverRabbitMQ.ReceiveData(_generalConfig, decloration);

                        _logger?.LogInformation($"-----------------------END CYCLE {DateTimeOffset.Now}-----------------------");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Worker->Execute", ex);
                    }
                    await Task.Delay(_generalConfig.WorkerControlTime, stoppingToken);
                }
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "BackgroundService Failed!");
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            foreach (var process in System.Diagnostics.Process.GetProcessesByName("LOBJECTS"))
            {
                process.Kill();
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError("Worker->StopAsync", ex);

        }
        return base.StopAsync(cancellationToken);

    }
}
