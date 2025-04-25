using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AutomobileRentalManagementAPI.WebApi.Common
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected int GetCurrentUserId() =>
           int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NullReferenceException());

        protected string GetCurrentUserEmail() =>
            User.FindFirst(ClaimTypes.Email)?.Value ?? throw new NullReferenceException();

        protected IActionResult Ok<T>(T data) =>
                base.Ok(new ApiResponseWithData<T> { Data = data, success = true });

        protected IActionResult OkRaw<T>(T data) => base.Ok(data);

        protected IActionResult OkRaw() => base.Ok();

        protected IActionResult Created<T>(string routeName, object routeValues, T data) =>
            base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, success = true });

        protected IActionResult BadRequest(string message) =>
            base.BadRequest(new ApiResponse { mensagem = message, success = false });
        protected IActionResult BadRequestRaw(string message) =>
            base.BadRequest(new { mensagem = message});

        protected IActionResult NotFound(string message = "Resource not found") =>
            base.NotFound(new ApiResponse { mensagem = message, success = false });

        protected IActionResult NotFoundRaw(string message = "Resource not found") =>
            base.NotFound(new { mensagem = message});

        protected IActionResult OkPaginated<T>(PaginatedList<T> pagedList) =>
                Ok(new PaginatedResponse<T>
                {
                    Data = pagedList,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    TotalCount = pagedList.TotalCount,
                    success = true
                });
    }
}
