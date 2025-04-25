using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Locations.PutLocation;
using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Enums;
using AutomobileRentalManagementAPI.Domain.Repositories;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace AutomobileRentalManagementAPI.UnitTests.Application.Features.Locations
{
    public class UpdateLocationHandlerTest
    {
        private readonly IMapper _mapperMock;
        private readonly ILocationRepository _locationRepositoryMock;
        private readonly UpdateLocationHandler _handler;

        public UpdateLocationHandlerTest()
        {
            _mapperMock = Substitute.For<IMapper>();
            _locationRepositoryMock = Substitute.For<ILocationRepository>();
            _handler = new UpdateLocationHandler(_mapperMock, _locationRepositoryMock);
        }

        [Fact(DisplayName = "It should update location and return mapped result when command is valid.")]
        public async Task Given_ValidCommand_When_Handle_Then_ReturnsUpdatedResult()
        {
            // Arrange
            var navigationId = Guid.NewGuid();
            var command = new UpdateLocationCommand
            {
                NavigationId = navigationId,
                DevolutionDate = DateTime.UtcNow.Date.AddDays(30)
            };

            var existingLocation = new Location
            {
                NavigationId = navigationId,
                StartDate = DateTime.UtcNow.Date,
                EstimatedEndDate = DateTime.UtcNow.Date.AddDays(30),
                Plan = LocationPlan.ThirtyDays
            };

            var updatedLocation = new Location { NavigationId = navigationId };
            var expectedResult = new UpdatedLocationResult { NavigationId = navigationId };

            _locationRepositoryMock.GetByIdAsync(navigationId, Arg.Any<CancellationToken>())
                .Returns(existingLocation);

            _locationRepositoryMock.UpdateAsync(existingLocation, Arg.Any<CancellationToken>())
                .Returns(updatedLocation);

            _mapperMock.Map<UpdatedLocationResult>(updatedLocation).Returns(expectedResult);

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
            var invalidCommand = new UpdateLocationCommand(); 

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() =>
                _handler.Handle(invalidCommand, CancellationToken.None));

            Assert.NotEmpty(ex.Errors);
        }

        [Fact(DisplayName = "It should throw DomainException when location is not found.")]
        public async Task Given_NonexistentLocation_When_Handle_Then_ThrowsDomainException()
        {
            // Arrange
            var command = new UpdateLocationCommand
            {
                NavigationId = Guid.NewGuid(),
                DevolutionDate = DateTime.UtcNow.Date
            };

            _locationRepositoryMock.GetByIdAsync(command.NavigationId, Arg.Any<CancellationToken>())
                .Returns((Location?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<DomainException>(() =>
                _handler.Handle(command, CancellationToken.None));

            Assert.Equal("Location not found", ex.Message);
        }
    }
}
