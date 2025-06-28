using application.BL.Models.RelationSet;
using application.DAL.Entities;
using application.DAL.Factories;
using AutoMapper;

namespace application.BL.Facades.RelationSet;

public class TransportFacade(
    DbContexCpFactory factory, 
    IMapper mapperProfile
) : Facade<Transport, TransportListModel>(factory, mapperProfile)
{
    
}