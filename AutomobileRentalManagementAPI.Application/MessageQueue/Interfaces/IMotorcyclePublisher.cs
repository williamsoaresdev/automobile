using AutomobileRentalManagementAPI.Application.Features.Motorcycles.CreateMotorcycle;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.UpdateMotorcycle;

namespace AutomobileRentalManagementAPI.Application.MessageQueue.Interfaces
{
    public interface IMotorcyclePublisher
    {
        Task PublishAsync(CreateMotorcycleCommand command, string queueName);
        //Task PublishAsync(UpdateMotorcycleCommand command, string queueName);
    }
}