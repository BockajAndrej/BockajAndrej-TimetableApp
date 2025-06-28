using application.BL.Models.Details;
using application.DAL.Entities;
using application.DAL.Factories;
using AutoMapper;

namespace application.BL.Facades;

public class CityFacade(
    DbContexCpFactory factory, 
    IMapper mapperProfile
) : Facade<City, CityDetailModel>(factory, mapperProfile)
{
    
}