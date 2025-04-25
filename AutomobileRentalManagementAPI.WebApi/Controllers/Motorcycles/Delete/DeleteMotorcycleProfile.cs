using AutoMapper;
using AutomobileRentalManagementAPI.Application.Features.Motorcycles.DeleteMotorcycle;

namespace AutomobileRentalManagementAPI.WebApi.Controllers.Motorcycles.Delete
{
    public class DeleteMotorcycleProfile : Profile
    {
        public DeleteMotorcycleProfile()
        {
            CreateMap<DeleteMotorcycleRequest, DeleteMotorcycleCommand>();
        }
    }
}