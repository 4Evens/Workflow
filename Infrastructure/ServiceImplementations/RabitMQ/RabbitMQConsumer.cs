using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceImplementations.RabitMQ
{
    public class RabbitMQConsumer : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // RabbitMQ consumer logic
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Clean up resources here
            return Task.CompletedTask;
        }
    }
}
