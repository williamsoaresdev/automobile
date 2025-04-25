using AutoMapper;
using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using AutomobileRentalManagementAPI.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Locations.PutLocation
{
    public class UpdateLocationHandler : IRequestHandler<UpdateLocationCommand, UpdatedLocationResult>
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;

        public UpdateLocationHandler(
            IMapper mapper,
            ILocationRepository locationRepository
        )
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }

        public async Task<UpdatedLocationResult> Handle(UpdateLocationCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateLocationCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var location = await _locationRepository.GetByIdAsync(command.NavigationId, cancellationToken);
            if (location == null) throw new DomainException("Location not found");
            
            location.DevolutionDate = command.DevolutionDate;
            location.CalculateValues();

            var updatedLocation = await _locationRepository.UpdateAsync(location, cancellationToken);

            return _mapper.Map<UpdatedLocationResult>(updatedLocation);
        }
    }
}