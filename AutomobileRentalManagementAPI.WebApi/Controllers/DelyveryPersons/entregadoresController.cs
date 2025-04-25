using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.CreateDeliveryPerson;
using AutomobileRentalManagementAPI.Application.Features.DeliveryPersons.UpdateLicensePlatePhoto;
using AutomobileRentalManagementAPI.WebApi.Common;
using AutomobileRentalManagementAPI.WebApi.Controllers.DelyveryPersons.Create;
using AutomobileRentalManagementAPI.WebApi.Controllers.DelyveryPersons.UpdateLicensePlatePhoto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.DelyveryPersons
{
    public class entregadoresController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public entregadoresController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateDeliveryPersonRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreateDeliveryPersonRequestValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) return BadRequest(new ApiResponse()
            {
                success = false,
                mensagem = "Dados inválidos",
                errors = validationResult.Errors
            });

            var command = _mapper.Map<CreateDeliveryPersonCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);
            var mappedResponse = _mapper.Map<CreateDeliveryPersonResponse>(response);

            return Created($"/get/{mappedResponse.NavigationId}", mappedResponse);
        }

        [HttpPost("{id}/cnh")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateLicensePlatePhoto([FromRoute] string id, [FromBody] UpdateLicensePlatePhotoRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateLicensePlatePhotoRequestValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateLicensePlatePhotoCommand>(request);
            command.NavigationId = Guid.Parse(id)
                ;
            var response = await _mediator.Send(command, cancellationToken);
            var mappedResponse = _mapper.Map<UpdateLicensePlatePhotoResponse>(response);

            return Created($"/get/{mappedResponse.NavigationId}", null);
        }
    }
}