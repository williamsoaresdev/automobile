using AutoMapper;
using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.HttpRepositories;
using AutomobileRentalManagementAPI.Domain.Repositories.DeliveryPersons;
using FluentValidation;
using MediatR;

namespace AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.UpdateLicensePlatePhoto
{
    public class UpdateLicensePlatePhotoHandler : IRequestHandler<UpdateLicensePlatePhotoCommand, UpdateLicensePlatePhotoResult>
    {
        private readonly IBlobHttpRepository _blobHttpRepository;
        private readonly IDeliveryPersonRepository _deliveryPersonRepository;
        private readonly IMapper _mapper;

        public UpdateLicensePlatePhotoHandler(
             IBlobHttpRepository blobHttpRepository,
             IDeliveryPersonRepository deliveryPersonRepository,
             IMapper mapper)
        {
            _blobHttpRepository = blobHttpRepository;
            _deliveryPersonRepository = deliveryPersonRepository;
            _mapper = mapper;
        }

        public async Task<UpdateLicensePlatePhotoResult> Handle(UpdateLicensePlatePhotoCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLicensePlatePhotoCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            DeliveryPerson? deliveryPerson = await _deliveryPersonRepository.GetByIdAsync(request.NavigationId, cancellationToken);
            if (deliveryPerson == null) throw new DomainException("User not found.");

            var newImgUrl = _blobHttpRepository.UploadBase64FileAndReturnPublicUrl(request.LicenseImageBase64);
            if(string.IsNullOrEmpty(newImgUrl)) throw new DomainException("Failed to upload cnh photo.");

            deliveryPerson.LicenseImageUrl = newImgUrl;
            var updatedEntity = await _deliveryPersonRepository.UpdateAsync(deliveryPerson, cancellationToken);
            var mappedResponse = _mapper.Map<UpdateLicensePlatePhotoResult>(updatedEntity);

            return mappedResponse;
        }
    }
}