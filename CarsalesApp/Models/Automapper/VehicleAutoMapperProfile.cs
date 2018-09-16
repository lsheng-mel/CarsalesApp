using AutoMapper;
using CarsalesApp.Models.DTO;

namespace CarsalesApp.Models.Automapper
{
    public class VehicleAutoMapperProfile : Profile
    {
        public VehicleAutoMapperProfile()
        {
            CreateMap<Vehicle, DealerVehicleDTO>();
            CreateMap<Vehicle, NonDealerVehicleDTO>();
        }
    }
}
