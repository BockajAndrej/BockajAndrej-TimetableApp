using application.BL.Facades;
using application.BL.Facades.RelationSet;
using application.BL.Models.Details;
using application.BL.Models.RelationSet;
using application.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace application.BL.Tests.FacadeTests;

[Collection("Facade Test Collection")]
public class CpFacadeTests : FacadeTests
{
    CpFacade _facade;
    private readonly ServiceProvider _serviceProvider;

    private ITestOutputHelper _testOutputHelper;
    public CpFacadeTests(ITestOutputHelper testOutputHelper, FacadeTests fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
        Factory = _serviceProvider.GetRequiredService<IDbContextFactory<MyDbContext>>();
        
        _facade = new CpFacade(Factory, Mapper);
        
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task TestComplexAsyncTask()
    {
        //Arrange
        var model = new CpDetailModel()
        {
            IdEmployee = "EMP001",
            IdStartCity = 3,
            IdEndCity = 4,
            CreationDate = DateOnly.FromDateTime(DateTime.Now),
            StartTime = DateTimeOffset.Now,
            EndTime = DateTimeOffset.Now.AddHours(8),
            CpState = "Vytvorený",
        };
        
        //Act
        int getBefore = (await _facade.GetAsync()).Count();
        model = await _facade.SaveAsync(model);
        Assert.NotEqual(0, model.Id);
        var retList = await _facade.GetAsync(filter: v => v.Id == model.Id);
        int getAfter = (await _facade.GetAsync()).Count();

        
        //Assert
        Assert.NotNull(retList);
        Assert.Equal(retList.First().Id, model.Id);
        Assert.Equal(getBefore+1, getAfter);
        
        //Act 
        await _facade.DeleteAsync(retList.First().Id);
        retList = await _facade.GetAsync(filter: v => v.Id == model.Id);
        getAfter = (await _facade.GetAsync()).Count();

        //Assert
        Assert.Empty(retList);
        Assert.Equal(getBefore, getAfter);
    }

    [Fact]
    public async Task TestManyToManyAsync()
    {
        //Arrange
        VehicleFacade vehicleFacade = new VehicleFacade(Factory, Mapper);
        
        var model = new CpDetailModel()
        {
            IdEmployee = "EMP001",
            IdStartCity = 3,
            IdEndCity = 4,
            CreationDate = DateOnly.FromDateTime(DateTime.Now),
            StartTime = DateTimeOffset.Now,
            EndTime = DateTimeOffset.Now.AddHours(8),
            CpState = "Zrušený",
        };
        var modelVehicle = new VehicleDetailModel { VehicleName = "Testing vehicle"};
        
        modelVehicle = await vehicleFacade.SaveAsync(modelVehicle);
        model = await _facade.SaveAsync(model);
        await _facade.SaveVehicle(model, modelVehicle);
        
        var modelList = await _facade.GetAsync();
        foreach (var item in modelList)
        {
            _testOutputHelper.WriteLine($"CP: {item.Id}, {item.CpState} ");
            foreach (var vehicle in item.VehicleList)
            {
                _testOutputHelper.WriteLine($"\t- {item.IdEmployee} -> Vehicle: {vehicle.Id}, {vehicle.VehicleName} ");
            }
        }
    }
}