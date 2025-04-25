using AutoMapper;
using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.HttpRepositories;
using AutomobileRentalManagementAPI.Domain.Repositories.DeliveryPersons;
using FluentValidation;
using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.CreateDeliveryPerson
{
    public class CreateDeliveryPersonHandler : IRequestHandler<CreateDeliveryPersonCommand, CreatedDeliveryPersonResult>
    {
        private readonly IBlobHttpRepository _blobHttpRepository;
        private readonly IDeliveryPersonRepository _deliveryPersonRepository;
        private readonly IMapper _mapper;

        public CreateDeliveryPersonHandler(
            IBlobHttpRepository blobHttpRepository, 
            IDeliveryPersonRepository deliveryPersonRepository,
            IMapper mapper)
        {
            _blobHttpRepository = blobHttpRepository;
            _deliveryPersonRepository = deliveryPersonRepository;
            _mapper = mapper;
        }

        public async Task<CreatedDeliveryPersonResult> Handle(CreateDeliveryPersonCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateDeliveryPersonCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid) 
                throw new ValidationException(validationResult.Errors);

            bool hasAnyRegister = await _deliveryPersonRepository.HasPreviousRegister(command.Cnpj, command.LicenseNumber);
            if(hasAnyRegister)
                throw new DomainException("Already have a delivery person with this data.");

            var imgPublicURL = string.Empty;
            if (!string.IsNullOrEmpty(command.LicenseImageBase64))
            {
                imgPublicURL = _blobHttpRepository.UploadBase64FileAndReturnPublicUrl(command.LicenseImageBase64);
                if (string.IsNullOrEmpty(imgPublicURL))
                    throw new DomainException("Failed to upload cnh photo.");
            }

            var mappedResult = _mapper.Map<DeliveryPerson>(command);
            mappedResult.LicenseImageUrl = imgPublicURL;
            mappedResult.NavigationId = Guid.NewGuid();

            var createdDeliveryPerson = await _deliveryPersonRepository.AddAsync(mappedResult, cancellationToken);
            
            return _mapper.Map<CreatedDeliveryPersonResult>(createdDeliveryPerson);
        }
    }
}