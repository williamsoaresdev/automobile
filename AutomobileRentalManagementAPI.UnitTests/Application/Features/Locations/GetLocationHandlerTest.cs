using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Locations.GetLocation;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Repositories;
using AutomobileRentalManagementAPI.TestTools.Application;
using NSubstitute;
using Xunit;

namespace AutomobileRentalManagementAPI.UnitTests.Application.Features.Locations
{
    public class GetLocationHandlerTest
    {
        private readonly IMapper _mapperMock;
        private readonly ILocationRepository _locationRepositoryMock;
        private readonly GetLocationHandler _handler;

        public GetLocationHandlerTest()
        {
            _mapperMock = Substitute.For<IMapper>();
            _locationRepositoryMock = Substitute.For<ILocationRepository>();
            _handler = new GetLocationHandler(_mapperMock, _locationRepositoryMock);
        }

        [Fact(DisplayName = "It should return mapped result when location exists.")]
        public async Task Given_ValidNavigationId_When_Handle_Then_ReturnsLocation()
        {
            // Arrange
            var navigationId = Guid.NewGuid();
            var command = new GetLocationCommand { NavigationId = navigationId };

            var location = LocationFakerUtils.GenerateValidDomainEntity();
            var expectedResult = new GetLocationResult { NavigationId = navigationId };

            _locationRepositoryMock.GetByIdAsync(navigationId, Arg.Any<CancellationToken>())
                .Returns(location);

            _mapperMock.Map<GetLocationResult>(location).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult.NavigationId, result.NavigationId);
        }

        [Fact(DisplayName = "It should return null when location does not exist.")]
        public async Task Given_InvalidNavigationId_When_Handle_Then_ReturnsNull()
        {
            // Arrange
            var navigationId = Guid.NewGuid();
            var command = new GetLocationCommand { NavigationId = navigationId };

            _locationRepositoryMock.GetByIdAsync(navigationId, Arg.Any<CancellationToken>())
                .Returns((Location?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}