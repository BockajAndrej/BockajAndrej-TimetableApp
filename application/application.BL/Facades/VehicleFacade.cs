using application.BL.Models.Details;
using application.DAL.Entities;
using application.DAL.Factories;
using AutoMapper;

namespace application.BL.Facades;

public class VehicleFacade(
    DbContexCpFactory factory, 
    IMapper mapperProfile
) : Facade<Vehicle, VehicleDetailModel>(factory, mapperProfile)
{
    
}