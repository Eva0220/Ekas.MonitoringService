using Confluent.Kafka;

namespace Ekas.ExampleService.Services;


public class KafkaProducer
{
    private readonly string _topic;
    private readonly ProducerConfig _config;
    private readonly IProducer<string, string> _producer;

    public KafkaProducer(string bootstrapServers, string topic)
    {
        _topic = topic;
        _config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers,

        };
        _producer = new ProducerBuilder<string, string>(_config).Build();
    }

    public async Task ProduceMessageAsync(string message)
    {
        try
        {
            var deliveryReport = await _producer.ProduceAsync(_topic, new Message<string, string> { Key = null, Value = message });
        }
        catch (ProduceException<string, string> e)
        {
            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
        }
    }
}