using application.BL.Models.Details;
using application.DAL;
using application.DAL.Entities;
using application.DAL.Factories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace application.BL.Facades;

public class CityFacade(
    IDbContextFactory<MyDbContext> factory,
    IMapper mapperProfile
) : Facade<City, CityDetailModel, MyDbContext>(factory, mapperProfile)
{

}