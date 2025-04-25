using AutoMapper;
using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Repositories;
using AutomobileRentalManagementAPI.Domain.Repositories.DeliveryPersons;
using FluentValidation;
using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Locations.CreateLocation
{
    public class CreateLocationHandler : IRequestHandler<CreateLocationCommand, CreatedLocationResult>
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;
        private readonly IDeliveryPersonRepository _deliveryPersonRepository;

        public CreateLocationHandler(
            IMapper mapper,
            ILocationRepository locationRepository, 
            IDeliveryPersonRepository deliveryPersonRepository)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
            _deliveryPersonRepository = deliveryPersonRepository;
        }

        public async Task<CreatedLocationResult> Handle(CreateLocationCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateLocationCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            DeliveryPerson? deliveryPerson = await _deliveryPersonRepository.GetByIdAsync(command.IdDeliveryPerson, cancellationToken);
            if (deliveryPerson == null) 
                throw new DomainException("No delivery person was found for the specified location.");

            if (!deliveryPerson.CanCreateLocation())
                throw new DomainException("Cannot create a location unless the delivery person's license type is 'A'.");

            var mappedEntity = _mapper.Map<Location>(command);
            int daysToAdd = (int)mappedEntity.Plan;
            mappedEntity.NavigationId = Guid.NewGuid();
            mappedEntity.EstimatedEndDate = mappedEntity.StartDate.AddDays(daysToAdd);
            mappedEntity.CalculateValues();

            var createdLocation = await _locationRepository.AddAsync(mappedEntity, cancellationToken);
            return _mapper.Map<CreatedLocationResult>(createdLocation);
        }
    }
}