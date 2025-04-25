using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using AutomobileRentalManagementAPI.Domain.Repositories;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using FluentValidation;
using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.DeleteMotorcycle
{
    public class DeleteMotorcycleHandler : IRequestHandler<DeleteMotorcycleCommand, Unit>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly ILocationRepository _locationRepository;

        public DeleteMotorcycleHandler(
            IMotorcycleRepository motorcycleRepository,
            ILocationRepository locationRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _locationRepository = locationRepository;
        }

        public async Task<Unit> Handle(DeleteMotorcycleCommand command, CancellationToken cancellationToken)
        {
            var validator = new DeleteMotorcycleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var hasLocation = await _locationRepository.HasAnyWithMotorcycleAsync(command.NavigationId, cancellationToken);
            if (hasLocation)
                throw new DomainException("It is not possible to remove the motorcycle. It is linked to one or more rentals.");

            var motorcycle = await _motorcycleRepository.GetByIdAsync(command.NavigationId, cancellationToken);
            if (motorcycle is null)
                throw new DomainException("Motorcycle not found");

            await _motorcycleRepository.DeleteAsync(motorcycle, cancellationToken);

            return Unit.Value;
        }
    }
}