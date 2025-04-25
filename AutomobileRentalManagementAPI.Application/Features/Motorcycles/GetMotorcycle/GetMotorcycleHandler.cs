using AutoMapper;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using FluentValidation;
using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetMotorcycle
{
    public class GetMotorcycleHandler : IRequestHandler<GetMotorcycleCommand, GetMotorcycleResult>
    {
        private readonly IMotorcycleRepository _repository;
        private readonly IMapper _mapper;

        public GetMotorcycleHandler(IMotorcycleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetMotorcycleResult> Handle(GetMotorcycleCommand command, CancellationToken cancellationToken)
        {
            var validator = new GetMotorcycleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var entity = await _repository.GetByIdAsync(command.NavigationId, cancellationToken);
            if (entity == null)
                return null!;

            return _mapper.Map<GetMotorcycleResult>(entity);
        }
    }
}
