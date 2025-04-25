using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.UpdateMotorcycle;
using AutomobileRentalManagementAPI.Domain.CustomExceptions;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using AutomobileRentalManagementAPI.TestTools.Application;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace AutomobileRentalManagementAPI.UnitTests.Application.Features.Motorcycles
{
    public class UpdateMotorcycleHandlerTest
    {
        private readonly IMotorcycleRepository _repositoryMock = Substitute.For<IMotorcycleRepository>();
        private readonly IMapper _mapperMock = Substitute.For<IMapper>();
        private readonly UpdateMotorcycleHandler _handler;

        public UpdateMotorcycleHandlerTest()
        {
            _handler = new UpdateMotorcycleHandler(_repositoryMock, _mapperMock);
        }

        [Fact(DisplayName = "Should update motorcycle plate when command is valid.")]
        public async Task Given_ValidCommand_When_Handle_Then_ReturnsResult()
        {
            // Arrange
            var entity = MotorcycleFakerUtils.GenerateValidDomainEntity();
            var command = MotorcycleFakerUtils.GenerateValidUpdateCommand();
            entity.NavigationId = command.NavigationId;

            var expectedResult = MotorcycleFakerUtils.GenerateValidUpdateResult();

            _repositoryMock.GetByIdAsync(command.NavigationId, Arg.Any<CancellationToken>()).Returns(entity);
            _repositoryMock.UpdateAsync(entity, Arg.Any<CancellationToken>()).Returns(entity);
            _mapperMock.Map<UpdateMotorcycleResult>(entity).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult.LicensePlate, result.LicensePlate);
        }

        [Fact(DisplayName = "Should throw ValidationException when command is invalid.")]
        public async Task Given_InvalidCommand_When_Handle_Then_ThrowsValidationException()
        {
            // Arrange
            var command = new UpdateMotorcycleCommand
            {
                NavigationId = Guid.Empty,
                LicensePlate = ""
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact(DisplayName = "Should throw DomainException when motorcycle is not found.")]
        public async Task Given_NonexistentMotorcycle_When_Handle_Then_ThrowsDomainException()
        {
            // Arrange
            var command = MotorcycleFakerUtils.GenerateValidUpdateCommand();
            _repositoryMock.GetByIdAsync(command.NavigationId, Arg.Any<CancellationToken>()).Returns((Motorcycle?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
