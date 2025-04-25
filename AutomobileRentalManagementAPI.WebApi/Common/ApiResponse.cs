using AutomobileRentalManagementAPI.CrossCutting.Validation;
using FluentValidation.Results;

namespace AutomobileRentalManagementAPI.WebApi.Common
{
    public class ApiResponse
    {
        public bool success { get; set; }
        public string mensagem { get; set; } = string.Empty;
        public IEnumerable<ValidationFailure> errors { get; set; } = [];
    }
}