using application.BL.Models.RelationSet;
using application.DAL;
using application.DAL.Entities;
using application.DAL.Factories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace application.BL.Facades.RelationSet;

public class TransportFacade(
    IDbContextFactory<MyDbContext> factory,
    IMapper mapperProfile
) : Facade<Transport, TransportListModel, MyDbContext>(factory, mapperProfile)
{

}