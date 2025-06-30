using application.BL.Models.Details;
using application.DAL;
using application.DAL.Entities;
using application.DAL.Factories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace application.BL.Facades;

public class VehicleFacade(
    IDbContextFactory<MyDbContext> factory,
    IMapper mapperProfile
) : Facade<Vehicle, VehicleDetailModel, MyDbContext>(factory, mapperProfile)
{

}