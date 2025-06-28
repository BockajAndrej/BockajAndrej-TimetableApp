using application.BL.Models.Details;
using application.DAL.Entities;
using AutoMapper;

namespace application.BL.Mappers.Details;

public class CpDetailMapperConfig : Profile
{
    //Todo: Change structure. In Cpmapperconfig is same code need to be unified
    public CpDetailMapperConfig()
    {
        CreateMap<Cp, CpDetailModel>()
            .ForMember(dest => dest.VehicleList,
                opt => opt.MapFrom(src => src.Transports.Select(t => t.IdVehicleNavigation)))
            .ForMember(dest => dest.TransportList,
                opt => opt.MapFrom(src => src.Transports));

        CreateMap<CpDetailModel, Cp>()
            .ForMember(dest => dest.Transports,
                opt => opt.MapFrom(src => src.TransportList));


    }
}