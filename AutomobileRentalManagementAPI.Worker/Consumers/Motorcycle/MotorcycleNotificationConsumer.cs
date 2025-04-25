using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using AutomobileRentalManagementAPI.Infra.MessageQueue.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace AutomobileRentalManagementAPI.Worker.Consumers.Motorcycle
{
    public class MotorcycleNotificationConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMQSettings _settings;

        public MotorcycleNotificationConsumer(IServiceProvider serviceProvider, IOptions<RabbitMQSettings> options)
        {
            _serviceProvider = serviceProvider;
            _settings = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.Host,
                Port = _settings.Port,
                UserName = _settings.Username,
                Password = _settings.Password,
                VirtualHost = _settings.VirtualHost
            };

            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "notification-queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: stoppingToken
            );

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                using var scope = _serviceProvider.CreateScope();
                var motorcycleMongoRepository = scope.ServiceProvider.GetRequiredService<IMotorcycleMongoRepository>();

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var motorcycle = JsonSerializer.Deserialize<MotorcycleMongo>(message);

                if (motorcycle != null)
                    await motorcycleMongoRepository.InsertAsync(motorcycle);
            };

            await channel.BasicConsumeAsync(
                queue: "notification-queue",
                autoAck: true,
                consumer: consumer
            );

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
