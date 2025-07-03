using application.BL.Facades.RelationSet;
using application.BL.Mappers;
using application.BL.Mappers.RelationSet;
using application.BL.Models;
using application.BL.Models.Details;
using application.BL.Models.RelationSet;
using application.DAL;
using application.DAL.Entities;
using application.DAL.Factories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace application.BL.Facades;

public class CpFacade(
    IDbContextFactory<MyDbContext> factory,
    IMapper mapperProfile
    ) : Facade<Cp, CpDetailModel, MyDbContext, int>(factory, mapperProfile)
{
    private readonly IDbContextFactory<MyDbContext> _factory = factory;
    private readonly IMapper _mapperProfile = mapperProfile;

    public async Task<CpDetailModel> SaveVehicle(CpDetailModel model, VehicleDetailModel vehicle)
    {
        TransportFacade transportFacade = new TransportFacade(_factory, _mapperProfile);
        VehicleFacade vehicleFacade = new VehicleFacade(_factory, _mapperProfile);

        TransportListModel transportListModel = new TransportListModel()
        {
            Vehicle = vehicle,
            Cp = model
        };
        transportListModel = await transportFacade.SaveAsync(transportListModel);
        model.TransportList.Add(transportListModel);
        vehicle.TransportList.Add(transportListModel);

        vehicle = await vehicleFacade.SaveAsync(vehicle);
        model = await this.SaveAsync(model);

        return model;
    }
}