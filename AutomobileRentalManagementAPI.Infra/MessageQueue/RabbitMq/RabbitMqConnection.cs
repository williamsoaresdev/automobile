using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace AutomobileRentalManagementAPI.Infra.MessageQueue.RabbitMq
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly Task<IConnection> _connectionTask;

        public RabbitMqConnection(IOptions<RabbitMQSettings> options)
        {
            var settings = options.Value;

            var factory = new ConnectionFactory
            {
                HostName = settings.Host,
                Port = settings.Port,
                UserName = settings.Username,
                Password = settings.Password,
                VirtualHost = settings.VirtualHost
            };

            _connectionTask = factory.CreateConnectionAsync();
        }

        public Task<IConnection> GetConnectionAsync() => _connectionTask;
    }
}
