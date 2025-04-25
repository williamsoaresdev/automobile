using AutomobileRentalManagementAPI.Application.Features.Motorcycles.DeleteMotorcycle;
using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Repositories;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using AutomobileRentalManagementAPI.TestTools.Application;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace AutomobileRentalManagementAPI.UnitTests.Application.Features.Motorcycles
{
    public class DeleteMotorcycleHandlerTest
    {
        private readonly IMotorcycleRepository _motorcycleRepoMock = Substitute.For<IMotorcycleRepository>();
        private readonly ILocationRepository _locationRepoMock = Substitute.For<ILocationRepository>();
        private readonly DeleteMotorcycleHandler _handler;

        public DeleteMotorcycleHandlerTest()
        {
            _handler = new DeleteMotorcycleHandler(_motorcycleRepoMock, _locationRepoMock);
        }

        [Fact(DisplayName = "Should delete when no linked rentals.")]
        public async Task Given_ValidCommandWithoutLocation_When_Handle_Then_DeletesSuccessfully()
        {
            var command = new DeleteMotorcycleCommand(Guid.NewGuid());
            var entity = MotorcycleFakerUtils.GenerateValidDomainEntity();

            _locationRepoMock.HasAnyWithMotorcycleAsync(command.NavigationId).Returns(false);
            _motorcycleRepoMock.GetByIdAsync(command.NavigationId, Arg.Any<CancellationToken>()).Returns(entity);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Equal(Unit.Value, result);
            await _motorcycleRepoMock.Received().DeleteAsync(entity, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Should throw if linked to a rental.")]
        public async Task Given_LinkedRental_When_Handle_Then_ThrowsInvalidOperation()
        {
            var command = new DeleteMotorcycleCommand(Guid.NewGuid());

            _locationRepoMock.HasAnyWithMotorcycleAsync(command.NavigationId).Returns(true);

            var ex = await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("It is not possible to remove the motorcycle. It is linked to one or more rentals.", ex.Message);
        }

        [Fact(DisplayName = "Should throw if motorcycle not found.")]
        public async Task Given_InvalidId_When_Handle_Then_ThrowsNotFound()
        {
            var command = new DeleteMotorcycleCommand(Guid.NewGuid());

            _locationRepoMock.HasAnyWithMotorcycleAsync(command.NavigationId).Returns(false);
            _motorcycleRepoMock.GetByIdAsync(command.NavigationId, Arg.Any<CancellationToken>()).Returns((Motorcycle?)null);

            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact(DisplayName = "Should throw ValidationException when command is invalid.")]
        public async Task Given_InvalidCommand_When_Handle_Then_ThrowsValidationException()
        {
            var command = new DeleteMotorcycleCommand(Guid.Empty);
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
