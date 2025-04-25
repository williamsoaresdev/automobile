using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.CreateMotorcycle;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.DeleteMotorcycle;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetAllMotorcycles;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.GetMotorcycle;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.UpdateMotorcycle;
using AutomobileRentalManagementAPI.WebApi.Common;
using AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Create;
using AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Delete;
using AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Get;
using AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.GetAll;
using AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Put;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles
{
    public class motosController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public motosController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateMotorcycleRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreateMotorcycleRequestValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) return BadRequest(new ApiResponse()
            {
                success = false,
                mensagem = "Dados inválidos",
                errors = validationResult.Errors
            });

            var command = _mapper.Map<CreateMotorcycleCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return Created($"/api/motos/", null);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetAllMotorcyclesResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllMotorcyclesRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await new GetAllMotorcyclesRequestValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)return BadRequest(new ApiResponse()
            {
                success = false,
                mensagem = "Dados inválidos",
                errors = validationResult.Errors
            });

            var command = _mapper.Map<GetAllMotorcyclesCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);
            var mapped = _mapper.Map<List<GetAllMotorcyclesResponse>>(result.Items);

            return OkRaw(mapped);
        }

        [HttpPut("{id}/placa")]
        [ProducesResponseType(typeof(UpdateMotorcycleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateMotorcyclePlateRequest placa, CancellationToken cancellationToken)
        {
            var request = new UpdateMotorcycleRequest()
            {
                LicensePlate = placa.placa,
                NavigationId = Guid.Parse(id),
            };

            var validationResult = await new UpdateMotorcycleRequestValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) return BadRequest(new ApiResponse()
            {
                success = false,
                mensagem = "Dados inválidos",
                errors = validationResult.Errors
            });

            var command = _mapper.Map<UpdateMotorcycleCommand>(request);
            await _mediator.Send(command, cancellationToken);
            string mensagem = "Placa modificada com sucesso";

            return OkRaw(new { mensagem });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetMotorcycleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] string id, CancellationToken cancellationToken)
        {
            var request = new GetMotorcycleRequest()
            {
                NavigationId = Guid.Parse(id),
            };

            var validationResult = await new GetMotorcycleRequestValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) return BadRequestRaw("Request mal formada");

            var command = _mapper.Map<GetMotorcycleCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);
            if (response == null) 
                return NotFoundRaw("Moto não encontrada");

            var mappedResponse = _mapper.Map<GetMotorcycleResponse>(response);

            return OkRaw(mappedResponse);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var request = new DeleteMotorcycleRequest()
            {
                NavigationId = Guid.Parse(id)
            };

            var validationResult = await new DeleteMotorcycleRequestValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) return BadRequestRaw("Dados inválidos");

            var command = _mapper.Map<DeleteMotorcycleCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return OkRaw();
        }
    }
}