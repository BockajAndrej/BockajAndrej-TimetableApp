
using application.BL.Mappers.Details;
using application.BL.Models;
using application.BL.Models.Details;
using application.BL.Models.Lists;
using application.DAL.Entities;
using AutoMapper;

namespace application.BL.Mappers;

public class CpMapperConfig : CpDetailMapperConfig
{
    public CpMapperConfig()
    {
        CreateMap<Cp, CpListModel>()
            .ForMember(dest => dest.IdEmployee,
                opt => opt.MapFrom(src => src.IdEmployee))
            .ForMember(dest => dest.IdStartCity,
                opt => opt.MapFrom(src => src.IdStartCity))
            .ForMember(dest => dest.IdEndCity,
                opt => opt.MapFrom(src => src.IdEndCity));
        
        CreateMap<CpListModel, Cp>()
            .ForMember(dest => dest.Transports,
                opt => opt.Ignore());
    }
}