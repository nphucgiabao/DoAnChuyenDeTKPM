using MessageQueue.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueue.Engines.RabbitMQ
{
    public class RabbitMQSubcriber : IQueueSubcribercs
    {
        private IModel _channel;
        private string _exchangeName;
        private readonly Action<string> _handler;
        public RabbitMQSubcriber(string exchangeName, Action<string> handle)
        {
            _exchangeName = exchangeName;
            _handler = handle;
            InitializeQueue();
        }
        private void InitializeQueue()
        {
            if (_channel == null)
            {
                var factory = new ConnectionFactory { HostName = "localhost" };
                var connection = factory.CreateConnection();
                _channel = connection.CreateModel();
                _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout, durable: false);
            }
        }
        public async Task ProcessQueue()
        {
            var task = new Task(() =>
            {
                var queueName = _channel.QueueDeclare().QueueName;
                _channel.QueueBind(queue: queueName,
                                  exchange: _exchangeName,
                                  routingKey: string.Empty);

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _handler(message);
                };
                _channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);
            });

            task.Start();
            await task;           
        }
    }
}
