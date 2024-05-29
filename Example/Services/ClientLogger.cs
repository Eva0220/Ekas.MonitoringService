using System.Text.Json;
using Ekas.ExampleService.Services;
using Ekas.ExampleService.ViewModels;

namespace Ekas.ExampleService;

public class ClientLogger 
{
    private readonly KafkaProducer _kafkaProducer;

    public ClientLogger(KafkaProducer kafkaProducer)
    {
        _kafkaProducer = kafkaProducer;
    }

    public async Task Log(LogLevel logLevel,Exception exception)
    {
        try
        {
            if (logLevel <= LogLevel.Information) return;

            var incident = new Incident
            {
                Name = exception?.Message,
                ErrorDescription = exception?.StackTrace,
                CreatedDate = DateTime.UtcNow,
                Category = "client exception",
                ServiceName = typeof(Program).Assembly.GetName().Name
            };

            var json = JsonSerializer.Serialize(incident);


            await _kafkaProducer.ProduceMessageAsync(json);
        }
        catch
        {
            // ignored
        }
    }
}

