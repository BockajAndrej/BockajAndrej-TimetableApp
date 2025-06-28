using application.BL.Models.RelationSet;
using application.DAL.Entities;
using AutoMapper;

namespace application.BL.Mappers.RelationSet;

public class TransportListMapper : Profile
{
    public TransportListMapper()
    {
        CreateMap<Transport, TransportListModel>()
            .ForMember(dest => dest.Cp,
                opt => opt.MapFrom(src => src.IdCpNavigation))
            .ForMember(dest => dest.Vehicle,
                opt => opt.MapFrom(src => src.IdVehicleNavigation));

        CreateMap<TransportListModel, Transport>()
            .ForMember(dest => dest.IdCpNavigation,
                opt => opt.MapFrom(src => src.Cp))
            .ForMember(dest => dest.IdCp,
                opt => opt.MapFrom(src => src.Cp.Id))
            .ForMember(dest => dest.IdVehicleNavigation,
                opt => opt.MapFrom(src => src.Vehicle))
            .ForMember(dest => dest.IdVehicle,
                opt => opt.MapFrom(src => src.Vehicle.Id));
    }
}