using application.BL.Facades;
using application.BL.Models.Details;
using Xunit.Abstractions;

namespace application.BL.Tests.FacadeTests;

public class CityFacadeTests : FacadeTests
{
    private CityFacade _facade;
    public CityFacadeTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _facade = new CityFacade(Factory, Mapper);
    }
    
    [Fact]
    public async Task TestSaveAsync()
    {
        //Arrange
        var model = new CityDetailModel()
        {
            Id = 100,
            CityName = "Test city name", 
            Latitude = 25, 
            Longitude = 35, 
            StateName = "Test country name"
        };
        
        //Act
        int cntBefor = (await _facade.GetAsync()).Count();
        await _facade.SaveAsync(model);
        var retList = await _facade.GetAsync(filter: v => v.Id == model.Id);
        int cntAfter = (await _facade.GetAsync()).Count();

        
        //Assert
        Assert.NotNull(retList);
        Assert.Equal(retList.First().Id, model.Id);
        Assert.Equal(cntBefor+1, cntAfter);
        
        //Act 
        await _facade.DeleteAsync(model);
        retList = await _facade.GetAsync(filter: v => v.Id == model.Id);
        cntAfter = (await _facade.GetAsync()).Count();

        //Assert
        Assert.Empty(retList);
        Assert.Equal(cntBefor, cntAfter);
    }
}