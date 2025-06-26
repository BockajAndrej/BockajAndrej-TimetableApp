
using application.BL.Models;
using application.BL.Models.Lists;
using application.DAL.Entities;
using AutoMapper;

namespace application.BL.Mappers;

public class CpMapperConfig : Profile
{
    public CpMapperConfig()
    {
        CreateMap<Cp, CpListModel>()
            .ForMember(dest => dest.IdEmployee,
                opt => opt.MapFrom(src => src.IdEmployeeNavigation.Id))
            .ForMember(dest => dest.IdStartCity,
                opt => opt.MapFrom(src => src.IdStartCityNavigation.Id))
            .ForMember(dest => dest.IdEndCity,
                opt => opt.MapFrom(src => src.IdEndCityNavigation.Id));
    }
}