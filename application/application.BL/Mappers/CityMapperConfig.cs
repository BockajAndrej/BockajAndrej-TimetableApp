using application.BL.Models.Details;
using application.DAL.Entities;
using AutoMapper;

namespace application.BL.Mappers;

public class CityMapperConfig : Profile
{
    public CityMapperConfig()
    {
        CreateMap<City, CityDetailModel>()
            .ForMember(dest => dest.TripsStarted,
                opt => opt.MapFrom(src => src.CpIdStartCityNavigations))
            .ForMember(dest => dest.TripsEnded,
                opt => opt.MapFrom(src => src.CpIdEndCityNavigations));

        CreateMap<CityDetailModel, City>()
            .ForMember(dest => dest.CpIdStartCityNavigations,
                opt => opt.MapFrom(src => src.TripsStarted))
            .ForMember(dest => dest.CpIdStartCityNavigations, 
                opt => opt.MapFrom(src => src.TripsEnded));
    }
}