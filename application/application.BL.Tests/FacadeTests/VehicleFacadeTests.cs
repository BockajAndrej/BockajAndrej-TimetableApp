using application.BL.Facades;
using application.BL.Models.Details;
using Xunit.Abstractions;

namespace application.BL.Tests.FacadeTests;

[Collection("Sequential")]
public class VehicleFacadeTests : FacadeTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    VehicleFacade _facade;
    public VehicleFacadeTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _facade = new VehicleFacade(Factory, Mapper);
    }

    [Fact]
    public async Task TestSaveAsync()
    {
        //Arrange
        var model = new VehicleDetailModel { VehicleName = "Testing vehicle"};
        
        //Act
        int cntBefor = (await _facade.GetAsync()).Count();
        await _facade.SaveAsync(model);
        var retList = await _facade.GetAsync(filter: v => v.VehicleName == model.VehicleName);
        int cntAfter = (await _facade.GetAsync()).Count();

        
        //Assert
        Assert.NotNull(retList);
        Assert.Equal(retList.First().VehicleName, model.VehicleName);
        Assert.Equal(cntBefor+1, cntAfter);
        
        //Act 
        await _facade.DeleteAsync(retList.First());
        retList = await _facade.GetAsync(filter: v => v.Id == retList.First().Id);
        cntAfter = _facade.GetAsync().Result.Count;

        //Assert
        Assert.Empty(retList);
        Assert.Equal(cntBefor, cntAfter);
    }

    [Fact]
    public async Task TestDeleteAsync()
    {
        var all = await _facade.GetAsync();
        foreach (var model in all)
        {
            _testOutputHelper.WriteLine($"Vehicle: {model.Id} {model.VehicleName}");
            foreach (var cp in model.CpList)
            {
                _testOutputHelper.WriteLine($"\t Cp: {cp.Id} {cp.IdEmployee} {cp.CpState}");
            }
            // await _facade.DeleteAsync(model);
        }
    }
}