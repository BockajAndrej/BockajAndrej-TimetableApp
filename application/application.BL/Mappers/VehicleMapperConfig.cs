using application.BL.Models.Details;
using application.BL.Models.Lists;
using application.DAL.Entities;
using AutoMapper;

namespace application.BL.Mappers;

public class VehicleMapperConfigv : VehicleDetailMapperConfig
{
    public VehicleMapperConfigv()
    {
        CreateMap<Vehicle, VehicleListModel>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.VehicleName,
                opt => opt.MapFrom(src => src.VehicleName));

        CreateMap<VehicleListModel, Vehicle>()
            .ForMember(dest => dest.Transports,
                opt => opt.Ignore());
    }
}