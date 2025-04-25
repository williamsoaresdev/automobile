using AutoMapper;
using AutomobileRentalManagementAPI.Domain.Repositories;
using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.Locations.GetLocation
{
    public class GetLocationHandler : IRequestHandler<GetLocationCommand, GetLocationResult>
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;

        public GetLocationHandler(
            IMapper mapper,
            ILocationRepository locationRepository
        )
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }

        public async Task<GetLocationResult> Handle(GetLocationCommand command, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(command.NavigationId, cancellationToken);
            return _mapper.Map<GetLocationResult>(location);
        }
    }
}