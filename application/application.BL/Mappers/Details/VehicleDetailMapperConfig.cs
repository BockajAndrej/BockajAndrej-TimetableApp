using application.BL.Models.Details;
using application.DAL.Entities;
using AutoMapper;

namespace application.BL.Mappers;

public class VehicleDetailMapperConfig : Profile
{
    public VehicleDetailMapperConfig()
    {
        CreateMap<Vehicle, VehicleDetailModel>()
            .ForMember(dest => dest.CpList,
                opt => opt.MapFrom(src => src.Transports.Select(t => t.IdCpNavigation)))
            .ForMember(dest => dest.TransportList,
                opt => opt.MapFrom(src => src.Transports));


        //Inversion functionality
        CreateMap<VehicleDetailModel, Vehicle>()
            .ForMember(dest => dest.Transports,
                opt => opt.MapFrom(src => src.TransportList));
    }
}