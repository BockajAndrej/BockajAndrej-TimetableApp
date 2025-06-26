using application.BL.Mappers;
using application.BL.Models;
using application.BL.Models.Details;
using application.DAL.Entities;
using application.DAL.Factories;
using AutoMapper;

namespace application.BL.Facades;

public class CpFacade(
    DbContexCpFactory factory, 
    IMapper mapperProfile
    ) : Facade<Cp, CpDetailModel>(factory, mapperProfile)
{
    
}