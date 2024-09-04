using Application.Services.Infrastructure;
using Infrastructure.ServiceImplementations.MappingProfiles;
using Infrastructure.ServiceImplementations.RabitMQ;
using Infrastructure.ServiceImplementations.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using StackExchange.Redis;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // AutoMapper servisini kaydet
            services.AddAutoMapper(typeof(EntityProfiles));

            // Redis ayarları
            var redisConfiguration = ConfigurationOptions.Parse(configuration.GetSection("RedisSettings:ConnectionString").Value);
            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(redisConfiguration));

            // ICacheService'yi RedisCacheService ile kaydet
            services.AddSingleton<Application.Services.Infrastructure.ICacheService, RedisCacheService>();

            // RabbitMQ ayarları
            var rabbitMQSettings = configuration.GetSection("RabbitMQSettings");
            var connectionFactory = new ConnectionFactory
            {
                HostName = rabbitMQSettings["HostName"],
                UserName = rabbitMQSettings["UserName"],
                Password = rabbitMQSettings["Password"]
            };
            var connection = connectionFactory.CreateConnection();
            services.AddSingleton<IConnection>(connection);
            services.AddSingleton<IMessageQueueService, RabbitMQProducerService>();
        }
    }
}