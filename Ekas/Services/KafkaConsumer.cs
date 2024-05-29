using Confluent.Kafka;
using System.Collections.Generic;

namespace Ekas.Monitoring.Services
{
    public class KafkaConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly IConsumer<string, string> _consumer;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public KafkaConsumer(string bootstrapServers, string groupId)
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            _consumer = new ConsumerBuilder<string, string>(_config).Build();

            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Subscribe(string topic) 
        {
            _consumer.Subscribe(topic);
        }

        public async Task<string> GetMessages() 
        {
            var message = _consumer.Consume(_cancellationTokenSource.Token).Message;
            _consumer.Commit();
            return message.Value;
        }

        public void Close() 
        {
            _consumer.Close();
        }

        public void StopConsuming()
        {
            _cancellationTokenSource.Cancel();
        }
    }

}
