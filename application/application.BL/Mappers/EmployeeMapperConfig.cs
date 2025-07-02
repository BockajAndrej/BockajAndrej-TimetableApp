using application.BL.Models;
using application.BL.Models.Details;
using application.DAL.Entities;
using AutoMapper;

namespace application.BL.Mappers;

public class EmployeeMapperConfig : Profile
{
    public EmployeeMapperConfig()
    {
        CreateMap<Employee, EmployeeDetailModel>()
            .ForMember(dest => dest.Trips,
                opt => opt.MapFrom(src => src.Cps));

        //Inversion functionality
        CreateMap<EmployeeDetailModel, Employee>()
            .ForMember(dest => dest.Cps,
                opt => opt.MapFrom(src => src.Trips));
    }
}