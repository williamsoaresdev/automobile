using AutoMapper;
using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using FluentValidation;
using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.UpdateMotorcycle
{
    public class UpdateMotorcycleHandler : IRequestHandler<UpdateMotorcycleCommand, UpdateMotorcycleResult>
    {
        private readonly IMotorcycleRepository _repository;
        private readonly IMapper _mapper;

        public UpdateMotorcycleHandler(IMotorcycleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UpdateMotorcycleResult> Handle(UpdateMotorcycleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateMotorcycleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var motorcycle = await _repository.GetByIdAsync(command.NavigationId, cancellationToken);
            if (motorcycle is null)
                throw new DomainException("Motorcycle not found");

            motorcycle.LicensePlate = command.LicensePlate;

            await _repository.UpdateAsync(motorcycle, cancellationToken);

            return _mapper.Map<UpdateMotorcycleResult>(motorcycle);
        }
    }
}
