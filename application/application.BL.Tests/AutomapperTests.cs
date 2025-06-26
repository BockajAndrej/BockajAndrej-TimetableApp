using application.BL.Facades;
using application.BL.Mappers;
using application.BL.Models;
using application.BL.Models.Details;
using application.DAL;
using application.DAL.Entities;
using application.DAL.Factories;
using AutoMapper;
using Xunit.Abstractions;

namespace application.BL.Tests;

public class AutomapperTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    public AutomapperTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void TestAutoMapperFunction()
    {
        var emp = new Employee
        {
            Id = "EMP004",
            FirstName = "Tesing",
            LastName = "Employee",
            BirthNumber = "123456/7890",
            BirthDay = new DateOnly(2000, 1, 1)
        };
        
        var trip = new Cp
        {
            IdStartCity = 3,
            IdEndCity = 4,
            CreationDate = DateOnly.FromDateTime(DateTime.Now),
            StartTime = DateTimeOffset.Now,
            EndTime = DateTimeOffset.Now.AddHours(8),
            CpState = "OK"
        };
        
        var trip2 = new Cp
        {
            IdStartCity = 3,
            IdEndCity = 4,
            CreationDate = DateOnly.FromDateTime(DateTime.Now),
            StartTime = DateTimeOffset.Now,
            EndTime = DateTimeOffset.Now.AddHours(8),
            CpState = "OK"
        };
        
        trip.IdEmployeeNavigation = emp;
        emp.Cps.Add(trip);
        emp.Cps.Add(trip2);
        
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<EmployeeMapperConfig>();
            cfg.AddProfile<CpMapperConfig>();
        });
        
        var mapper = mapperConfig.CreateMapper();
        
        EmployeeDetailModel employeeDetail = mapper.Map<EmployeeDetailModel>(emp);
        
        _testOutputHelper.WriteLine(employeeDetail.FirstName + " " + employeeDetail.LastName);

        foreach (var detailTrip in employeeDetail.Trips)
        {
            _testOutputHelper.WriteLine($"{detailTrip.Id} {detailTrip.StartTime}");
        }
    }
}