using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Locations.CreateLocation;
using AutomobileRentalManagementAPI.Application.Features.Locations.GetLocation;
using AutomobileRentalManagementAPI.Application.Features.Locations.PutLocation;
using AutomobileRentalManagementAPI.WebApi.Common;
using AutomobileRentalManagementAPI.WebApi.Controllers.Locations.Create;
using AutomobileRentalManagementAPI.WebApi.Controllers.Locations.Get;
using AutomobileRentalManagementAPI.WebApi.Controllers.Locations.Put;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Locations
{
    public class locacaoController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public locacaoController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateLocationRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreateLocationRequestValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateLocationCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created($"/get/{response.NavigationId}", response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetLocationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] string id, CancellationToken cancellationToken)
        {
            Guid navigationId = Guid.Parse(id);
            var command = new GetLocationCommand();
            command.NavigationId = navigationId;

            var response = await _mediator.Send(command, cancellationToken);
            if(response == null) return NotFound();

            var mappedResponse = _mapper.Map<GetLocationResponse>(response);
            return OkRaw(mappedResponse);
        }

        [HttpPut("{id}/devolucao")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateLocationRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateLocationRequestValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateLocationCommand>(request);
            command.NavigationId = Guid.Parse(id);
            var response = await _mediator.Send(command, cancellationToken);

            var totalValueString = Convert.ToDecimal(response.TotalValue).ToString("N2");
            return Ok(new
            {
                mensagem = "Data de devolução informada com sucesso",
                valor = "Valor total: R$" + totalValueString
            });
        }
    }
}
