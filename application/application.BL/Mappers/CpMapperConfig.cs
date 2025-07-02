
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
        CreateMap<Cp, CpListModel>();
        //.ForMember(dest => dest.IdEmployeeNav,
        //    opt => opt.MapFrom(src => src.IdEmployeeNavigation))
        //.ForMember(dest => dest.IdStartCityNav,
        //    opt => opt.MapFrom(src => src.IdStartCityNavigation))
        //.ForMember(dest => dest.IdEndCityNav,
        //    opt => opt.MapFrom(src => src.IdEndCityNavigation));


        CreateMap<CpListModel, Cp>()
            .ForMember(dest => dest.Transports,
                opt => opt.Ignore());
    }
}