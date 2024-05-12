using MessageQueue.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueue.Engines.RabbitMQ
{
    public class RabbitMQPublisher<T> : IQueuePublisher<T>
    {
        private IModel _channel;
        private string _exchangeName;
        public RabbitMQPublisher(string exchangeName)
        {
            _exchangeName = exchangeName;
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
        public Task SendMessage(T message)
        {
            var task = new Task(() =>
            {
                var data = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(data);
                _channel.BasicPublish(         
                    exchange: _exchangeName,
                                     routingKey: string.Empty,
                                     basicProperties: null,
                                     body: body);
            });
            
            return task;           
        }
    }
}
