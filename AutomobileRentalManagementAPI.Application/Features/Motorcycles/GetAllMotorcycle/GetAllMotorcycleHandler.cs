using AutoMapper;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using FluentValidation;
using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetAllMotorcycles
{
    public class GetAllMotorcyclesHandler : IRequestHandler<GetAllMotorcyclesCommand, GetAllMotorcyclesResult>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMapper _mapper;

        public GetAllMotorcyclesHandler(IMotorcycleRepository motorcycleRepository, IMapper mapper)
        {
            _motorcycleRepository = motorcycleRepository;
            _mapper = mapper;
        }

        public async Task<GetAllMotorcyclesResult> Handle(GetAllMotorcyclesCommand command, CancellationToken cancellationToken)
        {
            var validator = new GetAllMotorcyclesValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var data = await _motorcycleRepository.GetAllAsync(command.LicensePlate, cancellationToken);
            var mapped = _mapper.Map<List<MotorcycleItem>>(data);

            return new GetAllMotorcyclesResult
            {
                Items = mapped
            };
        }
    }
}
