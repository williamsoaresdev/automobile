using System.Threading.Tasks;
using RabbitMQ.Client;

namespace AutomobileRentalManagementAPI.Infra.MessageQueue.RabbitMq
{
    public interface IRabbitMqConnection
    {
        Task<IConnection> GetConnectionAsync();
    }
}
