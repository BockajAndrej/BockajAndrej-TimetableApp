using application.BL.Facades;
using application.BL.Models.Details;
using application.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit.Abstractions;

namespace application.BL.Tests.FacadeTests;

[Collection("Facade Test Collection")]
public class EmployeeFacadeTests : FacadeTests
{
    EmployeeFacade _facade;
    private readonly ServiceProvider _serviceProvider;
    public EmployeeFacadeTests(ITestOutputHelper testOutputHelper, FacadeTests fixture)
    {
        _serviceProvider = fixture.ServiceProvider;
        Factory = _serviceProvider.GetRequiredService<IDbContextFactory<MyDbContext>>();

        _facade = new EmployeeFacade(Factory, Mapper);
    }

    [Fact]
    public async Task TestSaveAsync()
    {
        //Arrange
        var model = new EmployeeDetailModel()
        {
            Id = "TestId",
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
        Assert.Equal(cntBefor + 1, cntAfter);

        //Act 
        await _facade.DeleteAsync(model.Id);
        retList = await _facade.GetAsync(filter: v => v.Id == model.Id);
        cntAfter = (await _facade.GetAsync()).Count();

        //Assert
        Assert.Empty(retList);
        Assert.Equal(cntBefor, cntAfter);
    }
}