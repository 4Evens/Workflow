using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Infrastructure;

namespace Infrastructure.ServiceImplementations.RabitMQ
{
    public class RabbitMQService : IMessageQueueService
    {
        private readonly IModel _channel;

        public RabbitMQService(IConnection connection)
        {
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: "workflow_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "workflow_queue", basicProperties: null, body: body);
        }
    }
}
