using MediatR;
using FluentValidation;
using AutomobileRentalManagementAPI.Application.MessageQueue.Interfaces;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using AutomobileRentalManagementAPI.Domain.CustomExceptions;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.CreateMotorcycle
{
    public class CreateMotorcycleHandler : IRequestHandler<CreateMotorcycleCommand, CreateMotorcycleResult>
    {
        private readonly IMotorcyclePublisher _motorcyclePublisher;
        private readonly IMotorcycleRepository _motorcycleRepository;

        public CreateMotorcycleHandler(IMotorcyclePublisher motorcyclePublisher, IMotorcycleRepository motorcycleRepository)
        {
            _motorcyclePublisher = motorcyclePublisher;
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<CreateMotorcycleResult> Handle(CreateMotorcycleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateMotorcycleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            if (command.Year == 2024)
                await _motorcyclePublisher.PublishAsync(command, "notification-queue");

            var lisencePlateExist = await _motorcycleRepository.GetByLicensePlateAsync(command.LicensePlate);
            if (lisencePlateExist != null)
                throw new DomainException("Already have a motorcycle with this plate.");

            await _motorcyclePublisher.PublishAsync(command, "motorcycle-queue");

            return new CreateMotorcycleResult();
        }
    }
}
