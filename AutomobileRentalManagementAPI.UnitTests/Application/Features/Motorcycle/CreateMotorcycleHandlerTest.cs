using AutomobileRentalManagementAPI.Application.Features.Motorcycles.CreateMotorcycle;
using AutomobileRentalManagementAPI.Application.MessageQueue.Interfaces;
using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using AutomobileRentalManagementAPI.TestTools.Application;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace AutomobileRentalManagementAPI.UnitTests.Application.Features.Motorcycles
{
    public class CreateMotorcycleHandlerTest
    {
        private readonly IMotorcycleRepository _repositoryMock = Substitute.For<IMotorcycleRepository>();
        private readonly IMotorcyclePublisher _publisherMock = Substitute.For<IMotorcyclePublisher>();
        private readonly CreateMotorcycleHandler _handler;

        public CreateMotorcycleHandlerTest()
        {
            _handler = new CreateMotorcycleHandler(_publisherMock, _repositoryMock);
        }

        [Fact(DisplayName = "Should create and publish motorcycle when command is valid and plate does not exist.")]
        public async Task Given_ValidCommand_When_Handle_Then_PublishesAndReturnsResult()
        {
            // Arrange
            var command = MotorcycleFakerUtils.GenerateValidCreateCommand();
            _repositoryMock.GetByLicensePlateAsync(command.LicensePlate).Returns((Motorcycle?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            await _publisherMock.Received().PublishAsync(command, "motorcycle-queue");

            if (command.Year == 2024)
                await _publisherMock.Received().PublishAsync(command, "notification-queue");
        }

        [Fact(DisplayName = "Should throw ValidationException when command is invalid.")]
        public async Task Given_InvalidCommand_When_Handle_Then_ThrowsValidationException()
        {
            // Arrange
            var command = new CreateMotorcycleCommand
            {
                Identifier = "",
                Model = "",
                Year = 0,
                LicensePlate = ""
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact(DisplayName = "Should throw DomainException when plate already exists.")]
        public async Task Given_DuplicatePlate_When_Handle_Then_ThrowsDomainException()
        {
            // Arrange
            var command = MotorcycleFakerUtils.GenerateValidCreateCommand();
            _repositoryMock.GetByLicensePlateAsync(command.LicensePlate).Returns(new Motorcycle());

            // Act & Assert
            var ex = await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Already have a motorcycle with this plate.", ex.Message);
        }
    }
}
