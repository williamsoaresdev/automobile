using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.CreateDeliveryPerson;
using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Enums;
using AutomobileRentalManagementAPI.Domain.HttpRepositories;
using AutomobileRentalManagementAPI.Domain.Repositories.DeliveryPersons;
using AutomobileRentalManagementAPI.TestTools;
using AutomobileRentalManagementAPI.TestTools.Application;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace AutomobileRentalManagementAPI.UnitTests.Application.Features.DeliveryPersons
{
    public class CreateDeliveryPersonHandlerTest
    {
        private readonly IBlobHttpRepository _blobHttpRepositoryMock;
        private readonly IDeliveryPersonRepository _repositoryMock;
        private readonly IMapper _mapperMock;
        private readonly CreateDeliveryPersonHandler _handler;

        public CreateDeliveryPersonHandlerTest()
        {
            _blobHttpRepositoryMock = Substitute.For<IBlobHttpRepository>();
            _repositoryMock = Substitute.For<IDeliveryPersonRepository>();
            _mapperMock = Substitute.For<IMapper>();
            _handler = new CreateDeliveryPersonHandler(_blobHttpRepositoryMock, _repositoryMock, _mapperMock);
        }

        [Fact(DisplayName = "It should return ok when handle received a valid command.")]
        public async Task Given_ValidCommand_When_Handle_Then_ReturnsResult()
        {
            // Arrange
            var publicImgUrl = "https://i.pravatar.cc/150?img=33";
            var entity = DeliveryPersonFakerUtils.GenerateValidDomainEntity();
            var command = DeliveryPersonFakerUtils.GenerateValidCommand();
            var expectedResult = TestUtils.CustomConvert<DeliveryPerson, CreatedDeliveryPersonResult>(entity);
            
            _blobHttpRepositoryMock.UploadBase64FileAndReturnPublicUrl(Arg.Any<string>()).Returns(publicImgUrl);
            _mapperMock.Map<DeliveryPerson>(Arg.Any<CreateDeliveryPersonCommand>()).Returns(entity);
            _repositoryMock.AddAsync(Arg.Any<DeliveryPerson>(), Arg.Any<CancellationToken>()).Returns(entity);
            _mapperMock.Map<CreatedDeliveryPersonResult>(entity).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(expectedResult);
            Assert.Equal(expectedResult.Identifier, result.Identifier);
            Assert.Equal(expectedResult.Name, result.Name);
            Assert.Equal(expectedResult.Cnpj, result.Cnpj);
            Assert.Equal(expectedResult.BirthDate, result.BirthDate);
            Assert.Equal(expectedResult.LicenseNumber, result.LicenseNumber);
            Assert.Equal(expectedResult.LicenseType, result.LicenseType);
            Assert.Equal(expectedResult.LicenseImageUrl, result.LicenseImageUrl);
        }

        [Fact(DisplayName = "It should throw ValidationException with all expected errors when command is invalid.")]
        public async Task Given_InvalidCommand_When_Handle_Then_ReturnsAllValidationErrors()
        {
            // Arrange
            var invalidCommand = new CreateDeliveryPersonCommand
            {
                Identifier = string.Empty, 
                Name = string.Empty, 
                Cnpj = "invalid_cnpj", 
                BirthDate = DateTime.UtcNow.AddDays(1), 
                LicenseNumber = "ABC123",
                LicenseType = CnhType.A, 
                LicenseImageBase64 = string.Empty 
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() =>
                _handler.Handle(invalidCommand, CancellationToken.None));

            var errorMessages = exception.Errors.Select(e => e.ErrorMessage).ToList();

            Assert.Contains("Identifier is required.", errorMessages);
            Assert.Contains("Name is required.", errorMessages);
            Assert.Contains("CNPJ is invalid.", errorMessages);
            Assert.Contains("Birth date must be in the past.", errorMessages);
            Assert.Contains("Driver's license number must be 11 digits.", errorMessages);
            Assert.Contains("Driver's license number must contain only digits.", errorMessages);
            Assert.Contains("Driver's license image is required.", errorMessages);
        }

        [Fact(DisplayName = "It should throw DomainException when CNPJ or LicenseNumber already exists.")]
        public async Task Given_DuplicatedCnpjOrCnh_When_Handle_Then_ThrowsVDomainException()
        {
            // Arrange
            var command = DeliveryPersonFakerUtils.GenerateValidCommand();

            _repositoryMock.HasPreviousRegister(command.Cnpj, command.LicenseNumber)
                .Returns(true);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Already have a delivery person with this data.", ex.Message);
        }

        [Fact(DisplayName = "It should throw DomainException when CNH image upload fails.")]
        public async Task Given_ImageUploadFails_When_Handle_Then_ThrowsVDomainException()
        {
            // Arrange
            var command = DeliveryPersonFakerUtils.GenerateValidCommand();

            _repositoryMock.HasPreviousRegister(command.Cnpj, command.LicenseNumber)
                .Returns(false);

            _blobHttpRepositoryMock.UploadBase64FileAndReturnPublicUrl(command.LicenseImageBase64)
                .Returns((string?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Failed to upload cnh photo.", ex.Message);
        }
    }
}