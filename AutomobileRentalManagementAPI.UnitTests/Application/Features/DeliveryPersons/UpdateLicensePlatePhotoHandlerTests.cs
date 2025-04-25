using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.UpdateLicensePlatePhoto;
using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.HttpRepositories;
using AutomobileRentalManagementAPI.Domain.Repositories.DeliveryPersons;
using AutomobileRentalManagementAPI.TestTools;
using AutomobileRentalManagementAPI.TestTools.Application;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace AutomobileRentalManagementAPI.UnitTests.Application.Features.DeliveryPersons
{
    public class UpdateLicensePlatePhotoHandlerTest
    {
        private readonly IBlobHttpRepository _blobHttpRepositoryMock;
        private readonly IDeliveryPersonRepository _repositoryMock;
        private readonly IMapper _mapperMock;
        private readonly UpdateLicensePlatePhotoHandler _handler;

        public UpdateLicensePlatePhotoHandlerTest()
        {
            _blobHttpRepositoryMock = Substitute.For<IBlobHttpRepository>();
            _repositoryMock = Substitute.For<IDeliveryPersonRepository>();
            _mapperMock = Substitute.For<IMapper>();
            _handler = new UpdateLicensePlatePhotoHandler(_blobHttpRepositoryMock, _repositoryMock, _mapperMock);
        }

        [Fact(DisplayName = "It should update photo when command is valid.")]
        public async Task Given_ValidCommand_When_Handle_Then_ReturnsResult()
        {
            // Arrange
            var entity = DeliveryPersonFakerUtils.GenerateValidDomainEntity();
            var command = new UpdateLicensePlatePhotoCommand
            {
                NavigationId = entity.NavigationId,
                LicenseImageBase64 = "base64data..."
            };
            var expectedResult = TestUtils.CustomConvert<DeliveryPerson, UpdateLicensePlatePhotoResult>(entity);

            _repositoryMock.GetByIdAsync(command.NavigationId, Arg.Any<CancellationToken>()).Returns(entity);
            _blobHttpRepositoryMock.UploadBase64FileAndReturnPublicUrl(command.LicenseImageBase64).Returns("http://url.com/image.png");
            _repositoryMock.UpdateAsync(Arg.Any<DeliveryPerson>(), Arg.Any<CancellationToken>()).Returns(entity);
            _mapperMock.Map<UpdateLicensePlatePhotoResult>(entity).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(expectedResult);
            Assert.Equal(expectedResult.NavigationId, result.NavigationId);
        }

        [Fact(DisplayName = "It should throw ValidationException when command is invalid.")]
        public async Task Given_InvalidCommand_When_Handle_Then_ThrowsValidationException()
        {
            // Arrange
            var command = new UpdateLicensePlatePhotoCommand
            {
                NavigationId = Guid.Empty,
                LicenseImageBase64 = ""
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("The cnh photo cannot be empty.", ex.Errors.Select(e => e.ErrorMessage));
        }

        [Fact(DisplayName = "It should throw DomainException when delivery person is not found.")]
        public async Task Given_NonexistentUser_When_Handle_Then_ThrowsDomainException()
        {
            // Arrange
            var command = new UpdateLicensePlatePhotoCommand
            {
                NavigationId = Guid.NewGuid(),
                LicenseImageBase64 = "base64"
            };

            _repositoryMock.GetByIdAsync(command.NavigationId, Arg.Any<CancellationToken>()).Returns((DeliveryPerson?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("User not found.", ex.Message);
        }

        [Fact(DisplayName = "It should throw DomainException when image upload fails.")]
        public async Task Given_UploadFails_When_Handle_Then_ThrowsDomainException()
        {
            // Arrange
            var entity = DeliveryPersonFakerUtils.GenerateValidDomainEntity();
            var command = new UpdateLicensePlatePhotoCommand
            {
                NavigationId = entity.NavigationId,
                LicenseImageBase64 = "base64"
            };

            _repositoryMock.GetByIdAsync(command.NavigationId, Arg.Any<CancellationToken>()).Returns(entity);
            _blobHttpRepositoryMock.UploadBase64FileAndReturnPublicUrl(command.LicenseImageBase64).Returns((string?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Failed to upload cnh photo.", ex.Message);
        }
    }
}
