using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Locations.CreateLocation;
using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Enums;
using AutomobileRentalManagementAPI.Domain.Repositories;
using AutomobileRentalManagementAPI.Domain.Repositories.DeliveryPersons;
using AutomobileRentalManagementAPI.TestTools.Application;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace AutomobileRentalManagementAPI.UnitTests.Application.Features.Locations
{
    public class CreateLocationHandlerTest
    {
        private readonly IMapper _mapperMock;
        private readonly ILocationRepository _locationRepositoryMock;
        private readonly IDeliveryPersonRepository _deliveryPersonRepositoryMock;
        private readonly CreateLocationHandler _handler;

        public CreateLocationHandlerTest()
        {
            _mapperMock = Substitute.For<IMapper>();
            _locationRepositoryMock = Substitute.For<ILocationRepository>();
            _deliveryPersonRepositoryMock = Substitute.For<IDeliveryPersonRepository>();
            _handler = new CreateLocationHandler(_mapperMock, _locationRepositoryMock, _deliveryPersonRepositoryMock);
        }

        [Fact(DisplayName = "It should return result when command is valid and delivery person can create location.")]
        public async Task Given_ValidCommand_When_Handle_Then_ReturnsResult()
        {
            // Arrange
            var command = LocationFakerUtils.GenerateValidCommand();
            var deliveryPerson = new DeliveryPerson { LicenseType = CnhType.A };
            var location = LocationFakerUtils.GenerateValidDomainEntity();
            var createdLocation = LocationFakerUtils.GenerateValidDomainEntity();
            var expectedResult = new CreatedLocationResult { NavigationId = createdLocation.NavigationId };

            _deliveryPersonRepositoryMock.GetByIdAsync(command.IdDeliveryPerson, Arg.Any<CancellationToken>())
                .Returns(deliveryPerson);

            _mapperMock.Map<Location>(command).Returns(location);
            _locationRepositoryMock.AddAsync(Arg.Any<Location>(), Arg.Any<CancellationToken>())
                .Returns(createdLocation);
            _mapperMock.Map<CreatedLocationResult>(createdLocation).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult.NavigationId, result.NavigationId);
        }

        [Fact(DisplayName = "It should throw ValidationException when command is invalid.")]
        public async Task Given_InvalidCommand_When_Handle_Then_ThrowsValidationException()
        {
            // Arrange
            var invalidCommand = new CreateLocationCommand();

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() =>
                _handler.Handle(invalidCommand, CancellationToken.None));

            Assert.NotEmpty(ex.Errors);
        }

        [Fact(DisplayName = "It should throw DomainException when delivery person is not found.")]
        public async Task Given_DeliveryPersonNotFound_When_Handle_Then_ThrowsDomainException()
        {
            // Arrange
            var command = LocationFakerUtils.GenerateValidCommand();

            _deliveryPersonRepositoryMock.GetByIdAsync(command.IdDeliveryPerson, Arg.Any<CancellationToken>())
                .Returns((DeliveryPerson?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<DomainException>(() =>
                _handler.Handle(command, CancellationToken.None));

            Assert.Equal("No delivery person was found for the specified location.", ex.Message);
        }

        [Fact(DisplayName = "It should throw DomainException when delivery person license is not 'A'.")]
        public async Task Given_DeliveryPersonCannotCreateLocation_When_Handle_Then_ThrowsDomainException()
        {
            // Arrange
            var command = LocationFakerUtils.GenerateValidCommand();
            var deliveryPerson = new DeliveryPerson { LicenseType = CnhType.B };

            _deliveryPersonRepositoryMock.GetByIdAsync(command.IdDeliveryPerson, Arg.Any<CancellationToken>())
                .Returns(deliveryPerson);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<DomainException>(() =>
                _handler.Handle(command, CancellationToken.None));

            Assert.Equal("Cannot create a location unless the delivery person's license type is 'A'.", ex.Message);
        }
    }
}