using application.BL.Facades;
using application.BL.Models.Details;
using Xunit.Abstractions;

namespace application.BL.Tests.FacadeTests;

public class EmployeeFacadeTests : FacadeTests
{
    EmployeeFacade _facade;
    public EmployeeFacadeTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _facade = new EmployeeFacade(Factory, Mapper);
    }
    
    [Fact]
    public async Task TestSaveAsync()
    {
        //Arrange
        var model =  new EmployeeDetailModel()
        {
            Id = "EMP004",
            FirstName = "Tesing",
            LastName = "Employee",
            BirthNumber = "123456/7890",
            BirthDay = new DateOnly(2000, 1, 1)
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