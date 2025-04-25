using AutomobileRentalManagementAPI.Application.Features.Motorcycles.CreateMotorcycle;
using AutomobileRentalManagementAPI.Application.MessageQueue.Interfaces;
using AutomobileRentalManagementAPI.Infra.MessageQueue.RabbitMq;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace AutomobileRentalManagementAPI.Application.MessageQueue.Publishers.Motorcycle
{
    public class MotorcyclePublisher : IMotorcyclePublisher
    {
        private readonly RabbitMQSettings _settings;

        public MotorcyclePublisher(IOptions<RabbitMQSettings> options)
        {
            _settings = options.Value;
        }

        public async Task PublishAsync(CreateMotorcycleCommand command, string queueName)
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

            await channel.QueueDeclareAsync(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var message = JsonSerializer.Serialize(command);
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queueName,
                body: body);
        }
    }
}
