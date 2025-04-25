using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetMotorcycle;
using AutomobileRentalManagementAPI.Domain.Entities;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using AutomobileRentalManagementAPI.TestTools.Application;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace AutomobileRentalManagementAPI.UnitTests.Application.Features.Motorcycles
{
    public class GetMotorcycleHandlerTest
    {
        private readonly IMotorcycleRepository _repoMock = Substitute.For<IMotorcycleRepository>();
        private readonly IMapper _mapperMock = Substitute.For<IMapper>();
        private readonly GetMotorcycleHandler _handler;

        public GetMotorcycleHandlerTest()
        {
            _handler = new GetMotorcycleHandler(_repoMock, _mapperMock);
        }

        [Fact(DisplayName = "Should return result when found.")]
        public async Task Given_ValidId_When_Handle_Then_ReturnsMapped()
        {
            var entity = MotorcycleFakerUtils.GenerateValidDomainEntity();
            var command = new GetMotorcycleCommand(entity.NavigationId);
            var expected = new GetMotorcycleResult();

            _repoMock.GetByIdAsync(command.NavigationId, Arg.Any<CancellationToken>()).Returns(entity);
            _mapperMock.Map<GetMotorcycleResult>(entity).Returns(expected);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "Should return null when not found.")]
        public async Task Given_InvalidId_When_Handle_Then_ReturnsNull()
        {
            var command = new GetMotorcycleCommand(Guid.NewGuid());

            _repoMock.GetByIdAsync(command.NavigationId, Arg.Any<CancellationToken>()).Returns((Motorcycle)null!);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact(DisplayName = "Should throw ValidationException if command invalid.")]
        public async Task Given_InvalidCommand_When_Handle_Then_ThrowsValidationException()
        {
            var command = new GetMotorcycleCommand(Guid.Empty);

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
