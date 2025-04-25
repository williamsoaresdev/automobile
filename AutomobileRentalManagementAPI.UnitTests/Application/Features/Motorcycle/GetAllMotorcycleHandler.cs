using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetAllMotorcycles;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using AutomobileRentalManagementAPI.TestTools.Application;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace AutomobileRentalManagementAPI.UnitTests.Application.Features.Motorcycles
{
    public class GetAllMotorcyclesHandlerTest
    {
        private readonly IMotorcycleRepository _repoMock = Substitute.For<IMotorcycleRepository>();
        private readonly IMapper _mapperMock = Substitute.For<IMapper>();
        private readonly GetAllMotorcyclesHandler _handler;

        public GetAllMotorcyclesHandlerTest()
        {
            _handler = new GetAllMotorcyclesHandler(_repoMock, _mapperMock);
        }

        [Fact(DisplayName = "Should return all motorcycles filtered by plate.")]
        public async Task Given_CommandWithFilter_When_Handle_Then_ReturnsList()
        {
            var motorcycles = MotorcycleFakerUtils.GenerateValidDomainEntity(3);
            var items = new List<MotorcycleItem> { new(), new(), new() };
            var command = new GetAllMotorcyclesCommand { LicensePlate = "XYZ" };

            _repoMock.GetAllAsync(command.LicensePlate, Arg.Any<CancellationToken>()).Returns(motorcycles);
            _mapperMock.Map<List<MotorcycleItem>>(motorcycles).Returns(items);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(3, result.Items.Count);
        }

        [Fact(DisplayName = "Should throw validation exception when command is invalid.")]
        public async Task Given_InvalidCommand_When_Handle_Then_ThrowsValidationException()
        {
            var command = new GetAllMotorcyclesCommand { LicensePlate = new string('X', 100) };

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
